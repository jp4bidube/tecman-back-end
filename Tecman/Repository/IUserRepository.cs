using Tecman.Models;
using Tecman.ValueObject;
using Tecman.ValueObject.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Tecman.Repository
{
    public interface IUserRepository
    {
        List<User> List();
        ApiMessage Create(User user);
        User FindByUsername(String username);
        User ValidateCredentials(UserCredentials user);
        User Find(int id);
        UserToken ValidateRefreshToken(string refreshToken);
        UserToken GetExpireAccessToken(int userId);
        ApiMessage Update(User user);
        bool RevokeToken(string userName);

        public object ComputeHash(string input, SHA256CryptoServiceProvider algotithm);

    }
}
