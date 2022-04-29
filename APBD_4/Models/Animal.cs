using System.ComponentModel.DataAnnotations;

namespace APBD_4.Models
{
    public class Animal
    {
        
        public string IdAnimal { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Area { get; set; }
    }
}
