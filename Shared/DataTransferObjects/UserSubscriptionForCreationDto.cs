namespace Shared.DataTransferObjects
{
    public record UserSubscriptionForCreationDto
    {
        public Guid UserId { get; init; }
        public Guid SubscriptionTypeId { get; init; }
        public DateTime PurchaseTime { get; set; }
    }
}
