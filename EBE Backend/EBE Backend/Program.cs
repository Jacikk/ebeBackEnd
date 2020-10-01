using EBE_Backend.Controllers;
using EBE_Backend.Classes;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EBE_Backend
{
    class Program
    {
        static void Main()
        {
            UserController UC = new UserController();
            User user = new User();

            //User user = new User(0, "Teste 14", true, "0014-12-12", "Cpf 14", "rg 14", 1, "teste 14", "1", "email 14", "Pw 14", "description 14", "Medic 14", "avatar 14", "refee 14", 14, 1); ;

            try
            {
                
                string json = UC.Show(14);

                Console.WriteLine(json);
                
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
              
            }
        }

 
    }
}
