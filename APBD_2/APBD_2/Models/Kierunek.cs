using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APBD_2.Models
{
    public class Kierunek
    {
        [JsonPropertyName("name")]
        public String Nazwa { get; set; }
        [JsonPropertyName("mode")]
        public String Tryb { get; set; }

    }
}
