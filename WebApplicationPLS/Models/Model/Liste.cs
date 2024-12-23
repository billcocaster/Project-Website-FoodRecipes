using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplicationPLS.Models.Model
{
    [Table("Liste")]
    public class Liste
    {
        [Key]
        public int ListeID { get; set; }
        [Required, StringLength(50, ErrorMessage = "50 karakter olmalıdır")]
        [DisplayName("Liste Adı")]
        public string ListeAd { get; set; }
        [DisplayName("Liste görseli")]
        public string ResimURL { get; set; }
        [DisplayName("Liste Açıklaması")]

        public string Aciklama { get; set; }
        public ICollection<Tarif> Tarifs { get; set; }
    }
}