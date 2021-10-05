using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Covid.Models
{
    public class Local
    {
        public int Id { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string Rua { get; set; }
        [Required]
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public int Disable { get; set; }
        public virtual ICollection<Lote> Lote { get; set; }
    }
}