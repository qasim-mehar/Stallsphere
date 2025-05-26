using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Stallsphere.Models.Entities
{
    public class Stall
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Ensures auto-increment
        public int StallId { get; set; }  // Primary Key, auto-incremented

        public String StallNo { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string BestFor { get; set; }
        public string LocationArea { get; set; }
        public string ImgSrc { get; set; }
        public float Area { get; set; }
        public string Status { get; set; } // ✅ Added Status property
    }
}
