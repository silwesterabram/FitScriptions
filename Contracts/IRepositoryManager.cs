namespace Contracts
{
    public interface IRepositoryManager
    {
        IGymRepository Gym { get; }
        ISubscriptionTypeRepository SubscriptionType { get; }
        IUserSubscriptionRepository UserSubscription { get; }
        void Save();
        Task SaveAsync();
    }
}
