using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Covid.Models
{
    public class Vacina
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public int QuantidadeDose { get; set; }
        public int Disable { get; set; }
        public int EmpresaId { get; set; }
        public virtual Empresa Empresa{ get; set; }
    }
}