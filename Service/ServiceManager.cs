using Contracts;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IGymService> _gymService;
        private readonly Lazy<ISubscriptionTypeService> _subscriptionTypeService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IProfileService> _profileService;
        private readonly Lazy<IUserSubscriptionService> _userSubscriptionService;

        public ServiceManager(
            IRepositoryManager repositoryManager, 
            ILoggerManager loggerManager,
            UserManager<User> userManager, 
            IConfiguration configuration)
        {
            _gymService = new Lazy<IGymService>(() => new GymService(repositoryManager, loggerManager));
            _subscriptionTypeService = new Lazy<ISubscriptionTypeService>(() => new SubscriptionTypeService(repositoryManager, loggerManager));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(loggerManager, userManager, configuration));
            _profileService = new Lazy<IProfileService>(() => new ProfileService(userManager));
            _userSubscriptionService = new Lazy<IUserSubscriptionService>(() => new UserSubscriptionService(repositoryManager, userManager));
        }

        public IGymService GymService => _gymService.Value;
        public ISubscriptionTypeService SubscriptionTypeService => _subscriptionTypeService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public IProfileService ProfileService => _profileService.Value;
        public IUserSubscriptionService UserSubscriptionService => _userSubscriptionService.Value;
    }
}
