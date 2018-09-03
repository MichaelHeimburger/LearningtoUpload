using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageApp.Models;

namespace ImageApp.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Image ImageModel)
        {
            string filename = Path.GetFileNameWithoutExtension(ImageModel.ImageFile.FileName);
            string extension = Path.GetExtension(ImageModel.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            ImageModel.ImagePath = "~/Image/" + filename;
            filename = Path.Combine(Server.MapPath("~/Image/"), filename);
            ImageModel.ImageFile.SaveAs(filename);
            using (ImageMVCDVEntities1 db = new ImageMVCDVEntities1())
            {
                db.Images.Add(ImageModel);
                db.SaveChanges();
            }
            ModelState.Clear();
                return View();
        }

        [HttpGet]
        public ActionResult View(int id)
        {
            Image imageModel = new Image();
            using (ImageMVCDVEntities1 db = new ImageMVCDVEntities1())
            {
                imageModel = db.Images.Where(x => x.ImageID == id).FirstOrDefault();
            }
            return View(imageModel);
        }
    }
}