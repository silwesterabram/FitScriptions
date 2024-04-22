namespace Shared.DataTransferObjects
{
    public record GymEntraceForCreationDto
    {
        public Guid UserId { get; init; }
        public Guid UserSubscriptionId { get; init; }
        public Guid GymId { get; init; }
        public DateTime EntranceTime { get; init; }
    }
}
