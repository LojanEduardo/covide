using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Covid.Models
{
    public class Lote
    {
        public int Id { get; set; }
        [Required]
        public int Quantidade { get; set; }
        public int Disable { get; set; }
        public int VacinaId { get; set; }
        public int LocalId { get; set; }
        public virtual Vacina Vacina { get; set; }
        public virtual Local Local { get; set; }
        public virtual ICollection<Vacinado> Vacinado { get; set; }
        public virtual ICollection<DiaVacinacao> DiaVacinacao { get; set; }
    }
}