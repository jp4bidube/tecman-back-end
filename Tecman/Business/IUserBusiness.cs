using Tecman.Models;
using Tecman.ValueObject;
using Tecman.ValueObject.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.ValueObject.UserObject;

namespace Tecman.Business
{
    public interface IUserBusiness
    {
        ApiMessage Create(UserCreate userCreate);
        TokenObject ValidateCredentials(UserCredentials userCredentials);
        public TokenObject ValidateCredentials(string refreshToken);
        User FindById(int id);
        User FindByEmployeeId(int id);
        User FindByUsername(string username);
        bool Update(UserUpdate userUpdate, User user);
        bool RevokeToken(String username);


    }
}
