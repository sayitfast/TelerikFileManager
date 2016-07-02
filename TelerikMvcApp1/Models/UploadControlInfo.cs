using System;

namespace TelerikMvcApp1.Models
{
    public enum FileBrowserType
    {
        Image,
        File
    }

    public class UploadControlInfo
    {
        public string Id { set; get; }

        public string UploadControlId { set; get; }

        public string FileUrl { set; get; }

        public FileBrowserType BrowserType { set; get; }

        public UploadControlInfo()
        {
            Id = Guid.NewGuid().ToString().Substring(0, 8);
            UploadControlId = string.Empty;
            BrowserType = FileBrowserType.Image;
            FileUrl = string.Empty;
        }
    }
}