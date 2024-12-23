using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationPLS.Models;
using WebApplicationPLS.Models.DataContext;
using WebApplicationPLS.Models.Model;

namespace WebApplicationPLS.Controllers
{
    public class AdminController : Controller
    {
        YemekTarifleriDBContext db = new YemekTarifleriDBContext();
        // GET: Admin
        [Route("yonetimpaneli")]
        public ActionResult Index()
        {
            ViewBag.YorumBekleyen = db.Yorum.Where(x => x.Onay == false).ToList();
            ViewBag.YorumOnay = db.Yorum.Where(x => x.Onay == false).Count();
            var sorgu = db.Kategori.ToList();
            return View(sorgu);
        }
        [Route("yonetimpaneli/giris")]

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(Admin admin)
        {
            var login = db.Admin.Where(x=>x.Eposta==admin.Eposta).SingleOrDefault();
            if (login == null || login.Sifre != admin.Sifre)
            {
                ViewBag.uyari = "Kullanıcı adı veya şifre yanlış";
                return View(admin);
            }
            Session["adminid"] = login.AdminID;
            Session["eposta"] = login.Eposta;
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult Logout()
        {
            Session["adminid"] = null;
            Session["eposta"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }
       
    }
}