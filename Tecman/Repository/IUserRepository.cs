using Tecman.Models;
using Tecman.ValueObject;
using Tecman.ValueObject.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.Repository
{
    public interface IUserRepository
    {
        List<User> List();
        ApiMessage Create(User user);
        User FindByUsername(String username);
        UserStatus FindUserStatusById(int id);
        UserProfile FindUserProfileById(int id);
        User ValidateCredentials(UserCredentials user);
        User Find(int id);
        UserToken ValidateRefreshToken(string refreshToken);
        UserToken GetExpireAccessToken(int userId);

        ApiMessage Update(User user);

    }
}
