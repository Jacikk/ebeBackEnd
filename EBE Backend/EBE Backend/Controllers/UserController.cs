using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EBE_Backend.Controllers
{
    class UserController
    { 
        public User JsonToUser(string jsonString)
        {
            User user = JsonSerializer.Deserialize<User>(jsonString);

            return user;
        }

        public string UserToJson(User user)
        {
            string jsonString = JsonSerializer.Serialize(user);
            Console.WriteLine(jsonString);

            return jsonString;
        }
        
        public string Create(string jsonString)
        {
            User user = JsonToUser(jsonString);
            user.Create();

            jsonString = JsonSerializer.Serialize(user);

            return jsonString;
        }
        public string Show(int id)
        {
            User user = new User();

            try
            {
                user.GetById(id);


                if(user != null)
                {
                    string jsonString = JsonSerializer.Serialize(user);
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
            User user = JsonSerializer.Deserialize<User>(jsonString);
            user.Update();

            jsonString = JsonSerializer.Serialize(user);

            return jsonString;
        }
        public string Index()
        {
            User user = new User();
            ArrayList Indexlist =  user.ReadTable();

            string jsonString = JsonSerializer.Serialize(Indexlist);

            return jsonString;
        }
        public string Delete(int id)
        {
            User user = new User();

            try
            {
                user.GetById(id);
                if( user == null)
                {
                    Error error = new Error("Nao encontrado");
                    string jsonString = JsonSerializer.Serialize(error);

                    return jsonString;
                }
                user.Delete(id);

                return "Deletado com Sucesso";
            }
            catch(Exception ex)
            {
                Error error = new Error(ex.ToString());
                string jsonString = JsonSerializer.Serialize(error);

                return jsonString;
            }

        }
    }
}
