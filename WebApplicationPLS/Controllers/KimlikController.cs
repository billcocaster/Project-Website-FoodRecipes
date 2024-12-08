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
    public class KimlikController : Controller
    {
        YemekTarifleriDBContext db = new YemekTarifleriDBContext();
        // GET: Kimlik
        public ActionResult Index()
        {
            return View(db.Kimlik.ToList());
        }

        // GET: Kimlik/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Kimlik/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kimlik/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Kimlik/Edit/5
        public ActionResult Edit(int id)
        {
            var kimlik = db.Kimlik.Where(x=>x.KimlikID==id).SingleOrDefault();
            return View(kimlik);
        }

        // POST: Kimlik/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Kimlik kimlik,HttpPostedFileBase LogoURL)
        {
            if (ModelState.IsValid)
            {
                var k = db.Kimlik.Where(x => x.KimlikID == id).SingleOrDefault();

                if (LogoURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(k.LogoURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(k.LogoURL));
                    }
                    WebImage img = new WebImage(LogoURL.InputStream);
                    FileInfo imginfo = new FileInfo(LogoURL.FileName);

                    string logoname = LogoURL.FileName + imginfo.Extension;
                    img.Resize(200,200);
                    img.Save("~/Uploads/Kimlik/"+logoname);

                    k.LogoURL ="/Uploads/Kimlik/"+logoname;
                }
                k.Title =kimlik.Title;
                k.Keywords =kimlik.Keywords;
                k.Description =kimlik.Description;
                k.Unvan = kimlik.Unvan;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Kimlik/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Kimlik/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
