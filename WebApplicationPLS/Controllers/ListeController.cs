using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class ListeController : Controller
    {
        private YemekTarifleriDBContext db = new YemekTarifleriDBContext();

        // GET: Liste
        public ActionResult Index()
        {
            return View(db.Liste.ToList());
        }

        // GET: Liste/Details/5
        [Route("yonetimpaneli/liste/detaylar/{id:int}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liste liste = db.Liste.Find(id);
            if (liste == null)
            {
                return HttpNotFound();
            }
            return View(liste);
        }

        // GET: Liste/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Liste/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Liste liste, HttpPostedFileBase ResimURL)
        {
            if (ResimURL != null)
            {
                var img = System.Drawing.Image.FromStream(ResimURL.InputStream);
                var resizedImg = new Bitmap(img, new Size(1024, 768));

                string listeimgname = Guid.NewGuid().ToString() + Path.GetExtension(ResimURL.FileName);
                string path = Server.MapPath("~/Uploads/Liste/" + listeimgname);

                resizedImg.Save(path, img.RawFormat);

                liste.ResimURL = "/Uploads/Liste/" + listeimgname;
            }
            db.Liste.Add(liste);
            db.SaveChanges();
            return Redirect("/Liste/Index/");

            //if (ResimURL != null)
            //{

            //    WebImage img = new WebImage(ResimURL.InputStream);
            //    FileInfo imginfo = new FileInfo(ResimURL.FileName);

            //    string listeimgname = Guid.NewGuid().ToString() + imginfo.Extension;
            //    img.Resize(1024, 768);
            //    img.Save("~/Uploads/Liste/" + listeimgname);


            //    liste.ResimURL = "/Uploads/Liste/"+listeimgname;
            //}
            //db.Liste.Add(liste);
            //db.SaveChanges();
            //return Redirect("/Liste/Index/");
        }

        // GET: Liste/Edit/5
        [Route("yonetimpaneli/liste/duzenle/{id:int}")]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liste liste = db.Liste.Find(id);
            if (liste == null)
            {
                return HttpNotFound();
            }
            return View(liste);
        }

        // POST: Liste/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Route("yonetimpaneli/liste/duzenle/{id:int}")]
        public ActionResult Edit(int id, Liste liste, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                var b = db.Liste.Where(x => x.ListeID == id).SingleOrDefault();
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
                        string listeimgname = Guid.NewGuid().ToString() + Path.GetExtension(ResimURL.FileName);
                        string path = Server.MapPath("~/Uploads/Liste/" + listeimgname);

                        // Resmi kaydet
                        resizedImg.Save(path, img.RawFormat);

                        // Resim URL'sini ayarla
                        b.ResimURL = "/Uploads/Liste/" + listeimgname;
                    }
                }

                // Diğer alanları güncelle
                b.ListeAd = liste.ListeAd;
                b.Aciklama = liste.Aciklama;

                db.SaveChanges();
                return Redirect("/Liste/Index/");
            }
            return View(liste);
        }

        //public ActionResult Edit(int id, Liste liste, HttpPostedFileBase ResimURL)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var b = db.Liste.Where(x => x.ListeID == id).SingleOrDefault();
        //        if (ResimURL != null)
        //        {
        //            // Mevcut resmi sil
        //            if (!string.IsNullOrEmpty(b.ResimURL) && System.IO.File.Exists(Server.MapPath(b.ResimURL)))
        //            {
        //                System.IO.File.Delete(Server.MapPath(b.ResimURL));
        //            }

        //            // Yeni resmi kaydet
        //            WebImage img = new WebImage(ResimURL.InputStream);
        //            FileInfo imginfo = new FileInfo(ResimURL.FileName);

        //            string listeimgname = Guid.NewGuid().ToString() + imginfo.Extension;
        //            img.Resize(1024, 768);
        //            img.Save(Server.MapPath("~/Uploads/Liste/" + listeimgname));

        //            // Resim URL'sini ayarla
        //            b.ResimURL = "/Uploads/Liste/" + listeimgname;
        //        }

        //        // Diğer alanları güncelle
        //        b.ListeAd = liste.ListeAd;
        //        b.Aciklama = liste.Aciklama;

        //        db.SaveChanges();
        //        return Redirect("/Liste/Index/");
        //    }
        //    return View(liste);
        //}

        // GET: Liste/Delete/5
        [Route("yonetimpaneli/liste/sil/{id:int}")]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liste liste = db.Liste.Find(id);
            if (liste == null)
            {
                return HttpNotFound();
            }
            return View(liste);
        }

        // POST: Liste/Delete/5
        [HttpPost]
        [Route("yonetimpaneli/liste/sil/{id:int}")]

        public ActionResult DeleteConfirmed(int id)
        {
            var b = db.Liste.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            if (System.IO.File.Exists(Server.MapPath(b.ResimURL)))
            {
                System.IO.File.Delete(Server.MapPath(b.ResimURL));
            }
            db.Liste.Remove(b);
            db.SaveChanges();

            return Redirect("/Liste/Index/");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
