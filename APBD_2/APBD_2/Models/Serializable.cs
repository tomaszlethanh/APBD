using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace APBD_2.Models
{
    public class Serializable
    {
        [JsonPropertyName("uczelnia")]
        public Uczelnia Uni { get; set; }

        public Serializable(Uczelnia uni)
        {
            Uni = uni;
        }
    }
}
