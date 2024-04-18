using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record SubscriptionTypeForCreationDto
    {
        public Guid GymId { get; init; }
        public string? Name { get; init; }
        public double Price { get; init; }
        public int ValidityInDays { get; init; }
        public int MaxAllowedTrainingSessions { get; init; }
        public int MaxAllowedDailyTrainingSessions { get; init; }
    }
}
