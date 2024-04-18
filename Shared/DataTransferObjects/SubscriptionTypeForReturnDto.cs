namespace Shared.DataTransferObjects
{
    public record SubscriptionTypeForReturnDto
    {
        public Guid Id { get; init; }
        public Guid GymId { get; init; }
        public string? Name { get; init; }
        public double Price { get; init; }
        public int ValidityInDays { get; init; }
        public int MaxAllowedTrainingSessions { get; init; }
        public int MaxAllowedDailyTrainingSessions { get; init; }

    }
}
