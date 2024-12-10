using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApplicationPLS.Models.DataContext;
using WebApplicationPLS.Models.Model;

namespace WebApplicationPLS.Controllers
{
    public class BlogController : Controller
    {
        private YemekTarifleriDBContext db = new YemekTarifleriDBContext();
        // GET: Blog
        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return View(db.Blog.Include("Kategori").ToList().OrderByDescending(x => x.BlogID));

        }

        public ActionResult Create()
        {
            ViewBag.KategoriID = new SelectList(db.Kategori, "KategoriID", "KategoriAd");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Blog blog, HttpPostedFileBase ResimURL)
        {
            if (ResimURL != null)
            {
                
                WebImage img = new WebImage(ResimURL.InputStream);
                FileInfo imginfo = new FileInfo(ResimURL.FileName);

                string blogimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                img.Resize(600, 400);
                img.Save("~/Uploads/Blog/" + blogimgname);

                blog.ResimURL = "/Uploads/Blog/" + blogimgname;
            }
            db.Blog.Add(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}