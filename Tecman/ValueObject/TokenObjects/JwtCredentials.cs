using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.ValueObject.TokenObjects
{
    public class JwtCredentials
    {
        public JwtCredentials(string uniqueName , int id)
        {
            unique_name = uniqueName;
            nameid = id;
        }
        public string unique_name { get; set; }
        public int nameid { get; set; }

    }
}
