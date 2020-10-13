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
        private string responseString;
        public void Httplistener()
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Seu ambiente não suporta os recursos da classe HttpListener.");
                return;
            }
            HttpListener UserListener = new HttpListener();

            UserListener.Prefixes.Add("http://localhost:8080/");

            UserListener.Start();
            Console.WriteLine("UserListener Started");

            HttpListenerContext context = UserListener.GetContext();

            HttpListenerRequest request = context.Request;
            ShowRequestProperties1(request);

            string[] StringRawUrl = request.RawUrl.Split('/');
            foreach (string item in StringRawUrl)
            {
                Console.WriteLine(item);
            }
            
            UserHandler userHandler = new UserHandler();

            switch (StringRawUrl[1]) {
                case "Usuarios":
                    responseString = userHandler.Handler(request);
                    break;
                case "Address":

                    break;
                case "Institution":

                    break;
                default:
                    responseString = "Bad Request";
                    break;

            }

            HttpListenerResponse response = context.Response;
            response.AddHeader("Access-Control-Allow-Origin", "*");
            response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            response.AddHeader("Access-Control-Allow-Headers", "X-Requested-With");
            response.AddHeader("Access-Control-Max-Age", "86400");

            //byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            byte[] buffer = Encoding.ASCII.GetBytes(responseString);
            Console.WriteLine(responseString);
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
            UserListener.Stop();
            
        }
        public static void ShowRequestProperties1(HttpListenerRequest request)
        {
            // Display the MIME types that can be used in the response.
            /*string[] types = request.AcceptTypes;
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
            }*/

            // Display the URL used by the client.
            Console.WriteLine("URL: {0}", request.Url.Query);
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
            Console.WriteLine();
        }
    }
}

