using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EBE_Backend.Controllers
{
    class ImagesController
    {
        public Images JsonToImages(string jsonString)
        {
            Images images = JsonSerializer.Deserialize<Images>(jsonString);

            return Images;
        }

        public string ImagesToJson(Images Images)
        {
            string jsonString = JsonSerializer.Serialize(Images);
            Console.WriteLine(jsonString);

            return jsonString;
        }

        public string Create(string jsonString)
        {
            Images images = JsonToImages(jsonString);
            Images.Create();

            jsonString = JsonSerializer.Serialize(Images);

            return jsonString;
        }
        public string Show(int id)
        {
            Images images = new Images();

            try
            {
                Images.GetById(id);


                if (Images != null)
                {
                    string jsonString = JsonSerializer.Serialize(Images);
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
            Images images = JsonSerializer.Deserialize<Images>(jsonString);
            Images.Update();

            jsonString = JsonSerializer.Serialize(Images);

            return jsonString;
        }
        public string Index()
        {
            Images images = new Images();
            ArrayList Indexlist = Images.ReadTable();

            string jsonString = JsonSerializer.Serialize(Indexlist);

            return jsonString;
        }
        public string Delete(int id)
        {
            Images images = new Images();

            try
            {
                Images.GetById(id);
                if (Images == null)
                {
                    Error error = new Error("Nao encontrado");
                    string jsonString = JsonSerializer.Serialize(error);

                    return jsonString;
                }
                Images.Delete(id);

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
