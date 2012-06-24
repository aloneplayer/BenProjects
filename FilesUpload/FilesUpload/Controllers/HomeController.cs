using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilesUpload.Code;
using System.IO;

namespace FilesUpload.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase uploadFile)
        {
            if (uploadFile != null && uploadFile.ContentLength > 0)
            {
                string filePath = Path.Combine(Server.MapPath("~/App_Data/uploads"), Path.GetFileName(uploadFile.FileName));
                //uploadFile.SaveAs(filePath);
            }
            return RedirectToAction("Index");
        }

        public ActionResult MultiUpload()
        {
            return View();
        }


        [HttpPost]
        public ActionResult MultiUpload(IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file != null)
                    {
                        if (file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                            //file.SaveAs(path);
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult MultiSelectAndUpload()
        {
            return View();
        }

        public ActionResult MultiFileUploadWithMultiFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MultiFileUploadWithMultiFile(IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file != null)
                    {
                        if (file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                            //file.SaveAs(path);
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult MultiFileUploadWithUploadify()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MultiFileUploadWithUploadify(IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file != null)
                    {
                        if (file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                            //file.SaveAs(path);
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
