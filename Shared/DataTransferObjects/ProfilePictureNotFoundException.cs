namespace Shared.DataTransferObjects
{
    public class ProfilePictureNotFoundException : Exception
    {
        public ProfilePictureNotFoundException(string message) : base(message) { }
    }
}
