using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Covid.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public int Disable { get; set; }
        public virtual ICollection<Vacina> Vacina { get; set; }
    }
}