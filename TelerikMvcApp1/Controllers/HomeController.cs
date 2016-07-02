using System;
using System.Web.Mvc;
using TelerikMvcApp1.Models;

namespace TelerikMvcApp1.Controllers
{
    public class ResponseInfo : IDisposable
    {
        public string ResponseMessage { set; get; }

        #region Instance Dispose

        private bool disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!disposed)
            {
                if (disposing)
                {
                    ResponseMessage = null;
                }

                disposed = true;
            }
        }

        ~ResponseInfo()
        {
            Dispose(false);
        }

        #endregion
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            return View(new UploadObject());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult UpdateForm(UploadObject objUpload)
        {
            return Json(new ResponseInfo
            {
                ResponseMessage = $"FileUrl: {objUpload.FileUrl}<br/> ImageUrl: {objUpload.ImageUrl}"
            });
        }
    }
}
