using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplicationPLS.Models.Model
{
    [Table("Kimlik")]
    public class Kimlik
    {
        [Key]
        public int KimlikID { get; set; }
        [DisplayName("Site Baslık")]
        [Required,StringLength(100,ErrorMessage ="100 karakter olmalıdır")]
        public string Title { get; set; }
        [DisplayName("Site Anahtar Kelimeler")]
        [Required, StringLength(200, ErrorMessage = "200 karakter olmalıdır")]
        public string Keywords { get; set; }
        [DisplayName("Site Aciklama")]
        [Required, StringLength(300, ErrorMessage = "300 karakter olmalıdır")]
        public string Description { get; set; }
        [DisplayName("Site Logo")]
        public string LogoURL { get; set; }
        [DisplayName("Site unvan")]
        public string Unvan { get; set; }
    }
}