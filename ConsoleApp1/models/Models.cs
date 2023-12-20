using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit.IO.Filters;
using MyHTTPServer.attributes;
using MyHTTPServer.services;

namespace MyHTTPServer.models
{
    [ModelAttribute("user")]
    public class user
    {
        public int user_id { get; set; }
        public string login { get; }
        public int password { set { password = value.GetHashCode(); } get { return password; } }
        public string name { get; set; }
        public bool admin { get; set; } = false;
        private static MyORM myORM = new MyORM();
        public user(int user_id, string login, string name, string password)
        {
            this.user_id = user_id;
            this.login = login;
            this.name = name;
            this.password = password.GetHashCode();
            this.admin = false;
        }
        public user(int user_id, string login, string name, int password)
        {
            this.user_id = user_id;
            this.login = login;
            this.name = name;
            this.password = password;
            this.admin = false;
        }
        public user(int user_id, string login, string name, bool admin, string password)
        {
            this.user_id = user_id;
            this.login = login;
            this.name = name;
            this.admin = admin;
            this.password = password.GetHashCode();
        }


        [Post("AddUser")]
        async public void AddUser(user obj)
        {
            Console.WriteLine(myORM.Add<user>(obj));
        }

        [Post("UpdateUser")]
        async public void UpdateUser(user obj)
        {

            Console.WriteLine(myORM.Update<user>(obj));
        }

        [Post("DeleteUser")]
        async public void DeleteUser(int id)
        {
            Console.WriteLine(myORM.Delete<user>(id));
        }


        [Post("SelectUser")]
        async public void SelectUserByID(int id)
        {
            Console.WriteLine(myORM.SelectById<user>(id));
        }

        [Post("SelectAllUser")]
        public List<user> SelectAllUser()
        {
            List<user> result = new List<user>();
            string command = "select * from images;";
            var reader = myORM.UseCommand(command);
            // Читаем результаты
            while (reader.Read())
            {
                user item = new user(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));

                result.Add(item);
            }
            return result;
        }
    }

    [ModelAttribute("text")]
    public class text
    {
        public int text_id { get; set; }
        public string before {  get; set; }
        public string body { get; set; }
        public string after { get; set; }
        private static MyORM myORM = new MyORM();
        public text(int text_id, string before, string body, string after)
        {
            this.text_id = text_id;
            this.before = before;
            this.body = body;
            this.after = after;
        }

        [Post("AddText")]
        async public void AddText(text obj)
        {
            Console.WriteLine(myORM.Add<text>(obj));
        }

        [Post("UpdateText")]
        async public void UpdateText(text obj)
        {

            Console.WriteLine(myORM.Update<text>(obj));
        }

        [Post("DeleteText")]
        async public void DeleteText(int id)
        {
            Console.WriteLine(myORM.Delete<text>(id));
        }

        [Post("SelectAllText")]
        public List<text> SelectAllText()
        {
            List<text> result = new List<text>();
            string command = "select * from images;";
            var reader = myORM.UseCommand(command);
            // Читаем результаты
            while (reader.Read())
            {
                text item = new text(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));

                result.Add(item);
            }
            return result;
        }

    }

    [ModelAttribute("image")]
    public class image
    {
        public int image_id { get; set;}
        public int height { get; set;}
        public int width { get; set;}
        public string image_link { get; set; }
        private static MyORM myORM = new MyORM();
        public image (int image_id, int height, int width, string image_link)
        {
            this.image_id = image_id;
            this.height = height;
            this.width = width;
            this.image_link = image_link;
        }

        [Post("AddImage")]
        async public void AddImage(image obj)
        {
            Console.WriteLine(myORM.Add<image>(obj));
        }

        [Post("UpdateImage")]
        async public void UpdateImage(image obj)
        {

            Console.WriteLine(myORM.Update<image>(obj));
        }

        [Post("DeleteImage")]
        async public void DeleteImage(int id)
        {
            Console.WriteLine(myORM.Delete<image>(id));
        }

        [Post("SelectImage")]
        async public void SelectImageByID(int id)
        {
            Console.WriteLine(myORM.SelectById<image>(id));
        }


        [Post("SelectAllImage")]
        public List<image> SelectAllArmor()
        {
            List<image> result = new List<image>();
            string command = "select * from images;";
            var reader = myORM.UseCommand(command);
            // Читаем результаты
            while (reader.Read())
            {
                image item = new image(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3));

                result.Add(item);
            }
            return result;
        }
    }

    [ModelAttribute("armor")]
    public class armor 
    {
        public int armor_id {  get; set;}
        public string armor_name { get; set;}
        public string description { get; set;}
        public string img_link { get; set; }
        private static MyORM myORM = new MyORM();
        public armor(int armor_id, string armor_name, string description, string img_link)
        {
            this.armor_id = armor_id;
            this.armor_name = armor_name;
            this.description = description;
            this.img_link = img_link;
        }
        [Post("AddArmor")]
        async public void AddArmor(armor obj)
        {
            Console.WriteLine(myORM.Add<armor>(obj));
        }

        [Post("UpdateArmor")]
        async public void UpdateArmor(armor obj)
        {

            Console.WriteLine(myORM.Update<armor>(obj));
        }

        [Post("DeleteArmor")]
        async public void DeleteArmor(int id)
        {
            Console.WriteLine(myORM.Delete<armor>(id));
        }


        [Post("SelectArmor")]
        async public void SelectArmorByID(int id)
        {
            Console.WriteLine(myORM.SelectById<armor>(id));
        }

        [Post("SelectAllArmor")]
        public List<armor> SelectAllArmor()
        {
            List<armor> result = new List<armor>();
            string command = "select * from armors;";
            var reader = myORM.UseCommand(command);
            // Читаем результаты
            while (reader.Read())
            {
                armor item = new armor(reader.GetInt32(0),reader.GetString(1),reader.GetString(2),reader.GetString(3));

                result.Add(item);
            }
            return result;
        }
    }
}
