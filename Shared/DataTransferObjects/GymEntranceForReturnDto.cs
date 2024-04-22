namespace Shared.DataTransferObjects
{
    public record GymEntranceForReturnDto
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public Guid GymId { get; init; }
        public DateTime EntranceTime { get; set; }
        public string? BarcodeUrl { get; set; }
    }
}
