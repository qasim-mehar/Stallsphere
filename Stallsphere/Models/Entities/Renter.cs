
using System.ComponentModel.DataAnnotations;

namespace Stallsphere.Models.Entities
{
    public class Renter
    {
        [Key]
        public int RenterId { get; set; } // Primary Key, auto-incremented

        [Required]
        [StringLength(100)]
        public string Name { get; set; } // Full Name of the Renter

        [Required]
        [Phone]
        public string ContactNo { get; set; } // Contact Number

        [Required]
        public DateTime StartingDate { get; set; } // Starting Date for the Rent

        [Required]
        public DateTime EndingDate { get; set; } // Ending Date for the Rent

        [Required]
        [StringLength(15)]
        public string CNIC { get; set; } // CNIC of the Renter

        [Required]
        [StringLength(250)]
        public string Address { get; set; } // Address of the Renter

        [Required]
        [StringLength(150)]
        public string BusinessName { get; set; } // Business Name related to the stall
        public int StallId { get; set; }

    }
}
