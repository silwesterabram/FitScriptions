namespace Service.Contracts
{
    public interface IServiceManager
    {
        IGymService GymService { get; }
        ISubscriptionTypeService SubscriptionTypeService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}
