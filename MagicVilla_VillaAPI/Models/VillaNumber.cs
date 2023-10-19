using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_VillaAPI.Models
{
    public class VillaNumber
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VillaNo { get; set; }
        [ForeignKey("villa")]
        public int VillaID { get; set; }    
        public Villa villa { get; set; }
        public string SpecialsDetails { get; set; } 
        public DateTime? CreateDate { get; set; } 
        public DateTime UpdateDate { get; set; }    

    }
}
