using MyHTTPServer.attributes;
using MyHTTPServer.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyHTTPServer.models;
using System.Data;

namespace MyHTTPServer.controllers
{
    [HttpController("AuthorizationController")]
    public class AuthorizationController
    {
        [Post("Login")]
        public Cookie Login(string login, string password)
        {
            var config = HttpServer.GetAppSettings();
            //var db = new MyORM(config.ConnectionString);


            var User = user.SelectAllUser().FirstOrDefault(user => user.login.Equals(login) && user.password.Equals(password.GetHashCode()));

            if (User == null)
                return default;
            string role = "user";
            if(User.admin)
            {
                role = "admin";
            }
            var cookie= new Cookie("role", role)
            {
                Domain = config.Address?.Split("//")[1],
                Path = "/",
                Expires = DateTime.Today.AddMonths(1)
            };
            //context.Response.Cookies.Add(cookie);
            return cookie;
        }

        [Post("Registration")]
        public Cookie Registration(string login,string name, string password)
        {
            var config = HttpServer.GetAppSettings();
            //var db = new MyORM(config.ConnectionString);

            var UserExemplar=new user(login,name,password);
            user.AddUser(UserExemplar);
            //var User = user.SelectAllUser().FirstOrDefault(user => user.login.Equals(realLogin) && user.password.Equals(realPassword));

            //if (User is null)
            //    return default;

            var cookie = new Cookie("role", "user")
            {
                Domain = config.Address?.Split("//")[1],
                Path = "/",
                Expires = DateTime.Today.AddMonths(1)
            };
            //context.Response.Cookies.Add(cookie);
            return cookie;
        }
    }
}
