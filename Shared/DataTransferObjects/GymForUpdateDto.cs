namespace Shared.DataTransferObjects
{
    public record GymForUpdateDto
    {
        public string? Name { get; init; }
        public string? Address { get; init; }
        public string? OpeningTime { get; init; }
        public string? ClosingTime { get; init; }
    }
}
