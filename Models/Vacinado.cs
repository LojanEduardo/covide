using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Covid.Models
{
    public class Vacinado
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(14)]
        public string Cpf { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddTHH:mm}")]
        public DateTime DataNascimento { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddTHH:mm}")]
        public DateTime DataVacinado { get; set; }
        [Required]
        public int Dose { get; set; }
        public int Disable { get; set; }
        public int LoteId {get;set;}
        public virtual Lote Lote { get; set; }

    }
}