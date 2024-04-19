namespace Shared.Helpers
{
    public static class ImageUrlLogic
    {
        public static string ExtractFileNameFromProfilePictureUri(string profilePictureUri)
        {
            return profilePictureUri.Split("/").Last();
        }
    }
}
