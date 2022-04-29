using APBD_08.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_08.DTOs
{
    public class GetPrescriptionRequestDto
    {
        public int IdPrescription { get; set; }
        public string PatientName { get; set; }
        public string PatientLastName { get; set; }
        public DateTime PatientBirthDate { get; set; }
        public string DoctorName { get; set; }
        public string DoctorLastName { get; set; }
        public virtual ICollection<string> Medicaments { get; set; }
    }
}
