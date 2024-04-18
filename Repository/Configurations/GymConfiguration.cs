using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class GymConfiguration : IEntityTypeConfiguration<Gym>
    {
        public void Configure(EntityTypeBuilder<Gym> builder)
        {
            builder.HasData(
                new Gym
                {
                    Id = Guid.NewGuid(),
                    Name = "Gold GYM",
                    Address = "1234 Gold St",
                    OpeningTime = TimeSpan.FromHours(6),
                    ClosingTime = TimeSpan.FromHours(23)
                },
                new Gym
                {
                    Id = Guid.NewGuid(),
                    Name = "Agora Fitness",
                    Address = "5678 Agora St",
                    OpeningTime = TimeSpan.FromHours(7),
                    ClosingTime = TimeSpan.FromHours(22)
                },
                new Gym
                {
                    Id = Guid.NewGuid(),
                    Name = "Planet Fitness",
                    Address = "91011 Planet St",
                    OpeningTime = TimeSpan.FromHours(5),
                    ClosingTime = TimeSpan.FromHours(23)
                }
            );
        }
    }
}
