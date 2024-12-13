using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationPLS.Models.DataContext;

namespace WebApplicationPLS.Controllers
{
    public class HomeController : Controller
    {
        private YemekTarifleriDBContext db = new YemekTarifleriDBContext();
        public ActionResult Index()
        {
   
            return View();
        }
        public ActionResult SliderPartial()
        {
            return View(db.Slider.ToList().OrderByDescending(x=>x.SliderID));
        }
        public ActionResult Hakkimizda()
        {

            return View(db.Hakkimizda.SingleOrDefault());
        }

        public ActionResult FooterPartial()
        {
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            return PartialView(db.Hakkimizda.SingleOrDefault());
        }
       
        

    }
}