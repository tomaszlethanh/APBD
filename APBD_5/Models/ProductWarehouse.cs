using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_5.Models
{
    public class ProductWarehouse
    {
        [Required(ErrorMessage = "IdProduct is required.")]
        public int IdProduct { get; set; }

        [Required(ErrorMessage = "IdWarehouse is required.")]
        public int IdWarehouse { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be larger than 0.")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "DateTime is required.")]
        public DateTime CreatedAt { get; set; }
    }
}
