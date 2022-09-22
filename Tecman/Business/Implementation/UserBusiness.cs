using Tecman.Configurations;
using Tecman.Models;
using Tecman.Repository;
using Tecman.Services;
using Tecman.ValueObject;
using Tecman.ValueObject.NewFolder;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;
using RestSharp;
using Tecman.ValueObject.UserObject;
using System.Security.Cryptography;

namespace Tecman.Business.Implementation
{
    public class UserBusiness : IUserBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private IResponseApiService _response;
        private IUserRepository _repository;
        private ITokenService _service;
        private TokenConfiguration _configuration;
        private IEmployeeBusiness _employee;


        public UserBusiness(IResponseApiService response, IUserRepository repository, ITokenService service, TokenConfiguration configuration, IEmployeeBusiness employee)
        {
            _response = response;
            _repository = repository;
            _service = service;
            _configuration = configuration;
            _employee = employee;

        }

        public ApiMessage Create(UserCreate userCreate)
        {
            User aux = _repository.FindByUsername(userCreate.username);
            if (aux != null) return _response.ResponseApi(-1, "Usuário já existente!");

            User user = new User{
                username = userCreate.username,
                password = userCreate.password,
                registrationDate = DateTime.Now,
                employee = _employee.Find(userCreate.employeeId),
            };

            return _repository.Create(user);

        }

        public User FindByEmployeeId(int id)
        {
            return _repository.FindByEmployeeId(id);
        }

        public User FindById(int id)
        {
            return _repository.Find(id);
        }

        public User FindByUsername(string username)
        {
            return _repository.FindByUsername(username);
        }

        public bool RevokeToken(string username)
        {
            User user = _repository.FindByUsername(username);
            TokenType token = _service.GetTokenType(2);
            UserToken userToken = _service.GetUserTokenByUserIdAndTokenTypeId(user.id, token.id);

            bool revoke = _service.RevokeTokenByValue(userToken.token);
            return revoke;
        }

        public bool Update(UserUpdate userUpdate, User user)
        {

            user.username = userUpdate.username;
            user.password = _repository.ComputeHash(userUpdate.password, new SHA256CryptoServiceProvider()).ToString();
            user.employee = _employee.Find(userUpdate.employeeId);
            user.employee.role = _employee.FindRoleById(userUpdate.role);

            ApiMessage update = _repository.Update(user);

            return update.Success;
        }

        public TokenObject ValidateCredentials(UserCredentials userCredentials)
        {
            // Validate user
            var user = _repository.ValidateCredentials(userCredentials);
            if (user == null) return null; // First Return Invalid User

            if (user.employee.employeeStatus.id == 2)
            {
                return null;
            }

            if (user.registrationDate > DateTime.Now)
            {
                return null;
            }

            if (user.deactivationDate < DateTime.Now)
            {
                user.employee.employeeStatus = _employee.FindEmployeeStatusById(2);
                _repository.Update(user);
                return null;
            }


            // Generate Claim, input user guid to JWT
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                //TODO employee.name
                new Claim(JwtRegisteredClaimNames.UniqueName, user.username),
                new Claim(JwtRegisteredClaimNames.NameId, user.id.ToString())
            };

            var accessToken = _service.GenerateAccessToken(claims);
            var refreshToken = _service.GenerateRefreshToken();

            // Refresh token 

            DateTime createDate = DateTime.Now;

            // 5 minutes more then now
            DateTime expirationDateAccessToken = createDate.AddMinutes(_configuration.MinutesAccessToken);
            // 24 hour mores then now
            DateTime expirationDateRefreshToken = createDate.AddMinutes(_configuration.MinutesRefreshToken);

            // Creates the user new access token
            _service.CreateUserToken(user.id, accessToken, 1, Convert.ToDateTime(expirationDateAccessToken.ToString(DATE_FORMAT)), null);

            // Creates the user new refresh token
            _service.CreateUserToken(user.id, refreshToken, 2, Convert.ToDateTime(expirationDateRefreshToken.ToString(DATE_FORMAT)), null);

            return new TokenObject(
                true,
                user.id,
                user.employee.employeeStatus.id,
                user.employee.role.id,
                user.username,
                user.employee.name,
                createDate.ToString(DATE_FORMAT),
                expirationDateAccessToken.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
            );
        }
        public TokenObject ValidateCredentials(string currentRefreshToken)
        {
            // Gets the user token, by refresh token
            UserToken userRefreshToken = _repository.ValidateRefreshToken(currentRefreshToken);
            // If the result is not null
            if (userRefreshToken == null)
            {
                return null;
            }
            else
            {
                // If refresh token is still valid
                if (userRefreshToken.expirationDate < DateTime.Now)
                {
                    return null;
                }
                else
                {
                    // Gets the user access toke
                    UserToken userAccessToken = _repository.GetExpireAccessToken(userRefreshToken.user.id);
                    // If user access token not null
                    if (userAccessToken == null)
                    {
                        return null;
                    }
                    else
                    {
                        // Gets the user status
                        User user = _repository.Find(userRefreshToken.user.id);

                        if(user.employee.employeeStatus.id == 2)
                        {
                            return null;
                        }

                        // gets the IDs
                        var principal = _service.GetPrincipalFromExpiryToken(userAccessToken.token);
                        // Generates a new access token
                        var accessToken = _service.GenerateAccessToken(principal.Claims);
                        // Generates a new refresh token
                        var refreshToken = _service.GenerateRefreshToken();

                        DateTime createDate = DateTime.Now;
                        // 5 minutes more then now
                        DateTime expirationDateAccessToken = createDate.AddMinutes(_configuration.MinutesAccessToken);
                        // 24 hour mores then now
                        DateTime expirationDateRefreshToken = createDate.AddMinutes(_configuration.MinutesRefreshToken);

                        // Creates the user new access token
                        _service.CreateUserToken(userRefreshToken.user.id, accessToken, 1, Convert.ToDateTime(expirationDateAccessToken.ToString(DATE_FORMAT)), null);

                        // Creates the user new refresh token
                        _service.CreateUserToken(userRefreshToken.user.id, refreshToken, 2, Convert.ToDateTime(expirationDateRefreshToken.ToString(DATE_FORMAT)), null);

                        return new TokenObject(
                            true,
                            userRefreshToken.user.id,
                            user.employee.employeeStatus.id,
                            user.employee.role.id,
                            userRefreshToken.user.username,
                            user.employee.name,
                            createDate.ToString(DATE_FORMAT),
                            expirationDateAccessToken.ToString(DATE_FORMAT),
                            accessToken,
                            refreshToken
                        );
                    }
                }
            }
        }
    }

}
