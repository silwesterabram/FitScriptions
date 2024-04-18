using Contracts;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IGymRepository> _gymRepository;
        private readonly Lazy<ISubscriptionTypeRepository> _subscriptionTypeRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _gymRepository = new Lazy<IGymRepository>(() => new GymRepository(_repositoryContext));
            _subscriptionTypeRepository = new Lazy<ISubscriptionTypeRepository>(() => new SubscriptionTypeRepository(_repositoryContext));
        }

        public IGymRepository Gym => _gymRepository.Value;
        public ISubscriptionTypeRepository SubscriptionType => _subscriptionTypeRepository.Value;

        public void Save() => _repositoryContext.SaveChanges();

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
