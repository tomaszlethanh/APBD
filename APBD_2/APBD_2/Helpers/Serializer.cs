using System;
using APBD_2.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace APBD_2.Helpers
{
    public static class Serializer
    {
        public static string SerializeToJson(Serializable conv)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };

            return JsonSerializer.Serialize(conv, options);
        }
    }
}
