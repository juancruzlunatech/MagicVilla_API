using System.ComponentModel.DataAnnotations;

namespace MagicVilla_WebPage.Models.Dto
{
    public class CreateVillaDTO
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string? Details { get; set; }
        [Required]  
        public double Rate { get; set; }    
        public int Ocuppancy { get; set; }  
        public int Sqft { get; set; } 
        public string? ImageUrl { get; set; }    
        public string? Amenity { get; set; } 


    }
}
