using System.ComponentModel.DataAnnotations;

namespace RegisterAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Nombre { get; set; }
        
        [Required]
        public string? Correo { get; set; }
    }
}