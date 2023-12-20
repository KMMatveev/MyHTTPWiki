using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHTTPServer.attributes
{
    public class ModelAttribute : Attribute, IHttpMethodAttribute
    {
        public string ModelName { get; }
        public ModelAttribute(string actionName) { ModelName = actionName; }
    }
}
