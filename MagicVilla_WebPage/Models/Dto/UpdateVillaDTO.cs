using System.ComponentModel.DataAnnotations;

namespace MagicVilla_WebPage.Models.Dto
{
    public class UpdateVillaDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]  
        public double Rate { get; set; }
        [Required]
        public int Ocuppancy { get; set; }
        [Required]
        public int Sqft { get; set; }
        [Required]
        public string ImageUrl { get; set; }    
        public string Amenity { get; set; } 


    }
}
