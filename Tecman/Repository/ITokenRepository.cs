using Tecman.Models;
using Tecman.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.Repository
{
    public interface ITokenRepository
    {

        ApiMessage Create(UserToken token);
        ApiMessage CreateRecoveryToken(UserToken token);
        TokenType GetTokenType(int id);
        UserToken GetTokenByValue(string tokenValue);
        UserToken GetTokenByValue(int id);
        List<UserToken> GetTokenByUserId(int user_id);
        bool DeleteToken(int id);
        UserToken GetTokenByUserIdAndTokenType(int user_id, int tokenTypeId);
        bool DeleteTokenByValue(string value);
        bool RevokeToken(UserToken userToken);
        bool Update(UserToken userToken);
        UserToken GetUserToken(int userId, string token);
    }
}
