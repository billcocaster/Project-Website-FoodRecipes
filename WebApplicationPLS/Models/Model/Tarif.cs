using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplicationPLS.Models.Model
{
    [Table("Tarif")]
    public class Tarif
    {
        public int TarifID { get; set; }
        [DisplayName("Tarif Başlığı")]
        public string Baslik { get; set; }
        [DisplayName("Tarif İçeriği")]
        public string Icerik { get; set; }
        [DisplayName("Tarif görseli")]
        public string ResimURL { get; set; }
        public int? ListeID { get; set; }
        [DisplayName("Yüklenme Tarihi")]
        public DateTime YuklenmeTarihi { get; set; } = DateTime.Now;
        public Liste Liste { get; set; }
        public int TiklanmaSayisi { get; set; }
        public ICollection<TarifYorum> TarifYorums { get; set; }
    }
}