namespace Shared.Helpers
{
    public static class ImageUrlLogic
    {
        public static string ExtractFileNameFromImageUri(string profilePictureUri)
        {
            return profilePictureUri.Split("/").Last();
        }
    }
}
