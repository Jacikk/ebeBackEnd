using System;

using EBE_Backend.Classes;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EBE_Backend.Controllers
{
    class Institution_has_UserController
    {
        public Institution_has_User JsonToInstitution_has_User(string jsonString)
        {
            Institution_has_User Institution_has_User = JsonSerializer.Deserialize<Institution_has_User>(jsonString);

            return Institution_has_User;
        }

        public string Institution_has_UserToJson(Institution_has_User Institution_has_User)
        {
            string jsonString = JsonSerializer.Serialize(Institution_has_User);
            Console.WriteLine(jsonString);

            return jsonString;
        }

        public string Create(string jsonString)
        {
            Institution_has_User institution_has_User = JsonToInstitution_has_User(jsonString);
            Institution_has_User.Create();

            jsonString = JsonSerializer.Serialize(Institution_has_User);

            return jsonString;
        }
        public string Show(int id)
        {
            Institution_has_User institution_has_User = new Institution_has_User();

            try
            {
                Institution_has_User.GetById(id);


                if (Institution_has_User != null)
                {
                    string jsonString = JsonSerializer.Serialize(Institution_has_User);
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
            Institution_has_User institution_has_User = JsonSerializer.Deserialize<Institution_has_User>(jsonString);
            Institution_has_User.Update();

            jsonString = JsonSerializer.Serialize(Institution_has_User);

            return jsonString;
        }
        public string Index()
        {
            Institution_has_User institution_has_User = new Institution_has_User();
            ArrayList Indexlist = Institution_has_User.ReadTable();

            string jsonString = JsonSerializer.Serialize(Indexlist);

            return jsonString;
        }
        public string Delete(int id)
        {
            Institution_has_User institution_has_User = new Institution_has_User();

            try
            {
                Institution_has_User.GetById(id);
                if (Institution_has_User == null)
                {
                    Error error = new Error("Nao encontrado");
                    string jsonString = JsonSerializer.Serialize(error);

                    return jsonString;
                }
                Institution_has_User.Delete(id);

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
