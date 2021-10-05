using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Covid.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Hash { get; set; }
        public int Disable { get; set; }
        public virtual ICollection<Historico> Historico { get; set; }
    }

}