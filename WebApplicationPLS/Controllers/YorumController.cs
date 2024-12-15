using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationPLS.Models.DataContext;
using WebApplicationPLS.Models.Model;

namespace WebApplicationPLS.Controllers
{
    public class YorumController : Controller
    {
        private YemekTarifleriDBContext db = new YemekTarifleriDBContext();

        // GET: Yorum
        public ActionResult Index()
        {
            var yorum = db.Yorum.Include(y => y.Blog).OrderByDescending(x=>x.BlogID);
            return View(yorum.ToList());
        }

        // GET: Yorum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = db.Yorum.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }

        // GET: Yorum/Create
        public ActionResult Create()
        {
            ViewBag.BlogID = new SelectList(db.Blog, "BlogID", "Baslik");
            return View();
        }

        // POST: Yorum/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "YorumID,AdSoyad,Eposta,Icerik,Onay,BlogID")] Yorum yorum)
        {
            if (ModelState.IsValid)
            {
                db.Yorum.Add(yorum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BlogID = new SelectList(db.Blog, "BlogID", "Baslik", yorum.BlogID);
            return View(yorum);
        }

        // GET: Yorum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = db.Yorum.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogID = new SelectList(db.Blog, "BlogID", "Baslik", yorum.BlogID);
            return View(yorum);
        }

        // POST: Yorum/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "YorumID,AdSoyad,Eposta,Icerik,Onay,BlogID")] Yorum yorum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yorum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogID = new SelectList(db.Blog, "BlogID", "Baslik", yorum.BlogID);
            return View(yorum);
        }

        // GET: Yorum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = db.Yorum.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }

        // POST: Yorum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Yorum yorum = db.Yorum.Find(id);
            db.Yorum.Remove(yorum);
            db.SaveChanges();
            return RedirectToAction("Index");
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
