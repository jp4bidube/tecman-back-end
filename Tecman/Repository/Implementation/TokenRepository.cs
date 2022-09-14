using Tecman.Models;
using Tecman.Models.Context;
using Tecman.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.Repository.Implementation
{
    public class TokenRepository : ITokenRepository
    {
        PostgreSqlDbContext _context;

        public TokenRepository(PostgreSqlDbContext context)
        {
            _context = context;
        }

        public UserToken GetTokenByValue(string tokenValue)
        {
            return _context.Token.FirstOrDefault(prop => prop.token == tokenValue);
        }

        public UserToken GetTokenByValue(int id)
        {
            return _context.Token.Find(id);
        }

        public List<UserToken> GetTokenByUserId(int user_id)
        {
            return _context.Token.Where(prop => prop.user.id == user_id).ToList();
        }

        public UserToken GetTokenByUserIdAndTokenType(int userId, int tokenTypeId)
        {
            try { 
            return _context.Token.SingleOrDefault(prop => prop.user.id.Equals(userId) && prop.tokenType.id.Equals(tokenTypeId));
                
                Console.WriteLine("AQUI");
            }
            catch
            {
                return null;
            }
        }

        public ApiMessage Create(UserToken token)
        {
            UserToken verifyToken = _context.Token.SingleOrDefault(prop => prop.user.id.Equals(token.user.id) && prop.tokenType.Equals(token.tokenType));

            if (verifyToken != null)
            {

                try
                {
                    verifyToken.token = token.token;
                    verifyToken.expirationDate = token.expirationDate;
                    verifyToken.dateOfUse = token.dateOfUse;
                    _context.Entry(verifyToken).CurrentValues.SetValues(verifyToken);
                    _context.SaveChanges();

                    return new ApiMessage
                    {
                        Message = "Token updated success!",
                        Success = true,
                        Result = token,
                    };
                }
                catch (Exception e)
                {
                    return new ApiMessage
                    {
                        ErrorCode = -1,
                        Message = e.Message.ToString(),
                        Success = false
                    };
                }
            }
            else
            {
                try
                {
                    _context.Add(token);
                    _context.SaveChanges();

                    return new ApiMessage
                    {
                        Message = "Token updated success!",
                        Success = true
                    };
                }
                catch (Exception e)
                {
                    return new ApiMessage
                    {
                        ErrorCode = -1,
                        Message = e.Message.ToString(),
                        Success = false
                    };
                }
            }




        }
        public ApiMessage CreateRecoveryToken(UserToken token)
        {
            try
            {
                _context.Add(token);
                _context.SaveChanges();

                return new ApiMessage
                {
                    Message = "Token updated success!",
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new ApiMessage
                {
                    ErrorCode = -1,
                    Message = e.Message.ToString(),
                    Success = false
                };
            }

        }

        public bool DeleteToken(int id)
        {
            try
            {
                UserToken token = _context.Token.Find(id);

                _context.Token.Remove(token);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteTokenByValue(string value)
        {
            try
            {
                UserToken token = _context.Token.Where(prop => prop.token == value).SingleOrDefault();

                _context.Token.Remove(token);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public TokenType GetTokenType(int id)
        {
            return _context.TokenType.Find(id);
        }

        public bool RevokeToken(UserToken userToken)
        {
            try
            {
                UserToken token = _context.Token.Find(userToken.user_token_id);

                _context.Token.Remove(token);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(UserToken userToken)
        {
            try
            {
                UserToken token = _context.Token.Find(userToken.user_token_id);
                _context.Entry(token).CurrentValues.SetValues(userToken);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public UserToken GetUserToken(int userId, string token)
        {
            return _context.Token.SingleOrDefault(prop => prop.user.id.Equals(userId) && prop.token.Equals(token));
        }
    }
}
