using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EBE_Backend.Controllers
{
    class ManagerController
    {
        public Manager JsonToManager(string jsonString)
        {
            Manager manager = JsonSerializer.Deserialize<Manager>(jsonString);

            return Manager;
        }

        public string ManagerToJson(Manager Manager)
        {
            string jsonString = JsonSerializer.Serialize(Manager);
            Console.WriteLine(jsonString);

            return jsonString;
        }

        public string Create(string jsonString)
        {
            Manager manager = JsonToManager(jsonString);
            Manager.Create();

            jsonString = JsonSerializer.Serialize(Manager);

            return jsonString;
        }
        public string Show(int id)
        {
            Manager manager = new Manager();

            try
            {
                Manager.GetById(id);


                if (Manager != null)
                {
                    string jsonString = JsonSerializer.Serialize(Manager);
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
            Manager manager = JsonSerializer.Deserialize<Manager>(jsonString);
            Manager.Update();

            jsonString = JsonSerializer.Serialize(Manager);

            return jsonString;
        }
        public string Index()
        {
            Manager manager = new Manager();
            ArrayList Indexlist = Manager.ReadTable();

            string jsonString = JsonSerializer.Serialize(Indexlist);

            return jsonString;
        }
        public string Delete(int id)
        {
            Manager manager = new Manager();

            try
            {
                Manager.GetById(id);
                if (Manager == null)
                {
                    Error error = new Error("Nao encontrado");
                    string jsonString = JsonSerializer.Serialize(error);

                    return jsonString;
                }
                Manager.Delete(id);

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
