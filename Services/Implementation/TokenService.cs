using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Tecman.Configurations;
using Tecman.Models;
using Tecman.Repository;
using Tecman.ValueObject.TokenObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Tecman.Services.Implementation
{
    public class TokenService : ITokenService
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _configuration;
        private ITokenRepository _repository;
        private IUserRepository _user;

        public TokenService(TokenConfiguration configuration, ITokenRepository repository, IUserRepository user)
        {
            _configuration = configuration;
            _repository = repository;
            _user = user;
        }

        public bool RevokeTokenByValue(string value)
        {
            return _repository.DeleteTokenByValue(value);
        }
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_configuration.MinutesAccessToken),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiryToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCulture))
                throw new SecurityTokenException("Invalid Token");

            return principal;
        }

        public bool CreateUserToken(int id, string token, int tokenType, DateTime expirationDate, DateTime? dateOfUser)
        {
            TokenType tokenType1 = _repository.GetTokenType(tokenType);

            UserToken userToken = _repository.GetTokenByUserIdAndTokenType(id, tokenType);
            if (userToken != null)
            {
                userToken.token = token;
                userToken.expirationDate = expirationDate;
                userToken.dateOfUse = dateOfUser;
            }
            else
            {
                User user = _user.Find(id);
                userToken = new UserToken
                {
                    user = user,
                    token = token,
                    tokenType = tokenType1,
                    expirationDate = expirationDate,
                    dateOfUse = dateOfUser,

                };
            }

            var result = _repository.Create(userToken);

            if (result == null) return false;

            return true;

        }

        public UserToken GetUserToken(int userId, string token)
        {
            return _repository.GetUserToken(userId, token);
        }


        // Service To Get Encoded Payload of JwtToken
        public JwtCredentials GetCrendentials(string token)
        {
            var handler = new JwtSecurityTokenHandler(); //Create a new JwTSecurityTokenHandler
            var jsonToken = handler.ReadToken(token); // Decode Token JwT in SecurityToken
            var tokenS = jsonToken as JwtSecurityToken; // Associate a SecurityToken in JwtSecurityToken
            return new JwtCredentials(tokenS.Payload["unique_name"].ToString(), Int32.Parse(tokenS.Payload["nameid"].ToString())); // Create a JwtCredentials with payload data of JwtSecurityToken
        }

        public UserToken GetUserTokenByUserIdAndTokenTypeId(int userId, int token)
        {
            return _repository.GetUserTokenByUserIdAndTokenTypeId(userId, token);
        }

        public TokenType GetTokenType(int id)
        {
            return _repository.GetTokenType(id);
        }
    }
}

    