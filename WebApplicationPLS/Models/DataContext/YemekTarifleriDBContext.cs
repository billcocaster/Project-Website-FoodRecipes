using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplicationPLS.Models.Model;

namespace WebApplicationPLS.Models.DataContext
{
    public class YemekTarifleriDBContext:DbContext
    {
        public YemekTarifleriDBContext():base("YemekTarifleriDB2")
        {
            
        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Hakkimizda> Hakkimizda { get; set; }
        public DbSet<Iletisim> Iletisim { get; set; }
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Kimlik> Kimlik { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<Yorum> Yorum { get; set; }







    }
}