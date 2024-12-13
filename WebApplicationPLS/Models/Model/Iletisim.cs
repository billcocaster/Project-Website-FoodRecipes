using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplicationPLS.Models.Model
{
    [Table("Iletisim")]
    public class Iletisim
    {
        [Key]
        public int IletisimID { get; set; }
        [StringLength(250,ErrorMessage ="250 karakter olmalıdır")]
        public string Adres { get; set; }
        [StringLength(250,ErrorMessage ="250 karakter olmalıdır")]
        public string Telefon { get; set; }

        public string Email { get; set; }
        public string Instagram { get; set; }
    }
}