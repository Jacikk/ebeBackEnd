using EBE_Backend.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections;

namespace EBE_Backend.Controllers
{
    class InstitutionController
    { 
        public Institution JsonToInstitution(string jsonString)
        {
            Institution institution = JsonSerializer.Deserialize<Institution>(jsonString);

            return institution;
        }

        public string UserToJson(Institution institution)
        {
            string jsonString = JsonSerializer.Serialize(institution);
            Console.WriteLine(jsonString);

            return jsonString;
        }

        public string Create(string jsonString)
        {
            Institution Institution = JsonToInstitution(jsonString);

            Institution.Create();

            jsonString = JsonSerializer.Serialize(Institution);

            return jsonString;
        }
        public string Show(int id)
        {
            Institution Institution = new Institution();

            try
            {
                Institution.GetById(id);


                if (Institution != null)
                {
                    string jsonString = JsonSerializer.Serialize(Institution);
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
            Institution Institution = JsonSerializer.Deserialize<Institution>(jsonString);
            Institution.Update();

            jsonString = JsonSerializer.Serialize(Institution);

            return jsonString;
        }
        public string Index()
        {
            Institution Institution = new Institution();
            ArrayList Indexlist = Institution.ReadTable();

            string jsonString = JsonSerializer.Serialize(Indexlist);

            return jsonString;
        }
        public string Delete(int id)
        {
            Institution Institution = new Institution();

            try
            {
                Institution.GetById(id);
                if (Institution == null)
                {
                    Error error = new Error("Nao encontrado");
                    string jsonString = JsonSerializer.Serialize(error);

                    return jsonString;
                }
                Institution.Id = id;
                Institution.Delete();

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
