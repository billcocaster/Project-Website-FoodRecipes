using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApplicationPLS.Models.DataContext;
using PagedList;
using PagedList.Mvc;
using WebApplicationPLS.Models.Model;



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
        public ActionResult Iletisim()
        {
            return View(db.Iletisim.SingleOrDefault());        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Iletisim(string adSoyad=null, string email = null, string konu = null, string mesaj = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("yemektarifleriprj@gmail.com");
                mail.To.Add("yemektarifleriprj@gmail.com");
                mail.Subject = konu;
                mail.Body = $"Gönderen: {adSoyad} ({email})<br />Mesaj: {mesaj}";
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("yemektarifleriprj@gmail.com", "pmts xwxy eznk wysb");
                smtp.EnableSsl = true;

                smtp.Send(mail);
                ViewBag.Uyari = "Mesaj başarıyla gönderildi.";
            }
            catch (Exception ex)
            {
                ViewBag.Uyari = "Hata: " + ex.Message;
            }

            return View();
        }

        public ActionResult Blog(int Sayfa = 1)
        {
            return View(db.Blog.Include("Kategori").OrderByDescending(x => x.BlogID).ToPagedList(Sayfa,6));
        }
        public ActionResult KategoriBlog(int id, int Sayfa=1)
        {
            var b = db.Blog.Include("Kategori").OrderByDescending(x=>x.KategoriID).Where(x=>x.Kategori.KategoriID==id).ToPagedList(Sayfa, 6);
            return View(b);
        }
        public ActionResult BlogDetay(int id)
        {
            var b = db.Blog.Include("Kategori").Include("Yorums").Where(x=>x.BlogID == id).SingleOrDefault();
            return View(b);
        }
        public JsonResult YorumYap(string adsoyad, string eposta, string icerik, int blogid)
        {
            if(icerik==null) return Json(new {success=false, message="Yorum içeriği boş olamaz."},JsonRequestBehavior.AllowGet);
            db.Yorum.Add(new Yorum { AdSoyad = adsoyad, Eposta = eposta, Icerik = icerik, BlogID = blogid, Onay=false });
            db.SaveChanges();
            return Json(new { success =true, message="Yorum gönderildi. Onay bekliyor."},JsonRequestBehavior.AllowGet);
        }
        public ActionResult BlogKategoriPartial()
        {

            return PartialView(db.Kategori.Include("Blogs").ToList().OrderBy(x=>x.KategoriAd));
        }
        public ActionResult BlogKayitPartial()
        {
            return PartialView(db.Blog.ToList().OrderByDescending(x=>x.BlogID));
        }
        public ActionResult FooterPartial()
        {
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            return PartialView(db.Hakkimizda.SingleOrDefault());
        }
       
        

    }
}