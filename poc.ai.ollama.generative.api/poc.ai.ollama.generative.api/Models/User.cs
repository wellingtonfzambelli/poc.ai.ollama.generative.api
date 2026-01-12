using System.ComponentModel.DataAnnotations;

namespace poc.ai.ollama.generative.api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; }

        // Navigation
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
