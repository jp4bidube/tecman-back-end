using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.ValueObject.UserObject
{
    public class MeObject
    {
        public MeObject(string username, string name, string role, string avatarUrl, string email)
        {
            this.username = username;
            this.name = name;
            this.role = role;
            this.avatarUrl = avatarUrl;
            this.email = email;
        }

        public string username { get; set; }
        public string name { get; set; }
        public string role { get; set; }
        public string? avatarUrl { get; set; }
        public string email { get; set; }
    }
}
