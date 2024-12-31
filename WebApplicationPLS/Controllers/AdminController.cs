using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
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
            ViewBag.BlogSayisi = db.Blog.Where(x => x.Icerik != null).Count();
            ViewBag.TarifSayisi = db.Tarif.Where(Tarif => Tarif.Icerik != null).Count();
            ViewBag.ListeSayisi = db.Liste.Count();

            ViewBag.YorumBekleyen = db.Yorum.Where(x => x.Onay == false).ToList();
            ViewBag.YorumOnay = db.Yorum.Where(x => x.Onay == false).Count();

            ViewBag.TarifYorumBekleyen = db.TarifYorum.Where(x => x.Onay == false).ToList();
            ViewBag.TarifYorumOnay = db.TarifYorum.Where(x => x.Onay == false).Count();

            ViewBag.ToplamYorum = ViewBag.TarifYorumOnay + ViewBag.YorumOnay;

            ViewBag.EnSonTariflerAdmin = db.Tarif.OrderByDescending(t => t.TarifID).Take(3);
            ViewBag.EnSonBloglarAdmin = db.Blog.OrderByDescending(b => b.BlogID).Take(3);

            ViewBag.TiklananTariflerAdmin = db.Tarif.OrderByDescending(t => t.TiklanmaSayisi).Take(5);
            ViewBag.TiklananBloglarAdmin = db.Blog.OrderByDescending(t => t.TiklanmaSayisi).Take(3);




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
            if (login.Eposta==admin.Eposta && login.Sifre==Crypto.Hash(admin.Sifre,"MD5"))
            {
                Session["adminid"] = login.AdminID;
                Session["eposta"] = login.Eposta;
                Session["yetki"] = login.Yetki;

                return RedirectToAction("Index", "Admin");
            }
            ViewBag.uyari = "Kullanıcı adı veya şifre yanlış";
            return View(admin);

        }
        public ActionResult Logout()
        {
            Session["adminid"] = null;
            Session["eposta"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }
        public ActionResult ForgotPassword()
        {
            return View();        
        }
        [HttpPost]
        public ActionResult ForgotPassword(string eposta)
        {
            var m = db.Admin.Where(x => x.Eposta == eposta).SingleOrDefault();
            if(m!=null)
            {
                Random rnd = new Random();
                int yeniSifre = rnd.Next();
                Admin sifre = new Admin();
                m.Sifre = Crypto.Hash(Convert.ToString(yeniSifre), "MD5");
                db.SaveChanges();

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("yemektarifleriprj@gmail.com");
                mail.To.Add(eposta);
                mail.Subject = "Admin Şifreniz";
                mail.Body = $"Gönderen: YemekTarifleri<br />Şifreniz: {Convert.ToString(yeniSifre)}";
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("yemektarifleriprj@gmail.com", "pmts xwxy eznk wysb");
                smtp.EnableSsl = true;

                smtp.Send(mail);
                ViewBag.Uyari = "Şifre başarıyla gönderildi.";
            }
            else
            {
                ViewBag.Uyari = "Hata Oluştu. Tekrar deneyiniz.";
            }
            
            return View();
        }
        public ActionResult Adminler()
        {
            return View(db.Admin.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Admin admin, string eposta, string sifre)
        {
            if (ModelState.IsValid)
            {
                admin.Sifre=Crypto.Hash(sifre,"MD5");
                db.Admin.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }
            return View(admin);
        }
        public ActionResult Edit(int id)
        {
            var a = db.Admin.Where(x => x.AdminID == id).SingleOrDefault();
            return View(a);
        }
        [HttpPost]
        public ActionResult Edit(int id,Admin admin, string eposta ,string sifre)
        {
            if (ModelState.IsValid)
            {
                var a = db.Admin.Where(x => x.AdminID == id).SingleOrDefault();
                a.Sifre = Crypto.Hash(sifre, "MD5");
                a.Eposta = admin.Eposta;
                a.Yetki = admin.Yetki;
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }
            
            return View(admin);
        }

        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admin.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }
        public ActionResult Delete(int id)
        {
            var a = db.Admin.Where(x => x.AdminID == id).SingleOrDefault();
            if (a != null)
            {
                db.Admin.Remove(a);
                db.SaveChanges();
                return RedirectToAction("Adminler");

            }
            return View();
        }


    }
}