using EBE_Backend.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EBE_Backend.Controllers
{
    class InstitutionController
    {
        private string jsonString;

        public Institution JsonToInstitution()
        {

            Institution institution = JsonSerializer.Deserialize<Institution>(jsonString);

            return institution;
        }

        public string InstitutionToJson(Institution institution)
        {
            jsonString = JsonSerializer.Serialize(institution);

            Console.WriteLine(jsonString);

            return jsonString;
        }
    }
}
