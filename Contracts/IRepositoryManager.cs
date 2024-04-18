namespace Contracts
{
    public interface IRepositoryManager
    {
        IGymRepository Gym { get; }
        ISubscriptionTypeRepository SubscriptionType { get; }
        void Save();
        Task SaveAsync();
    }
}
