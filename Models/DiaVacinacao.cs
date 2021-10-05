using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Covid.Models
{
    public class DiaVacinacao
    {
        public int Id { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddTHH:mm}")]
        public DateTime DataVacinacao { get; set; }
        public int Disable { get; set; }
        public int LoteId { get; set; }
        public virtual Lote Lote { get; set; }
    }
}