using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplicationPLS.Models.Model
{
    [Table("Blog")]
    public class Blog
    {
        public int BlogID { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public string ResimURL { get; set; }
        public int? KategoriID { get; set; }
        public Kategori Kategori { get; set; }
    }
}