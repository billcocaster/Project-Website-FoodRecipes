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
        [Route("")]
        [Route("Anasayfa")]


        public ActionResult Index()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View();
        }
        public ActionResult SliderPartial()
        {
            return View(db.Slider.ToList().OrderByDescending(x=>x.SliderID));
        }
        [Route("Hakkimizda")]

        public ActionResult Hakkimizda()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.BlogSayisi = db.Blog.Where(x => x.Icerik != null).Count();
            ViewBag.TarifSayisi = db.Tarif.Where(Tarif => Tarif.Icerik != null).Count();
            ViewBag.ListeSayisi = db.Liste.Count();



            return View(db.Hakkimizda.SingleOrDefault());
        }
        [Route("Iletisim")]

        public ActionResult Iletisim()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

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

        [Route("Blog")]
        public ActionResult Blog(int Sayfa = 1)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return View(db.Blog.Include("Kategori").OrderByDescending(x => x.BlogID).ToPagedList(Sayfa,6));
        }
        [Route("Blog/{kategoriad}/{id:int}")]
///Blog/@SeoHelper.ToSeoUrl(item.Baslik).ToLower()-@item.BlogID
        public ActionResult KategoriBlog(int id, int Sayfa=1)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            var b = db.Blog.Include("Kategori").OrderByDescending(x=>x.KategoriID).Where(x=>x.Kategori.KategoriID==id).ToPagedList(Sayfa, 6);
            return View(b);
        }
        [Route("Blog/{baslik}-{id:int}")]
        public ActionResult BlogDetay(int id)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.BlogYorumSayisi = db.Blog.Include("Yorums").Where(x => x.BlogID == id).SingleOrDefault().Yorums.Count();


            var b = db.Blog.Include("Kategori").Include("Yorums").Where(x=>x.BlogID == id).SingleOrDefault();
            return View(b);
        }
        [Route("Tarif")]
        public ActionResult Tarif(int Sayfa = 1)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();


            return View(db.Tarif.Include("Liste").OrderByDescending(x => x.TarifID).ToPagedList(Sayfa, 6));
        }
        [Route("Tarif/{listead}/{id:int}")]

        public ActionResult ListeTarif(int id, int Sayfa = 1)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            var b = db.Tarif.Include("Liste").OrderByDescending(x => x.ListeID).Where(x => x.Liste.ListeID == id).ToPagedList(Sayfa, 6);
            return View(b);
        }
        [Route("Tarif/{baslik}-{id:int}")]
        public ActionResult TarifDetay(int id)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.TarifYorumSayisi = db.Tarif.Include("TarifYorums").Where(x => x.TarifID == id).SingleOrDefault().TarifYorums.Count();

            var b = db.Tarif.Include("Liste").Include("TarifYorums").Where(x => x.TarifID == id).SingleOrDefault();
            return View(b);
        }
        public JsonResult YorumYap(string adsoyad, string eposta, string icerik, int blogid)
        {
            if(icerik==null) return Json(new {success=false, message="Yorum içeriği boş olamaz."},JsonRequestBehavior.AllowGet);
            db.Yorum.Add(new Yorum { AdSoyad = adsoyad, Eposta = eposta, Icerik = icerik, BlogID = blogid, Onay=false });
            db.SaveChanges();
            return Json(new { success =true, message="Yorum gönderildi. Onay bekliyor."},JsonRequestBehavior.AllowGet);
        }
        public JsonResult TarifYorumYap(string adsoyad, string eposta, string icerik, int tarifid)
        {
            if (icerik == null) return Json(new { success = false, message = "Yorum içeriği boş olamaz." }, JsonRequestBehavior.AllowGet);
            db.TarifYorum.Add(new TarifYorum { AdSoyad = adsoyad, Eposta = eposta, Icerik = icerik, TarifID = tarifid, Onay = false });
            db.SaveChanges();
            return Json(new { success = true, message = "Yorum gönderildi. Onay bekliyor." }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BlogKategoriPartial()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return PartialView(db.Kategori.Include("Blogs").ToList().OrderBy(x=>x.KategoriAd));
        }
        public ActionResult BlogKayitPartial()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return PartialView(db.Blog.ToList().OrderByDescending(x=>x.BlogID));
        }
        public ActionResult TarifListePartial()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return PartialView(db.Liste.Include("Tarifs").ToList().OrderBy(x => x.ListeAd));
        }
        public ActionResult TarifKayitPartial()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return PartialView(db.Tarif.ToList().OrderByDescending(x => x.TarifID));
        }
        [Route("Liste")]
        public ActionResult Liste(int Sayfa = 1)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return View(db.Liste.OrderByDescending(x => x.ListeID).ToPagedList(Sayfa, 6));
        }
        public ActionResult FooterPartial()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            return PartialView(db.Hakkimizda.SingleOrDefault());
        }
       
        

    }
}