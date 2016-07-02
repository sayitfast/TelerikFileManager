using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ThumbnailCreator = TelerikMvcApp1.Models.ThumbnailCreator;

namespace TelerikMvcApp1.Controllers
{
    public class ImageBrowserController : FileManagerController
    {
        private const int ThumbnailHeight = 80;
        private const int ThumbnailWidth = 80;

        private readonly ThumbnailCreator thumbnailCreator;

        public ImageBrowserController()
        {
            thumbnailCreator = new ThumbnailCreator();
            DefaultFilter = "*.png,*.gif,*.jpg,*.jpeg";
            ContentPath = "/Uploads/Images";
        }

        #region ActionResults

        [OutputCache(Duration = 3600, VaryByParam = "path")]
        public virtual ActionResult Thumbnail(string path)
        {
            path = NormalizePath(path);

            if (AuthorizeThumbnail(path))
            {
                var physicalPath = Server.MapPath(path);

                if (System.IO.File.Exists(physicalPath))
                {
                    Response.AddFileDependency(physicalPath);

                    return CreateThumbnail(physicalPath);
                }
                throw new HttpException(404, "File Not Found");
            }
            throw new HttpException(403, "Forbidden");
        }

        private FileContentResult CreateThumbnail(string physicalPath)
        {
            using (var fileStream = System.IO.File.OpenRead(physicalPath))
            {
                var desiredSize = new ImageSize
                {
                    Width = ThumbnailWidth,
                    Height = ThumbnailHeight
                };

                const string contentType = "image/png";

                return File(thumbnailCreator.Create(fileStream, desiredSize, contentType), contentType);
            }
        }

        [OutputCache(Duration = 360, VaryByParam = "path")]
        public ActionResult Image(string path)
        {
            path = NormalizePath(path);

            if (AuthorizeImage(path))
            {
                //var physicalPath = Server.MapPath(path);

                //if (System.IO.File.Exists(physicalPath))
                //{
                //    const string contentType = "image/png";
                //    return File(System.IO.File.OpenRead(physicalPath), contentType);
                //}

                return Content(path);
            }

            throw new HttpException(403, "Forbidden");
        }

        #endregion
    }
}