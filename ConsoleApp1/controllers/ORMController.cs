using MyHTTPServer.attributes;
using MyHTTPServer.models;
using MyHTTPServer.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyHTTPServer.controllers
{
    [HttpController("ORMController")]
    public class ORMController
    {
        public static MyORM myORM = new MyORM();

        //Console.WriteLine(myORM.Add<products>(new products(4, "apple", 12.12, 1)));
        //Console.WriteLine(myORM.Update<products>(new products(3,"banana",12.12,1)));
        //Console.WriteLine(myORM.Update<products>(new products(3,"banana",12.12,1)));
        //Console.WriteLine(myORM.Delete<products>(4));
        //var list = myORM.Select<products>(new products(2, "apple", 12.12, 1));
        //foreach (var product in list)
        //{
        //    Console.WriteLine(product);
        //}


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


        [Post("SelectText")]
        async public void SelectTextByID(int id)
        {
            Console.WriteLine(myORM.SelectById<text>(id));
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


    }
}
