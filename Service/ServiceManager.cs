using Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IGymService> _gymService;
        private readonly Lazy<ISubscriptionTypeService> _subscriptionTypeService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
        {
            _gymService = new Lazy<IGymService>(() => new GymService(repositoryManager, loggerManager));
            _subscriptionTypeService = new Lazy<ISubscriptionTypeService>(() => new SubscriptionTypeService(repositoryManager, loggerManager));
        }

        public IGymService GymService => _gymService.Value;
        public ISubscriptionTypeService SubscriptionTypeService => _subscriptionTypeService.Value;
    }
}
