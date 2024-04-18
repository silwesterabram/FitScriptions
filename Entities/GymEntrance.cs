using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class GymEntrance
    {
        [Column("GymEntranceId")]
        public Guid Id { get; set; }

        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey(nameof(Gym))]
        public Guid GymId { get; set; }
        public Gym? Gym { get; set; }

        [ForeignKey(nameof(UserSubscription))]
        public Guid UserSubscriptionId { get; set; }
        public UserSubscription? UserSubscription { get; set; }

        [Required(ErrorMessage = "Entrance time is required")]
        public DateTime EntranceTime { get; set; }

        public string? BarcodeUrl { get; set; }

    }
}
