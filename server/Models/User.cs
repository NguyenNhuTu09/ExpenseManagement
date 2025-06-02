using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    [Table("Users")]
    public class User{
        [Key]
        public int Id {get; set}

        [Required]
        [MaxLength(100)]
        public string userName {get; set}

        [Required]
        [EmailAddress]
        public string email {get; set}

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}