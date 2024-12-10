using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplicationPLS.Models.Model
{
    [Table("Kategori")]
    public class Kategori
    {
        [Key]
        public int KategoriID { get; set; }
        [Required,StringLength(50,ErrorMessage ="50 karakter olmalıdır")]
        [DisplayName("Kategori Adı")]
        public string KategoriAd { get; set; }
        [DisplayName("Kategori Açıklaması")]

        public string Aciklama { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}