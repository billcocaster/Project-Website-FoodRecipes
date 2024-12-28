using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog, HttpPostedFileBase ResimURL)
        {
            if (ResimURL != null)
            {
                var img = System.Drawing.Image.FromStream(ResimURL.InputStream);
                var resizedImg = new Bitmap(img, new Size(1024, 768));

                string blogimgname = Guid.NewGuid().ToString() + Path.GetExtension(ResimURL.FileName);
                string path = Server.MapPath("~/Uploads/Blog/" + blogimgname);

                resizedImg.Save(path, img.RawFormat);

                blog.ResimURL = "/Uploads/Blog/" + blogimgname;
            }
            db.Blog.Add(blog);
            db.SaveChanges();
            return Redirect("/Blog/Index/");
        }
        [Route("yonetimpaneli/blog/duzenle/{id:int}")]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var b = db.Blog.Where(x => x.BlogID == id).SingleOrDefault();
            if (b==null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriID = new SelectList(db.Kategori, "KategoriID", "KategoriAd", b.KategoriID);
            return View(b);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Route("yonetimpaneli/blog/duzenle/{id:int}")]

        public ActionResult Edit(int id, Blog blog, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                var b = db.Blog.Where(x => x.BlogID == id).SingleOrDefault();
                if (ResimURL != null)
                {
                    // Mevcut resmi sil
                    if (!string.IsNullOrEmpty(b.ResimURL) && System.IO.File.Exists(Server.MapPath(b.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(b.ResimURL));
                    }

                    // Yeni resmi işle ve kaydet
                    using (var img = System.Drawing.Image.FromStream(ResimURL.InputStream))
                    {
                        // Resmi yeniden boyutlandır
                        var resizedImg = new System.Drawing.Bitmap(img, new System.Drawing.Size(1024, 768));

                        // Resim dosya adını oluştur
                        string blogimgname = Guid.NewGuid().ToString() + Path.GetExtension(ResimURL.FileName);
                        string path = Server.MapPath("~/Uploads/Blog/" + blogimgname);

                        // Resmi kaydet
                        resizedImg.Save(path, img.RawFormat);

                        // Resim URL'sini ayarla
                        b.ResimURL = "/Uploads/Blog/" + blogimgname;
                    }
                }

                // Diğer alanları güncelle
                b.Baslik = blog.Baslik;
                b.Icerik = blog.Icerik;
                b.KategoriID = blog.KategoriID;
                db.SaveChanges();
                return Redirect("/Blog/Index/");

            }
            return View(blog);
        }
       
        [Route("yonetimpaneli/blog/sil/{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }
        [HttpPost]
        [Route("yonetimpaneli/blog/sil/{id:int}")]

        public ActionResult Delete(int id)
        {
            var b = db.Blog.Find(id);
            if(b == null)
            {
                return HttpNotFound();
            }
            if (System.IO.File.Exists(Server.MapPath(b.ResimURL)))
            {
                System.IO.File.Delete(Server.MapPath(b.ResimURL));
            }
            db.Blog.Remove(b);
            db.SaveChanges();

            return Redirect("/Blog/Index/");
        }
    }
}