using System.ComponentModel.DataAnnotations;

namespace Medismart.Models.DataModels
{
    public class User
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string Address { get; set; } = string.Empty;
        [Required]
        public DateTime DateBirth { get; set; } = DateTime.Now;
    }
}
