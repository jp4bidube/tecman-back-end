using Tecman.Models;
using Tecman.Models.Context;
using Tecman.ValueObject;
using Tecman.ValueObject.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tecman.Services;
using Tecman.Services.Implementation;

namespace Tecman.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private PostgreSqlDbContext _context;
        private IResponseApiService _response;
        public UserRepository(PostgreSqlDbContext context, IResponseApiService response)
        {
            _response = response;
            _context = context;
        }

        public ApiMessage Create(User user)
        {
            try
            { 
            var decriptPass = user.password;
            user.password = ComputeHash(user.password, new SHA256CryptoServiceProvider()).ToString();
            _context.Add(user);
            _context.SaveChanges();

                return _response.ResponseApi(0, "Usuário criado com sucesso!");
            }
            catch(Exception e)
            {   
                return _response.ResponseApi(-2, e.Message);

            }
        }
        public bool RevokeToken(string userCorporateEmail)
        {
            throw new NotImplementedException();
        }

        public User FindByUsername(string username)
        {
            return _context.User.FirstOrDefault(prop => (prop.username == username));
        }

        public List<User> List()
        {
            return _context.User.ToList();
        }

        public User ValidateCredentials(UserCredentials user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider()).ToString();
            User userRet = _context.User.FirstOrDefault(u => (u.username == user.Username) && (u.password == pass));
            return userRet;
        }
        public User Find(int id)
        {
            return _context.User.Find(id);
        }

        public UserToken ValidateRefreshToken(string refreshToken)
        {
            return _context.Token.SingleOrDefault(prop => prop.token.Equals(refreshToken) && prop.tokenType.id.Equals(2));
        }

        /**
         * Verifies if the user from the refresh token, has a valid access token
         * 
         */
        public UserToken GetExpireAccessToken(int userId)
        {

            return _context.Token.SingleOrDefault(prop => prop.user.id.Equals(userId) && prop.tokenType.id.Equals(1));
        }

        public object ComputeHash(string input, SHA256CryptoServiceProvider algotithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = algotithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        public ApiMessage Update(User user)
        {
            var result = _context.User.SingleOrDefault(p => p.id.Equals(user.id));


            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return new ApiMessage
            {
                Success = true,
                Result = result
            };
        }

        public User FindByEmployeeId(int id)
        {
            return _context.User.FirstOrDefault(prop=> prop.employee.id.Equals(id));
        }
    }
}
