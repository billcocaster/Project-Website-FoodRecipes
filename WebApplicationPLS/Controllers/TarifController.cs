using System;
using System.Collections.Generic;
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
    public class TarifController : Controller
    {
        private YemekTarifleriDBContext db = new YemekTarifleriDBContext();
        // GET: Tarif
        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return View(db.Tarif.Include("Liste").ToList().OrderByDescending(x => x.TarifID));

        }

        public ActionResult Create()
        {
            ViewBag.ListeID = new SelectList(db.Liste, "ListeID", "ListeAd");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tarif tarif, HttpPostedFileBase ResimURL)
        {
            if (ResimURL != null)
            {

                WebImage img = new WebImage(ResimURL.InputStream);
                FileInfo imginfo = new FileInfo(ResimURL.FileName);

                string tarifimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                img.Resize(1024, 768);
                img.Save("~/Uploads/Tarif/" + tarifimgname);

                tarif.ResimURL = "/Uploads/Tarif/" + tarifimgname;
            }
            db.Tarif.Add(tarif);
            db.SaveChanges();
            return Redirect("/Tarif/Index/");
        }
        [Route("yonetimpaneli/tarif/duzenle/{id:int}")]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var b = db.Tarif.Where(x => x.TarifID == id).SingleOrDefault();
            if (b == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListeID = new SelectList(db.Liste, "ListeID", "ListeAd", b.ListeID);
            return View(b);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Route("yonetimpaneli/tarif/duzenle/{id:int}")]

        public ActionResult Edit(int id, Tarif tarif, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                var b = db.Tarif.Where(x => x.TarifID == id).SingleOrDefault();
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(b.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(b.ResimURL));
                    }
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string tarifimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(1024, 768);
                    img.Save("~/Uploads/Tarif/" + tarifimgname);

                    b.ResimURL = "/Uploads/Tarif/" + tarifimgname;
                }
                b.Baslik = tarif.Baslik;
                b.Icerik = tarif.Icerik;
                b.ListeID = tarif.ListeID;
                db.SaveChanges();
                return Redirect("/Tarif/Index/");
            }
            return View(tarif);
        }
        [Route("yonetimpaneli/tarif/sil/{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarif tarif = db.Tarif.Find(id);
            if (tarif == null)
            {
                return HttpNotFound();
            }
            return View(tarif);
        }
        [HttpPost]
        [Route("yonetimpaneli/tarif/sil/{id:int}")]

        public ActionResult Delete(int id)
        {
            var b = db.Tarif.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            if (System.IO.File.Exists(Server.MapPath(b.ResimURL)))
            {
                System.IO.File.Delete(Server.MapPath(b.ResimURL));
            }
            db.Tarif.Remove(b);
            db.SaveChanges();

            return Redirect("/Tarif/Index/");
        }
    }
}