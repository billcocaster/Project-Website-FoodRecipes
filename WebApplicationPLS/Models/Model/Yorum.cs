﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplicationPLS.Models.Model
{
    [Table("Yorum")]
    public class Yorum
    {
        public int YorumID {  get; set; }
        [Required,StringLength(50,ErrorMessage ="50 karakter olmalıdır")]
        public string AdSoyad { get; set; }
        public string Eposta { get; set; }
        [DisplayName("Yorumunuz")]
        public string Icerik { get; set; }
        public bool Onay {  get; set; }
        [DisplayName("Yüklenme Tarihi")]
        public DateTime YorumTarihi { get; set; } = DateTime.Now;
        public int? BlogID { get; set; }
        public Blog Blog { get; set; }
    }
}