using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationPLS.Models;
using WebApplicationPLS.Models.DataContext;

namespace WebApplicationPLS.Controllers
{
    public class AdminController : Controller
    {
        YemekTarifleriDBContext db = new YemekTarifleriDBContext();
        // GET: Admin
        public ActionResult Index()
        {
            var sorgu = db.Kategori.ToList();
            return View(sorgu);
        }
    }
}