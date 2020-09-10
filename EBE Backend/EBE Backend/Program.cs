using System;

namespace EBE_Backend
{
    class Program
    {
        static void Main()
        {
            Address ads = new Address(0, "cep5", "city5", "country5", "street5", "state5", "district5");
           
            ads.ReadTable();
        }
    }
}
