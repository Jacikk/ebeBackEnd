using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EBE_Backend.Controllers
{
    class User_has_EventsController
    {
        public User_has_Events JsonToUser_has_Events(string jsonString)
        {
            User_has_Events user_has_Events = JsonSerializer.Deserialize<User_has_Events>(jsonString);

            return User_has_Events;
        }

        public string User_has_EventsToJson(User_has_Events User_has_Events)
        {
            string jsonString = JsonSerializer.Serialize(User_has_Events);
            Console.WriteLine(jsonString);

            return jsonString;
        }

        public string Create(string jsonString)
        {
            User_has_Events user_has_Events = JsonToUser_has_Events(jsonString);
            User_has_Events.Create();

            jsonString = JsonSerializer.Serialize(User_has_Events);

            return jsonString;
        }
        public string Show(int id)
        {
            User_has_Events user_has_Events = new User_has_Events();

            try
            {
                User_has_Events.GetById(id);


                if (User_has_Events != null)
                {
                    string jsonString = JsonSerializer.Serialize(User_has_Events);
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
            User_has_Events user_has_Events = JsonSerializer.Deserialize<User_has_Events>(jsonString);
            User_has_Events.Update();

            jsonString = JsonSerializer.Serialize(User_has_Events);

            return jsonString;
        }
        public string Index()
        {
            User_has_Events user_has_Events = new User_has_Events();
            ArrayList Indexlist = User_has_Events.ReadTable();

            string jsonString = JsonSerializer.Serialize(Indexlist);

            return jsonString;
        }
        public string Delete(int id)
        {
            User_has_Events user_has_Events = new User_has_Events();

            try
            {
                User_has_Events.GetById(id);
                if (User_has_Events == null)
                {
                    Error error = new Error("Nao encontrado");
                    string jsonString = JsonSerializer.Serialize(error);

                    return jsonString;
                }
                User_has_Events.Delete(id);

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
