using DropzoneBasic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DropzoneBasic.Contollers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Index2()
        {
            return View();
        }


        public ActionResult SaveDropzoneJsUploadedFiles()
        {
            bool isSavedSuccessfully = false;
            string fName="";
            string uploadpath;
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];

                //fName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {
                    if (file != null)
                    {
                        string resimadi = System.IO.Path.GetFileName(file.FileName);
                        fName = resimadi;
                        //string adres = Server.MapPath("~/Content/img/"+resimadi);
                        string adres = System.IO.Path.Combine(Server.MapPath("~/MyImages/" + resimadi));
                        file.SaveAs(adres);

                       
                    }
                    //bool isExists = System.IO.Directory.Exists(pathString);
                    //if (!isExists) System.IO.Directory.CreateDirectory(pathString);
                    //uploadpath = string.Format("{0}\\{1}", pathString, file.FileName);
                    //file.SaveAs(uploadpath);
                }

                isSavedSuccessfully = true;
            }

            Product p = new Product();
            p.ID = 1;
            p.Name = "deneme";
            p.ImagePath =fName;

            return View("Index",p);

        }


        public ActionResult Upload()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        var path = Path.Combine(Server.MapPath("~/MyImages"));
                        string pathString = System.IO.Path.Combine(path.ToString());
                        var fileName1 = Path.GetFileName(file.FileName);
                        bool isExists = System.IO.Directory.Exists(pathString);
                        if (!isExists) System.IO.Directory.CreateDirectory(pathString);
                        var uploadpath = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(uploadpath);
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }
            if (isSavedSuccessfully)
            {
                return Json(new
                {
                    Message = fName
                });
            }
            else
            {
                return Json(new
                {
                    Message = "Error in saving file"
                });
            }
        }
    }
}