using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Collections;

namespace EBE_Backend.Controllers
{
    class AddressController
    {
        public Address JsonToAddress(string jsonString)
        {
            Address address = JsonSerializer.Deserialize<Address>(jsonString);

            return address;
        }

        public string UserToJson(Address address)
        {
            string jsonString = JsonSerializer.Serialize(address);
            Console.WriteLine(jsonString);

            return jsonString;
        }

        public string Create(string jsonString)
        {
            Address address = JsonToAddress(jsonString);

            address.Create();

            jsonString = JsonSerializer.Serialize(address);

            return jsonString;
        }
        public string Show(int id)
        {
            Address address = new Address();

            try
            {
                address.GetById(id);


                if (address != null)
                {
                    string jsonString = JsonSerializer.Serialize(address);
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
            address.Update();

            jsonString = JsonSerializer.Serialize(address);

            return jsonString;
        }
        public string Index()
        {
            Address address = new Address();
            ArrayList Indexlist = address.ReadTable();

            string jsonString = JsonSerializer.Serialize(Indexlist);

            return jsonString;
        }
        public string Delete(int id)
        {
            Address address = new Address();

            try
            {
                address.GetById(id);
                if (address == null)
                {
                    Error error = new Error("Nao encontrado");
                    string jsonString = JsonSerializer.Serialize(error);

                    return jsonString;
                }
                address.Id = id;
                address.Delete();

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
