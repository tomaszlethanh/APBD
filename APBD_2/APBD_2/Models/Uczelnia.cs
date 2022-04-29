using APBD_2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APBD_2.Models
{
    public class Uczelnia
    {
        [JsonPropertyName("createdAt")]
        public string CreatedAt { get; set; }
        [JsonPropertyName("author")]
        public string Author { get; set; }
        [JsonPropertyName("studenci")]
        public HashSet<Student> Students { get; set; }
        [JsonPropertyName("activeStudies")]
        public HashSet<Study> Studies { get; set; }


        public Uczelnia(HashSet<Student> students)
        {
            Students = students;
            var grouped = students.GroupBy(student => student.Studies.Nazwa);
            Studies = new HashSet<Study>();
            foreach(var s in grouped)
            {
                Study stud = new Study
                {
                    NumOfStudents = s.Count(),
                    Name = s.Key
                };
                Studies.Add(stud);
            }

            Author = "Tomasz Le Thanh";
            CreatedAt = DateTime.Now.ToString("dd.MM.yyyy");
        }
    }

}
