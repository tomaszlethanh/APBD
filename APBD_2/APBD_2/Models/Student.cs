using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APBD_2.Models
{
    public class Student
    {
        [JsonPropertyName("indexNumber")]
        public string IndexNumber { get; set; }
        [JsonPropertyName("fname")]
        public string FirstName { get; set; }
        [JsonPropertyName("lname")]
        public string LastName { get; set; }
        [JsonPropertyName("birthdate")]
        public string BirthDate { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("mothersName")]
        public string MothersName { get; set; }
        [JsonPropertyName("fathersName")]
        public string FathersName { get; set; }
        [JsonPropertyName("studies")]
        public Kierunek Studies { get; set; }

       

        public override bool Equals(object obj)
        {
            Student student = obj as Student;

            return (this.IndexNumber.Equals(student.IndexNumber) &&
                this.FirstName.Equals(student.FirstName) &&
                this.LastName.Equals(student.LastName));
        }

        public override int GetHashCode()
        {
            return this.FirstName.GetHashCode() * 17 + this.LastName.GetHashCode()
                + this.IndexNumber.GetHashCode();
        }


    }
}
