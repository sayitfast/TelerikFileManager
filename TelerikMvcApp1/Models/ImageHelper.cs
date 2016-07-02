namespace TelerikMvcApp1.Models
{
    public class ImageHelper
    {
        public static bool IsImage(string imageUrl)
        {
            try
            {
                return !string.IsNullOrEmpty(imageUrl)
                       && (imageUrl.ToLower().EndsWith(".jpg")
                           || imageUrl.ToLower().EndsWith(".png")
                           || imageUrl.ToLower().EndsWith(".jpeg")
                           || imageUrl.ToLower().EndsWith(".gif"));
            }
            catch
            {
                return false;
            }
        }
    }
}