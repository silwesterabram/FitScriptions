namespace Shared.DataTransferObjects
{
    public record GymForReturdDto
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public string? Address { get; init; }
        public TimeSpan OpeningTime { get; init; }
        public TimeSpan ClosingTime { get; init; }
    }
}
