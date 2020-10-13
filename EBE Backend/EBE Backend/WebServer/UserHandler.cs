using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using EBE_Backend.Controllers;
using EBE_Backend.Classes;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace EBE_Backend.WebServer
{
    class UserHandler
    {
        public string Handler(HttpListenerRequest request)
        {

            switch (request.HttpMethod)
            {
                case "GET":
                    return UserHandlerGet(request);
                case "POST":
                    return UserHandlerPost(request);
                case "PUT":
                    return UserHandlerPut(request);
                case "DELETE":
                    return UserHandlerDelete(request);
            }
            return "error";
        }
        string UserHandlerGet(HttpListenerRequest request)
        {
            UserController userController = new UserController();

            string[] StringRawUrl = request.RawUrl.Split('/');
            if (StringRawUrl.Length <= 2)
            {
                try
                {
                    return userController.Index();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return "Bad request";
                }
            }
            else
            {
                try
                {
                    StringRawUrl[2] = StringRawUrl[2].Remove(0, 4);

                    return userController.Show(Int32.Parse(StringRawUrl[2]));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return "erro";
                }
            }

        }
        string UserHandlerPost(HttpListenerRequest request)
        {
            string jsonString;
            try
            {

                using (var reader = new StreamReader(request.InputStream,
                                         request.ContentEncoding))
                {
                    jsonString = reader.ReadToEnd();
                }
                UserController userController = new UserController();
                return userController.Create(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "erro";
            }
        }

        string UserHandlerPut(HttpListenerRequest request)
        {
            string jsonString;
            try
            {

                using (var reader = new StreamReader(request.InputStream,
                                         request.ContentEncoding))
                {
                    jsonString = reader.ReadToEnd();
                }
                UserController userController = new UserController();
                return userController.Edit(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "erro";
            }
        }
        string UserHandlerDelete(HttpListenerRequest request)
        {
            UserController userController = new UserController();


            try
            {
                string[] StringRawUrl = request.RawUrl.Split('/');
                StringRawUrl[2] = StringRawUrl[2].Remove(0, 4);
                Console.WriteLine(StringRawUrl[2]);
                return userController.Delete(Int32.Parse(StringRawUrl[2]));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "Bad request";
            }
        }
    }
}
