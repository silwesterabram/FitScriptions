namespace Shared.DataTransferObjects
{
    public record UserSubscriptionForReturnDto
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public Guid SubscriptionTypeId { get; init; }
        public DateTime PurchaseTime { get; set; }
        public string? BarcodeUrl { get; set; }
        public int TrainingSessionsDone { get; set; }
        public double PurchasePrice { get; set; }
        public bool IsActive { get; set; }
    }
}
