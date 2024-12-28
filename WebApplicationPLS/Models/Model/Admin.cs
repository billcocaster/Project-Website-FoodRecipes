using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplicationPLS.Models.Model
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }
        [Required,StringLength(50,ErrorMessage ="50 Karakter olmalıdır")]
        public string Eposta { get; set; }
        [Required,StringLength(50,ErrorMessage ="50 Karakter olmalıdır")]
        public string Sifre { get; set; }
        [DisplayName("Hesap Oluşturulma Tarihi")]
        public DateTime HesapTarihi { get; set; } = DateTime.Now;

        public string Yetki { get; set; }

    }
}