namespace Shared.Exceptions
{
    public class QRCodeNotFoundException : Exception
    {
        public QRCodeNotFoundException(string? message) : base(message)
        {
        }
    }
}
