using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHTTPServer.models
{
    public class PersonalCookie
    {
        public string login { get; set; }
        public int password { get; set; }

        PersonalCookie(string login, int password) 
        {
            this.login = login;
            this.password = password;
        }
    }
}
