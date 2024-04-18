using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configurations;

namespace Repository
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new GymConfiguration());
        }

        public DbSet<Gym> Gyms { get; set; }
        public DbSet<GymEntrance> GymEntrances { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }
    }
}
