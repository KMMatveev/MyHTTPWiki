using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using MailKit.Search;
using MyHTTPServer.attributes;
using MyHTTPServer.models;
using MyHTTPServer.services;

namespace MyHTTPServer.controllers
{
    [HttpController("pageController")]
    public class PageController
    {
        public static Page page = new Page(basic.SelectAllBasic(),user.SelectAllUser(),image.SelectAllImage(),text.SelectAllText(),armor.SelectAllArmor());

        [Get("basic")]
        public static string basicPage(int id)
        {
            Console.WriteLine("Generating basic page");
            var armorList = new ArmorList(armor.SelectAllArmor().Where(a => a.basic_id == id).ToList());
            var textList = new TextList(text.SelectAllText().Where(a => a.basic_id == id).ToList());
            var Basics = new BasicList(basic.SelectAllBasic());
            var Armors = armorList;
            var Texts = textList;
            var template = File.ReadAllText(HttpServer.GetAppSettings().StaticFilePath+"/wiki/basic/index.html");
            Console.WriteLine(Texts.Texts.Count());
            var exemplars = new 
            {
                Armors = armorList.Armors,
                Texts = textList.Texts
            };
            var result = template.Substitute(exemplars);
            // result = result.Substitute(Armors);
            //result = result.Substitute(Texts);
            return result;
        }
    }
}
