using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using EBE_Backend.Controllers;
using EBE_Backend.Classes;

namespace EBE_Backend.WebServer
{
    class AddressHandler
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
            Console.WriteLine("Address Called");

            HttpListenerContext context = UserListener.GetContext();
            HttpListenerRequest request = context.Request;

            HttpListenerResponse response = context.Response;
            response.AddHeader("Access-Control-Allow-Origin", "*");
            response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            response.AddHeader("Access-Control-Allow-Headers", "X-Requested-With");
            response.AddHeader("Access-Control-Max-Age", "86400");

            Address address = new Address();
            UserController userController = new UserController();

            string responseString = userController.Show(15);

            byte[] buffer = Encoding.UTF8.GetBytes(responseString);

            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            output.Close();

            UserListener.Stop();
        }
    }
}

