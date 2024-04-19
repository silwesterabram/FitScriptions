namespace Shared.DataTransferObjects
{
    public record ProfileForUpdateDto
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? UserName { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public string? Address { get; init; }
        public string? IdCardNumber { get; init; }
    }
}
