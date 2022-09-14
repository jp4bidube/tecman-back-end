/**
 * Created: Daniel Quintal

 * Modified: Daniel Quintal
 * Date: January, 31, 2022
 *
 * Token service file - FRONT
 * 
 */

using Tecman.Models;
using Tecman.ValueObject.TokenObjects;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Tecman.Services {
    public interface ITokenService {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiryToken(string token);
        public bool RevokeTokenByValue(string value);
        public bool CreateUserToken(int id, string token, int tokenType, DateTime expirationDate, DateTime? dateOfUser);
        public UserToken GetUserToken(int userId, string token);
        public JwtCredentials GetCrendentials(string token);
    }
}