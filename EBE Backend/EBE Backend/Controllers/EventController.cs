using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EBE_Backend.Controllers
{
    class EventController
    {
        public Event JsonToEvent(string jsonString)
        {
            Event Event = JsonSerializer.Deserialize<Event>(jsonString);

            return Event;
        }

        public string EventToJson(Event Event)
        {
            string jsonString = JsonSerializer.Serialize(Event);
            Console.WriteLine(jsonString);

            return jsonString;
        }

        public string Create(string jsonString)
        {
            Event Event = JsonToEvent(jsonString);
            Event.Create();

            jsonString = JsonSerializer.Serialize(Event);

            return jsonString;
        }
        public string Show(int id)
        {
            Event event = new Event();

            try
            {
                Event.GetById(id);


                if (Event != null)
                {
                    string jsonString = JsonSerializer.Serialize(Event);
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
            Event event = JsonSerializer.Deserialize<Event>(jsonString);
            Event.Update();

            jsonString = JsonSerializer.Serialize(Event);

            return jsonString;
        }
        public string Index()
        {
            Event event = new Event();
            ArrayList Indexlist = Event.ReadTable();

            string jsonString = JsonSerializer.Serialize(Indexlist);

            return jsonString;
        }
        public string Delete(int id)
        {
            Event event = new Event();

            try
            {
                Event.GetById(id);
                if (Event == null)
                {
                    Error error = new Error("Nao encontrado");
                    string jsonString = JsonSerializer.Serialize(error);

                    return jsonString;
                }
                Event.Delete(id);

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
