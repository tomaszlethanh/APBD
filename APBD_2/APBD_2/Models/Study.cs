using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APBD_2.Models
{
    public class Study
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("numberOfStudents")]
        public int NumOfStudents { get; set; }
    }
}
