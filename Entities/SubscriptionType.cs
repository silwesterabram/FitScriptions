using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class SubscriptionType
    {
        [Column("SubscriptionTypeId")]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Gym))]
        public Guid GymId { get; set; }
        public Gym? Gym { get; set; }

        [Required(ErrorMessage = "Subscription type name is required")]
        [MaxLength(20, ErrorMessage = "Maximum length for the subscription type's name is 20 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Subscription type price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Subscription type validity in days is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Validity must be greater than 0")]
        public int ValidityInDays { get; set; }

        [Required(ErrorMessage = "Subscription type max allowed training sessions is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Max allowed training sessions must be greater than 0")]
        public int MaxAllowedTrainingSessions { get; set; }

        [Required(ErrorMessage = "Max allowed daily training session count is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Max allowed daily training sessions must be greater than 0")]
        public int MaxAllowedDailyTrainingSessions { get; set; }
    }
}
