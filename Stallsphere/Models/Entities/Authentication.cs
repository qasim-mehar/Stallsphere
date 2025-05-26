using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Stallsphere.Models.Entities
{
    [Index(nameof(Email), IsUnique =true)]
    public class Authentication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        [MinLength(8,ErrorMessage ="Minimum 8 letters are required")]
        public string Password { get; set; }
    }
}
