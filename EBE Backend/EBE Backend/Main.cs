using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using EBE_Backend.Classes;
using MySql.Data.MySqlClient;


namespace EBE_Backend
{
    class MainClass
    {
        static void Main()
        {
            Address ads = new Address(1, "1", "1", "1", "1", "1", "1");
            ads.ReadTable();
        }
    }

}