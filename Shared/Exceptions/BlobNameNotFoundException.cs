namespace Shared.Exceptions
{
    public class BlobNameNotFoundException : Exception
    {
        public BlobNameNotFoundException(string message) : base(message)
        {
        }
    }
}
