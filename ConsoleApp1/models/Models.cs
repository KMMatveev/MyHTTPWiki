using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit.IO.Filters;
using MyHTTPServer.attributes;
using MyHTTPServer.services;
using static System.Net.Mime.MediaTypeNames;

namespace MyHTTPServer.models
{
    [ModelAttribute("user")]
    public class user
    {
        public int user_id { get; set; }
        public string login { get; set; }
        public int password { set; get; }
        public string name { get; set; }
        public bool admin { get; set; } = false;
        private static int count { get 
            { 
                if(SelectAllUser()!=null)
                    return (SelectAllUser().Last().user_id+1); 
                else return 0;
            } 
        }
        private static MyORM myORM = new MyORM();
        public user(int user_id, string login, string name, string password)
        {
            this.user_id = user_id;
            this.login = login;
            this.name = name;
            this.password = password.GetHashCode();
            this.admin = false;
        }
        public user( string login, string name, string password)
        {
            this.user_id = count;
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
        public user(int user_id, string login, string name, int password,bool admin)
        {
            this.user_id = user_id;
            this.login = login;
            this.name = name;
            this.admin = admin;
            this.password = password;
        }


        [Post("AddUser")]
        static async public void AddUser(user obj)
        {
            Console.WriteLine(myORM.Add<user>(obj));
        }

        [Post("UpdateUser")]
        static async public void UpdateUser(user obj)
        {

            Console.WriteLine(myORM.Update<user>(obj));
        }

        [Post("DeleteUser")]
        static async public void DeleteUser(int id)
        {
            Console.WriteLine(myORM.Delete<user>(id));
        }


        [Post("SelectUser")]
        static public void SelectUserByID(int id)
        {
            Console.WriteLine(myORM.SelectById<user>(id));
        }

        [Post("SelectAllUser")]
        static public List<user> SelectAllUser()
        {
            List<user> result = new List<user>();
            string command = "select * from users;";
            var reader = myORM.UseCommand(command);
            // Читаем результаты
            while (reader.Read())
            {
                user item = new user(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(4), reader.GetBoolean(3));

                result.Add(item);
            }
            return result;
        }
        public string UserAsRowHTML()
        {
            string result = @$"
<div>
    <div class=""block_fl id_width"">{user_id}</div>
    <div class=""block_fl"">{login}</div>
    <div class=""block_fl"">{name}</div>
    <div class=""block_fl"">{password}</div>
    <div class=""block_fl"">{admin}</div>
</div>";
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
        public int basic_id { get; set; }
        private static int count
        {
            get
            {
                if (SelectAllText() != null)
                    return (SelectAllText().Last().text_id + 1);
                else return 0;
            }
        }
        private static MyORM myORM = new MyORM();
        public text(int text_id, string before, string body, string after, int basic_id)
        {
            this.text_id = text_id;
            this.before = before;
            this.body = body;
            this.after = after;
            this.basic_id = basic_id;
        }
        public text( string before, string body, string after, int basic_id)
        {
            this.text_id = count;
            this.before = before;
            this.body = body;
            this.after = after;
            this.basic_id = basic_id;
        }
        

        [Post("AddText")]
        static async public void AddText(text obj)
        {
            Console.WriteLine(myORM.Add<text>(obj));
        }

        [Post("UpdateText")]
        static async public void UpdateText(text obj)
        {

            Console.WriteLine(myORM.Update<text>(obj));
        }

        [Post("DeleteText")]
        static async public void DeleteText(int id)
        {
            Console.WriteLine(myORM.Delete<text>(id));
        }

        [Post("SelectAllText")]
        static public List<text> SelectAllText()
        {
            List<text> result = new List<text>();
            string command = "select * from texts;";
            var reader = myORM.UseCommand(command);
            // Читаем результаты
            while (reader.Read())
            {
                text item = new text(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4));

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
        private static int count
        {
            get
            {
                if (SelectAllImage() != null)
                    return (SelectAllImage().Last().image_id + 1);
                else return 0;
            }
        }
        private static MyORM myORM = new MyORM();
        public image (int image_id, int height, int width, string image_link)
        {
            this.image_id = image_id;
            this.height = height;
            this.width = width;
            this.image_link = image_link;
        }

        [Post("AddImage")]
        static async public void AddImage(image obj)
        {
            Console.WriteLine(myORM.Add<image>(obj));
        }

        [Post("UpdateImage")]
        static async public void UpdateImage(image obj)
        {

            Console.WriteLine(myORM.Update<image>(obj));
        }

        [Post("DeleteImage")]
        static async public void DeleteImage(int id)
        {
            Console.WriteLine(myORM.Delete<image>(id));
        }

        [Post("SelectImage")]
        static public void SelectImageByID(int id)
        {
            Console.WriteLine(myORM.SelectById<image>(id));
        }


        [Post("SelectAllImage")]
        static public List<image> SelectAllImage()
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
        public int basic_id { get; set;}
        private static int count
        {
            get
            {
                if (SelectAllArmor() != null)
                    return (SelectAllArmor().Last().armor_id + 1);
                else return 0;
            }
        }
        private static MyORM myORM = new MyORM();
        public armor(int armor_id, string armor_name, string description, string img_link, int basic_id)
        {
            this.armor_id = armor_id;
            this.armor_name = armor_name;
            this.description = description;
            this.img_link = img_link;
            this.basic_id = basic_id;
        }
        public armor( string armor_name, string description, string img_link, int basic_id)
        {
            this.armor_id = count;
            this.armor_name = armor_name;
            this.description = description;
            this.img_link = img_link;
            this.basic_id = basic_id;
        }
        
        [Post("AddArmor")]
        static async public void AddArmor(armor obj)
        {
            Console.WriteLine(myORM.Add<armor>(obj));
        }

        [Post("UpdateArmor")]
        static async public void UpdateArmor(armor obj)
        {

            Console.WriteLine(myORM.Update<armor>(obj));
        }

        [Post("DeleteArmor")]
        static async public void DeleteArmor(int id)
        {
            Console.WriteLine(myORM.Delete<armor>(id));
        }


        [Post("SelectArmor")]
        static public armor SelectArmorByID(int id)
        {
            //Console.WriteLine(myORM.SelectById<armor>(id));
            List<armor> result = new List<armor>();
            string command = $"select * from armors where armor_id={id};";
            var reader = myORM.UseCommand(command);
            // Читаем результаты
            while (reader.Read())
            {
                armor item = new armor(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4));
                return item;
                result.Add(item);
            }
            return null;
        }

        [Post("SelectAllArmor")]
        static public List<armor> SelectAllArmor()
        {
            List<armor> result = new List<armor>();
            string command = "select * from armors;";
            var reader = myORM.UseCommand(command);
            // Читаем результаты
            while (reader.Read())
            {
                armor item = new armor(reader.GetInt32(0),reader.GetString(1),reader.GetString(2),reader.GetString(3), reader.GetInt32(4));

                result.Add(item);
            }
            return result;
        }
    }

    [ModelAttribute("basic")]
    public class basic
    {
        public int basic_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string img_link { get; set; }
        private static int count
        {
            get
            {
                if (SelectAllBasic() != null)
                    return (SelectAllBasic().Last().basic_id + 1);
                else return 0;
            }
        }
        private static MyORM myORM = new MyORM();
        public basic(int id, string name, string description, string img_link)
        {
            this.basic_id = id;
            this.name = name;
            this.description = description;
            this.img_link = img_link;
        }
        [Post("AddBasic")]
        static async public void AddBasic(basic obj)
        {
            Console.WriteLine(myORM.Add<basic>(obj));
        }

        [Post("UpdateBasic")]
        static async public void UpdateBasic(basic obj)
        {

            Console.WriteLine(myORM.Update<basic>(obj));
        }

        [Post("DeleteBasic")]
        static async public void DeleteBasic(int id)
        {
            Console.WriteLine(myORM.Delete<basic>(id));
        }


        [Post("SelectBasic")]
        static public void SelectBasicByID(int id)
        {
            Console.WriteLine(myORM.SelectById<basic>(id));
        }

        [Post("SelectAllBasic")]
        static public List<basic> SelectAllBasic()
        {
            List<basic> result = new List<basic>();
            string command = "select * from basics;";
            var reader = myORM.UseCommand(command);
            // Читаем результаты
            while (reader.Read())
            {
                basic item = new basic(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));

                result.Add(item);
            }
            return result;
        }
    }

    public class ArmorList
    {
        public List<armor> Armors { get; set; }
        public ArmorList(List<armor> armors)
        {
            this.Armors = armors;
        }
    }
    public class BasicList
    {
        public List<basic> Basics { get; set; }
        public BasicList(List<basic> Basics) => this.Basics = Basics;
    }
    public class TextList
    {
        public List<text> Texts { get; set; }
        public TextList(List<text> texts) => this.Texts = texts;
    }

    public class ImageList
    {
        public List<image> Images { get; set; }
        public ImageList(List<image> images) => this.Images = images;
    }

    public class UserList
    {
        public List<user> Users { get; set; }
        public UserList(List<user> users) => this.Users = users;
    }
    public class Page 
    {
        public List<user> Users { get; set; }
        public List<image> Images { get; set; }
        public List<text> Texts { get; set; }
        public List<basic> Basics { get; set; }
        public List<armor> Armors { get; set; }
        public Page(List<basic> Basics, List<user> users,List<image> images, List<text> texts, List<armor> armors)
        {
            this.Users = users; this.Images = images; this.Basics = Basics; this.Armors = armors; this.Texts = texts;
        }
        public Page() { }
            

    }

}
