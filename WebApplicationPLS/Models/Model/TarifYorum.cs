using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationPLS.Models.Model
{
    [Table("TarifYorum")]
    public class TarifYorum
    {
        [Key]
        public int TarifYorumID { get; set; }
        [Required, StringLength(50, ErrorMessage = "50 karakter olmalıdır")]
        public string AdSoyad { get; set; }
        public string Eposta { get; set; }
        [DisplayName("Yorumunuz")]
        public string Icerik { get; set; }
        public bool Onay { get; set; }
        [DisplayName("Yüklenme Tarihi")]
        public DateTime YorumTarihi { get; set; } = DateTime.Now;
        public int? TarifID { get; set; }
        public Tarif Tarif { get; set; }
    }
}