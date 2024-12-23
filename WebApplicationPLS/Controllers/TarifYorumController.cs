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
    public class TarifYorumController : Controller
    {
        private YemekTarifleriDBContext db = new YemekTarifleriDBContext();

        // GET: TarifYorum
        public ActionResult Index()
        {
            var tarifYorum = db.TarifYorum.Include(t => t.Tarif);
            return View(tarifYorum.ToList());
        }

        // GET: TarifYorum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarifYorum tarifYorum = db.TarifYorum.Find(id);
            if (tarifYorum == null)
            {
                return HttpNotFound();
            }
            return View(tarifYorum);
        }

        // GET: TarifYorum/Create
        public ActionResult Create()
        {
            ViewBag.TarifID = new SelectList(db.Tarif, "TarifID", "Baslik");
            return View();
        }

        // POST: TarifYorum/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TarifYorumID,AdSoyad,Eposta,Icerik,Onay,YorumTarihi,TarifID")] TarifYorum tarifYorum)
        {
            if (ModelState.IsValid)
            {
                db.TarifYorum.Add(tarifYorum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TarifID = new SelectList(db.Tarif, "TarifID", "Baslik", tarifYorum.TarifID);
            return View(tarifYorum);
        }

        // GET: TarifYorum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarifYorum tarifYorum = db.TarifYorum.Find(id);
            if (tarifYorum == null)
            {
                return HttpNotFound();
            }
            ViewBag.TarifID = new SelectList(db.Tarif, "TarifID", "Baslik", tarifYorum.TarifID);
            return View(tarifYorum);
        }

        // POST: TarifYorum/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TarifYorumID,AdSoyad,Eposta,Icerik,Onay,YorumTarihi,TarifID")] TarifYorum tarifYorum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarifYorum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TarifID = new SelectList(db.Tarif, "TarifID", "Baslik", tarifYorum.TarifID);
            return View(tarifYorum);
        }

        // GET: TarifYorum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarifYorum tarifYorum = db.TarifYorum.Find(id);
            if (tarifYorum == null)
            {
                return HttpNotFound();
            }
            return View(tarifYorum);
        }

        // POST: TarifYorum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TarifYorum tarifYorum = db.TarifYorum.Find(id);
            db.TarifYorum.Remove(tarifYorum);
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
