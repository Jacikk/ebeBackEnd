using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EBE_Backend.Controllers
{
    class PalestrantesController
    {
        public Palestrantes JsonToPalestrantes(string jsonString)
        {
            Palestrantes palestrantes = JsonSerializer.Deserialize<Palestrantes>(jsonString);

            return Palestrantes;
        }

        public string PalestrantesToJson(Palestrantes Palestrantes)
        {
            string jsonString = JsonSerializer.Serialize(Palestrantes);
            Console.WriteLine(jsonString);

            return jsonString;
        }

        public string Create(string jsonString)
        {
            Palestrantes palestrantes = JsonToPalestrantes(jsonString);
            Palestrantes.Create();

            jsonString = JsonSerializer.Serialize(Palestrantes);

            return jsonString;
        }
        public string Show(int id)
        {
            Palestrantes palestrantes = new Palestrantes();

            try
            {
                Palestrantes.GetById(id);


                if (Palestrantes != null)
                {
                    string jsonString = JsonSerializer.Serialize(Palestrantes);
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
            Palestrantes palestrantes = JsonSerializer.Deserialize<Palestrantes>(jsonString);
            Palestrantes.Update();

            jsonString = JsonSerializer.Serialize(Palestrantes);

            return jsonString;
        }
        public string Index()
        {
            Palestrantes palestrantes = new Palestrantes();
            ArrayList Indexlist = Palestrantes.ReadTable();

            string jsonString = JsonSerializer.Serialize(Indexlist);

            return jsonString;
        }
        public string Delete(int id)
        {
            Palestrantes palestrantes = new Palestrantes();

            try
            {
                Palestrantes.GetById(id);
                if (Palestrantes == null)
                {
                    Error error = new Error("Nao encontrado");
                    string jsonString = JsonSerializer.Serialize(error);

                    return jsonString;
                }
                Palestrantes.Delete(id);

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
