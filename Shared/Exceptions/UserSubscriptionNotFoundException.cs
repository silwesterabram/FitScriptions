namespace Shared.Exceptions
{
    public class UserSubscriptionNotFoundException : Exception
    {
        public UserSubscriptionNotFoundException(string? message) : base(message)
        {
        }
    }
}
