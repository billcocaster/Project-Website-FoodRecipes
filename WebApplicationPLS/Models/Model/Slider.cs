using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationPLS.Models.Model
{
    public class Slider
    {
        [Key]
        public int SliderID { get; set; }
        [DisplayName("Slider Başlık"), StringLength(30, ErrorMessage ="30 karakter olmalıdır")]
        public string Baslik {  get; set; }
        [DisplayName("Slider Açıklama"), StringLength(150, ErrorMessage = "150 karakter olmalıdır")]
        public string Aciklama { get; set; }
        [DisplayName("Slider Resim"), StringLength(200, ErrorMessage = "200 karakter olmalıdır")]
        public string ResimURL { get; set; }
    }
}