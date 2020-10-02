using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EBE_Backend.Controllers
{
    class AddressController
    {
        public Address JsonToAddress(string jsonString)
        {
            Address address = JsonSerializer.Deserialize<Address>(jsonString);

            return Address;
        }

        public string AddressToJson(Address Address)
        {
            string jsonString = JsonSerializer.Serialize(Address);
            Console.WriteLine(jsonString);

            return jsonString;
        }

        public string Create(string jsonString)
        {
            Address address = JsonToAddress(jsonString);
            Address.Create();

            jsonString = JsonSerializer.Serialize(Address);

            return jsonString;
        }
        public string Show(int id)
        {
            Address address = new Address();

            try
            {
                Address.GetById(id);


                if (Address != null)
                {
                    string jsonString = JsonSerializer.Serialize(Address);
                    return jsonString;
                }
                else
                {
                    Error error = new Error("Nao encontrado");
                    string jsonString = JsonSerializer.Serialize(error);
                    return jsonString;
                }

            }
            catch (Exception ex)
            {
                Error error = new Error(ex.ToString());
                string jsonString = JsonSerializer.Serialize(error);

                return jsonString;
            }
        }

        public string Edit(string jsonString)
        {
            Address address = JsonSerializer.Deserialize<Address>(jsonString);
            Address.Update();

            jsonString = JsonSerializer.Serialize(Address);

            return jsonString;
        }
        public string Index()
        {
            Address address = new Address();
            ArrayList Indexlist = Address.ReadTable();

            string jsonString = JsonSerializer.Serialize(Indexlist);

            return jsonString;
        }
        public string Delete(int id)
        {
            Address Address = new Address();

            try
            {
                Address.GetById(id);
                if (Address == null)
                {
                    Error error = new Error("Nao encontrado");
                    string jsonString = JsonSerializer.Serialize(error);

                    return jsonString;
                }
                Address.Delete(id);

                return "Deletado com Sucesso";
            }
            catch (Exception ex)
            {
                Error error = new Error(ex.ToString());
                string jsonString = JsonSerializer.Serialize(error);

                return jsonString;
            }

        }
    }
}
