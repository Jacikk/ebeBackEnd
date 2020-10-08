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
    class UserHandler
    {
        public void Handler(HttpListenerRequest request) 
        {
            switch (request.HttpMethod)
            {
                case "GET":

                    break;
                case "POST":

                    break;
                case "UPDATE":

                    break;
                case "DELETE":

                    break;
            }
        }
            
}
}
