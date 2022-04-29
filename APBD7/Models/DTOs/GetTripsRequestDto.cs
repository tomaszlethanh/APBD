using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD7.Models.DTOs
{
    public class GetTripsRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int MaxPeople { get; set; }
       
    }
}
