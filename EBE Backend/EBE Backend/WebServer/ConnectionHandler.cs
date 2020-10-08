using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using EBE_Backend.Controllers;
using EBE_Backend.Classes;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

namespace EBE_Backend.WebServer
{
    public class ConnectionHandler
    {
        public void httplistener()
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Seu ambiente não suporta os recursos da classe HttpListener.");
                return;
            }
            HttpListener UserListener = new HttpListener();

            UserListener.Prefixes.Add("http://localhost:8080/");

            UserListener.Start();
            Console.WriteLine("UserListener Called");

            HttpListenerContext context = UserListener.GetContext();

            HttpListenerRequest request = context.Request;
            ShowRequestProperties1(request);

            switch(request.HttpMethod) {
                case "USUARIOS":

                    break;
                case "POST":

                    break;
                case "UPDATE":

                    break;
                case "DELETE":

                    break;
            }
            HttpListenerResponse response = context.Response;
            response.AddHeader("Access-Control-Allow-Origin", "*");
            response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            response.AddHeader("Access-Control-Allow-Headers", "X-Requested-With");
            response.AddHeader("Access-Control-Max-Age", "86400");

            Controllers.UserController userController = new Controllers.UserController();
            string responseString = userController.Show(15); 

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            
            output.Close();
            
            UserListener.Stop();
        }
        public static void ShowRequestProperties1(HttpListenerRequest request)
        {
            // Display the MIME types that can be used in the response.
            string[] types = request.AcceptTypes;
            if (types != null)
            {
                Console.WriteLine("Acceptable MIME types:");
                foreach (string s in types)
                {
                    Console.WriteLine(s);
                }
            }
            // Display the language preferences for the response.
            types = request.UserLanguages;
            if (types != null)
            {
                Console.WriteLine("Acceptable natural languages:");
                foreach (string l in types)
                {
                    Console.WriteLine(l);
                }
            }

            // Display the URL used by the client.
            Console.WriteLine("URL: {0}", request.Url.OriginalString);
            Console.WriteLine("Raw URL: {0}", request.RawUrl);
            Console.WriteLine("Query: {0}", request.QueryString);

            // Display the referring URI.
            Console.WriteLine("Referred by: {0}", request.UrlReferrer);

            //Display the HTTP method.
            Console.WriteLine("HTTP Method: {0}", request.HttpMethod);
            //Display the host information specified by the client;
            Console.WriteLine("Host name: {0}", request.UserHostName);
            Console.WriteLine("Host address: {0}", request.UserHostAddress);
            Console.WriteLine("User agent: {0}", request.UserAgent);
        }
    }
}

