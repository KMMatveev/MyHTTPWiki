using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MyHTTPServer.config;
using MyHTTPServer.attributes;
using MyHTTPServer.controllers;
using MyHTTPServer.models;

namespace MyHTTPServer.handlers
{
    internal class ControllerHandler:Handler
    {
        private Assembly? _controllerAssembly;
        
        async public override void HandleRequest(HttpListenerContext context)
        {
            try
            {
                var strParams = context.Request.Url!
                    .Segments
                    .Skip(1)
                    .Select(s => s.Replace("/", ""))
                    .ToArray();
                //var strParams=context.Request.RawUrl.ToString().Replace("?","/").Split("/").Skip(1).ToArray();
                var controllerName = strParams[0];
                var methodName = strParams[1];

                Console.WriteLine($"{controllerName},{methodName}");
                string objName = null;
                if (strParams.Length > 2)
                    objName = strParams[2];
                Console.WriteLine($"{controllerName},{methodName},{objName}");
                _controllerAssembly = Assembly.GetEntryAssembly();

                var controller = _controllerAssembly!.GetTypes()
                    .Where(t => Attribute.IsDefined(t, typeof(HttpController)))
                    .FirstOrDefault(c => (((HttpController)Attribute.GetCustomAttribute(c, typeof(HttpController))!)!)
                        .ControllerName.Equals(controllerName + "controller", StringComparison.OrdinalIgnoreCase));

                if (controller == null) { Console.WriteLine("null controller"); throw new ArgumentException("null controller"); }//Successor.HandleRequest(context);//throw new ArgumentException("null controller");


                var method = controller.GetMethods()
                    .Where(x => x.GetCustomAttributes(true)
                        .Any(attr => attr.GetType().Name.Equals($"{context.Request.HttpMethod}Attribute",
                            StringComparison.OrdinalIgnoreCase)))
                    .FirstOrDefault(m => m.Name.Equals(methodName, StringComparison.OrdinalIgnoreCase));
                if (controllerName == "page")
                {
                    method = controller.GetMethod($"{methodName}Page");
                }
                if (method == null) { Console.WriteLine("null method"); throw new ArgumentException("null method"); }
                string[] strPar;
                if (context.Request.HttpMethod == "post"|| context.Request.HttpMethod == "Post"|| context.Request.HttpMethod == "POST")
                    strPar = ParseRequest(context.Request).Result;
                else
                { 
                    strPar = context.Request.RawUrl.Split("?"); 
                    //Console.WriteLine(strPar[1]);
                }
                
                var queryParams = Array.Empty<object>();
                var objAssembly = Assembly.GetEntryAssembly();

                if (strPar.Length > 0)
                {
                    
                    if(controllerName=="ORM")
                    {
                        Console.WriteLine("OrmStart");
                        var obj = objAssembly!.GetTypes()
                        .Where(t => Attribute.IsDefined(t, typeof(ModelAttribute)))
                        .FirstOrDefault(c => (((ModelAttribute)Attribute.GetCustomAttribute(c, typeof(ModelAttribute))!)!)
                        .ModelName.Equals(objName, StringComparison.OrdinalIgnoreCase));
                        Console.WriteLine(obj.GetType());
                        var objConstructor = obj.GetConstructors()[0];
                        Console.WriteLine(objConstructor);
                        Console.WriteLine("OrmStart");
                        var sParams = new string[strPar.Length];
                        var paramArray = new object[1];
                        for (int i=0;i<sParams.Length;i++)
                        {
                            sParams[i] = string.Join("=",strPar[i].Split("=").Skip(1));
                        }
                        foreach (var c in sParams){ Console.WriteLine(c); }
                        if(!methodName.Contains("Delete")&& !methodName.Contains("SelectByID")) 
                        { 
                            queryParams = objConstructor?.GetParameters()
                            .Select((p, i) =>
                            {
                                Console.WriteLine($"{i}: {p.ParameterType} ({sParams[i].GetType()})");
                                return Convert.ChangeType(sParams[i], p.ParameterType);
                            })
                            .ToArray();
                            var answer = objConstructor.Invoke(queryParams);
                            Console.WriteLine(answer);
                            paramArray[0] = answer;
                        }
                        else
                        {
                            var answer = int.Parse(sParams[0]);
                            Console.WriteLine(answer);
                            paramArray[0] = answer;
                        }
                        queryParams = paramArray;
                    }
                    else if(controllerName == "Authorization")
                    {
                        var paramArray = new object[strPar.Length];
                        //if (methodName== "Registration")
                        //{
                        //    for(int i=0;i<strPar.Length;i++)
                        //    {
                        //        paramArray[i]= strPar[i].ToString();
                        //    }
                        //}
                        //else
                        {
                            paramArray = strPar;
                        }
                        queryParams = paramArray;
                    }
                    else if(controllerName=="page")
                    {
                        var id = int.Parse(strPar[1].Split("=")[1]);
                        int answer = id;
                        var paramArray = new object[1];
                        paramArray[0] = answer;
                        queryParams = paramArray;
                        Console.WriteLine(answer);
                    }
                    else
                    {
                        Console.WriteLine("Unknown controller");throw new ArgumentException("Unknown controller");
                    }

                }
                Console.WriteLine("Tryind to invoke method");
                var ret = method.Invoke(Activator.CreateInstance(controller), queryParams);
                byte[]? responseBuffer = Array.Empty<byte>();
                //Type returnType = method.ReturnType;
                //var ret= Activator.CreateInstance(returnType);
                //byte[]? responseBuffer = Array.Empty<byte>();
                //if (controllerName == "ORM") {
                //    ret = method.Invoke(Activator.CreateInstance(controller), queryParams);
                //}
                //else if (controllerName =="page")
                //{
                //    ret =method.Invoke(null, queryParams);
                //}
                //else
                //{
                //    ret = null;
                //}
                Console.WriteLine("method returned");
                if (ret is string)
                    responseBuffer = Encoding.UTF8.GetBytes((ret as string)!);
                else if( ret is Cookie)
                {
                    context.Response.Cookies.Add(ret as Cookie);
                }
                else if (!(ret is null))
                {
                    var serializeObj = JsonSerializer.Serialize(ret);
                    responseBuffer = Encoding.UTF8.GetBytes(serializeObj);
                }

                var response = context.Response;

                if (responseBuffer.Length > 0)
                {
                    response.ContentLength64 = responseBuffer.Length;
                    using Stream output = response.OutputStream;
                    await output.WriteAsync(responseBuffer);
                    await output.FlushAsync();
                    output.Close();
                }

                if (context.Request.HttpMethod.Equals("post", StringComparison.OrdinalIgnoreCase))
                {
                    var _serverConfig = HttpServer.GetAppSettings();
                    response.Redirect($"{_serverConfig.Address}:{_serverConfig.Port}/");
                }
                response.Close();
            }
            catch { Successor.HandleRequest(context); }
        }

        async private Task<string[]> ParseRequest(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
                return Array.Empty<string>();

            var stream = new StreamReader(request.InputStream);
            var requestData = await stream.ReadToEndAsync();
            requestData = Uri.UnescapeDataString(Regex.Unescape(requestData));
            requestData = requestData.Replace("&", "\n");
            //requestData = requestData.Replace("==", "$% ");
            requestData = requestData.Replace("+", " ");
            var array = requestData.Split('\n').ToArray();
            array.Select(i=> i.TrimStart('0').TrimStart('='));
            //var classData = array.Where(val => Array.IndexOf(array, val) % 2 == 1).ToArray();
            return array;//classData;
        }
    }
}
