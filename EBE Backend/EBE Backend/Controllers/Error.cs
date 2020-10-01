using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EBE_Backend.Controllers
{
    class Error
    {
        private string error;

        public Error(string error)
        {
            this.error = error;
        }

        [JsonPropertyName("Erro")]
        public string Erro { get => error; set => error = value; }
    }
}
