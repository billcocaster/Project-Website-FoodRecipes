using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplicationPLS.Models.Model
{
    [Table("Blog")]
    public class Blog
    {
        public int BlogID { get; set; }
        [DisplayName("Blog Başlığı")]
        public string Baslik { get; set; }
        [DisplayName("Blog İçeriği")]
        public string Icerik { get; set; }
        [DisplayName("Blog görseli")]
        public string ResimURL { get; set; }
        public int TiklanmaSayisi { get; set; }
        public int? KategoriID { get; set; }
        public Kategori Kategori { get; set; }
        public ICollection<Yorum> Yorums { get; set; }
        [DisplayName("Yüklenme Tarihi")]
        public DateTime YuklenmeTarihi { get; set; } = DateTime.Now;
    }
}