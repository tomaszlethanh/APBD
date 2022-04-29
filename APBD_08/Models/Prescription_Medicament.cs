using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_08.Models
{
    public class Prescription_Medicament
    {
        public int IdMedicament { get; set; }
        public virtual Medicament Medicament { get; set; }
        public int IdPrescription { get; set; }
        public virtual Prescription Prescription { get; set; }
        public int? Dose { get; set; }
        public string Details { get; set; }
        
    } 
}
