namespace Shared.DataTransferObjects
{
    public record SubscriptionTypeForUpdateDto
    {
        public string? Name { get; init; }
        public double Price { get; init; }
        public int ValidityInDays { get; init; }
        public int MaxAllowedTrainingSessions { get; init; }
        public int MaxAllowedDailyTrainingSessions { get; init; }
    }
}
