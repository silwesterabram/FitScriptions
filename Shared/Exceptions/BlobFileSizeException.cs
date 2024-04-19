namespace Shared.Exceptions
{
    public class BlobFileSizeException : Exception
    {
        public BlobFileSizeException(string message) : base(message)
        { }
    }
}
