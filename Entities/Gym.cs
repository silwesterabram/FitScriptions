using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Gym
    {
        [Column("GymId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Gym name is required")]
        [MaxLength(60, ErrorMessage = "Maximum length for the gym's name is 60 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Gym address is required")]
        [MaxLength(100, ErrorMessage = "Maximum length for the gym's address is 100 characters.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Gym opening time is required")]
        public TimeSpan OpeningTime { get; set; }

        [Required(ErrorMessage = "Gym closing time is required")]
        public TimeSpan ClosingTime { get; set; }
    }
}
