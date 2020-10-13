using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EBE_Backend.Controllers
{
    class SubEventController
    {
        public SubEvent JsonToSubEvent(string jsonString)
        {
            SubEvent subEvent = JsonSerializer.Deserialize<SubEvent>(jsonString);

            return SubEvent;
        }

        public string SubEventToJson(SubEvent SubEvent)
        {
            string jsonString = JsonSerializer.Serialize(SubEvent);
            Console.WriteLine(jsonString);

            return jsonString;
        }

        public string Create(string jsonString)
        {
            SubEvent subEvent = JsonToSubEvent(jsonString);
            SubEvent.Create();

            jsonString = JsonSerializer.Serialize(SubEvent);

            return jsonString;
        }
        public string Show(int id)
        {
            SubEvent subEvent = new SubEvent();

            try
            {
                SubEvent.GetById(id);


                if (SubEvent != null)
                {
                    string jsonString = JsonSerializer.Serialize(SubEvent);
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
            SubEvent subEvent = JsonSerializer.Deserialize<SubEvent>(jsonString);
            SubEvent.Update();

            jsonString = JsonSerializer.Serialize(SubEvent);

            return jsonString;
        }
        public string Index()
        {
            SubEvent subEvent = new SubEvent();
            ArrayList Indexlist = SubEvent.ReadTable();

            string jsonString = JsonSerializer.Serialize(Indexlist);

            return jsonString;
        }
        public string Delete(int id)
        {
            SubEvent subEvent = new SubEvent();

            try
            {
                SubEvent.GetById(id);
                if (SubEvent == null)
                {
                    Error error = new Error("Nao encontrado");
                    string jsonString = JsonSerializer.Serialize(error);

                    return jsonString;
                }
                SubEvent.Delete(id);

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
