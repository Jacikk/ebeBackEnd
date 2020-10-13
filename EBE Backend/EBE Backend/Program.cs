using EBE_Backend.Controllers;
using EBE_Backend.Classes;
using EBE_Backend.WebServer;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using EBE_Backend.Controller;
using System.Web;
using System.Net;

namespace EBE_Backend
{
    class Program
    {
        static void Main()
        {
            ConnectionHandler connectionHandler = new ConnectionHandler();
            var r = 0;
            while (r == 0)
            {
                try
                {
                    connectionHandler.Httplistener();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            
        }

    }   
}
