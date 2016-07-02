using System.Web;
using System.Web.Mvc;

namespace TelerikMvcApp1.Controllers
{
    public class FileBrowserController : FileManagerController
    {
        public FileBrowserController()
        {
            DefaultFilter = "*.txt,*.doc,*.docx,*.xls,*.xlsx,*.ppt,*.pptx,*.zip,*.rar,*.pdf";
            ContentPath = "/Uploads";
        }

        #region ActionResults

        [OutputCache(Duration = 3600, VaryByParam = "fileName;mode")]
        public ActionResult File(string fileName, int mode = 1)
        {
            var path = NormalizePath(fileName);

            if (AuthorizeFile(path))
            {
                var physicalPath = Server.MapPath(path);

                if (System.IO.File.Exists(physicalPath) && mode == 1)
                {
                    const string contentType = "application/octet-stream";
                    return File(System.IO.File.OpenRead(physicalPath), contentType, fileName);
                }

                return Content(path);
            }

            throw new HttpException(403, "Forbidden");
        }

        #endregion
    }
}