using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class UserSubscription
    {
        [Column("UserSubscriptionId")]
        public Guid Id { get; set; }

        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey(nameof(SubscriptionType))]
        public Guid SubscriptionTypeId { get; set; }
        public SubscriptionType? SubscriptionType { get; set; }

        [Required(ErrorMessage = "Purchase time is required")]
        public DateTime PurchaseTime { get; set; }

        public string? BarcodeUrl { get; set; }

        [Required(ErrorMessage = "Training sessions done is required")]
        public int TrainingSessionsDone { get; set; }

        [Required(ErrorMessage = "Purchase price is required")]
        public double PurchasePrice { get; set; }

        [Required(ErrorMessage = "Subscription validity is required")]
        public bool IsActive { get; set; }

    }
}
