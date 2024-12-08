using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using WebApplicationPLS.Models.DataContext;
using WebApplicationPLS.Models.Model;

namespace WebApplicationPLS.Controllers
{
    public class HakkimizdaController : Controller
    {
        YemekTarifleriDBContext db = new YemekTarifleriDBContext();
        // GET: Hakkimizda
        public ActionResult Index()
        {
            return View(db.Hakkimizda.ToList());
        }
        public ActionResult Edit(int id)
        {
            var hakkimizda = db.Hakkimizda.Where(x => x.HakkimizdaID == id).FirstOrDefault();
            return View(hakkimizda);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Hakkimizda hakkimizda)
        {
            if (ModelState.IsValid)
            {
                var hakkimizda2 = db.Hakkimizda.Where(x => x.HakkimizdaID == id).SingleOrDefault();
                hakkimizda2.Aciklama = hakkimizda.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}