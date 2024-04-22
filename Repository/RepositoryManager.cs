using Contracts;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IGymRepository> _gymRepository;
        private readonly Lazy<ISubscriptionTypeRepository> _subscriptionTypeRepository;
        private readonly Lazy<IUserSubscriptionRepository> _userSubscriptionRepository;
        private readonly Lazy<IGymEntranceRepository> _gymEntranceRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _gymRepository = new Lazy<IGymRepository>(() => new GymRepository(_repositoryContext));
            _subscriptionTypeRepository = new Lazy<ISubscriptionTypeRepository>(() => new SubscriptionTypeRepository(_repositoryContext));
            _userSubscriptionRepository = new Lazy<IUserSubscriptionRepository>(() => new UserSubscriptionRepository(_repositoryContext));
            _gymEntranceRepository = new Lazy<IGymEntranceRepository>(() => new GymEntranceRepository(_repositoryContext));
        }

        public IGymRepository Gym => _gymRepository.Value;
        public ISubscriptionTypeRepository SubscriptionType => _subscriptionTypeRepository.Value;
        public IUserSubscriptionRepository UserSubscription => _userSubscriptionRepository.Value;
        public IGymEntranceRepository GymEntrance => _gymEntranceRepository.Value;

        public void Save() => _repositoryContext.SaveChanges();
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
