using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EBE_Backend.Controllers
{
    class Subevent_has_UserController
    {
        public Subevent_has_User JsonToSubevent_has_User(string jsonString)
        {
            Subevent_has_User subevent_has_User = JsonSerializer.Deserialize<Subevent_has_User>(jsonString);

            return Subevent_has_User;
        }

        public string Subevent_has_UserToJson(Subevent_has_User Subevent_has_User)
        {
            string jsonString = JsonSerializer.Serialize(Subevent_has_User);
            Console.WriteLine(jsonString);

            return jsonString;
        }

        public string Create(string jsonString)
        {
            Subevent_has_User subevent_has_User = JsonToSubevent_has_User(jsonString);
            Subevent_has_User.Create();

            jsonString = JsonSerializer.Serialize(Subevent_has_User);

            return jsonString;
        }
        public string Show(int id)
        {
            Subevent_has_User subevent_has_User = new Subevent_has_User();

            try
            {
                Subevent_has_User.GetById(id);


                if (Subevent_has_User != null)
                {
                    string jsonString = JsonSerializer.Serialize(Subevent_has_User);
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
            Subevent_has_User subevent_has_User = JsonSerializer.Deserialize<Subevent_has_User>(jsonString);
            Subevent_has_User.Update();

            jsonString = JsonSerializer.Serialize(Subevent_has_User);

            return jsonString;
        }
        public string Index()
        {
            Subevent_has_User subevent_has_User = new Subevent_has_User();
            ArrayList Indexlist = Subevent_has_User.ReadTable();

            string jsonString = JsonSerializer.Serialize(Indexlist);

            return jsonString;
        }
        public string Delete(int id)
        {
            Subevent_has_User subevent_has_User = new Subevent_has_User();

            try
            {
                Subevent_has_User.GetById(id);
                if (Subevent_has_User == null)
                {
                    Error error = new Error("Nao encontrado");
                    string jsonString = JsonSerializer.Serialize(error);

                    return jsonString;
                }
                Subevent_has_User.Delete(id);

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
