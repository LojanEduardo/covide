using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Covid.Models
{
    public class Contexto : DbContext
    {
        public Contexto() : base(nameOrConnectionString: "StringConexao") { }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Local> Local { get; set; }
        public DbSet<Lote> Lote { get; set; }
        public DbSet<Vacina> Vacina { get; set; }
        public DbSet<Vacinado> Vacinado { get; set; }
        public DbSet<DiaVacinacao> DiaVacinacao { get; set; }
        public DbSet<Historico> Historico { get; set; }

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            var usu = mb.Entity<Usuario>();
            usu.ToTable("usu_usuario");
            usu.Property(x => x.Id).HasColumnName("usu_id");
            usu.Property(x => x.Email).HasColumnName("usu_email");
            usu.Property(x => x.Senha).HasColumnName("usu_senha");
            usu.Property(x => x.Hash).HasColumnName("usu_hash");
            usu.Property(x => x.Disable).HasColumnName("usu_disable");


            var emp = mb.Entity<Empresa>();
            emp.ToTable("emp_empresa");
            emp.Property(x => x.Id).HasColumnName("emp_id");
            emp.Property(x => x.Nome).HasColumnName("emp_nome");
            emp.Property(x => x.Disable).HasColumnName("emp_disable");


            var loc = mb.Entity<Local>();
            loc.ToTable("loc_local");
            loc.Property(x => x.Id).HasColumnName("loc_id");
            loc.Property(x => x.Cidade).HasColumnName("loc_cidade");
            loc.Property(x => x.Bairro).HasColumnName("loc_bairro");
            loc.Property(x => x.Rua).HasColumnName("loc_rua");
            loc.Property(x => x.Numero).HasColumnName("loc_numero");
            loc.Property(x => x.Complemento).HasColumnName("loc_complemento");
            loc.Property(x => x.Disable).HasColumnName("loc_disable");


            var lot = mb.Entity<Lote>();
            lot.ToTable("lot_lote");
            lot.Property(x => x.Id).HasColumnName("lot_id");
            lot.Property(x => x.Quantidade).HasColumnName("lot_quantidade");
            lot.Property(x => x.Disable).HasColumnName("lot_disable");


            var vac = mb.Entity<Vacina>();
            vac.ToTable("vac_vacina");
            vac.Property(x => x.Id).HasColumnName("vac_Id");
            vac.Property(x => x.Nome).HasColumnName("vac_nome");
            vac.Property(x => x.Nome).HasColumnName("vac_quantDose");
            vac.Property(x => x.Disable).HasColumnName("vac_disable");


            var vci = mb.Entity<Vacinado>();
            vci.ToTable("vci_vacinado");
            vci.Property(x => x.Id).HasColumnName("vci_id");
            vci.Property(x => x.Nome).HasColumnName("vci_nome");
            vci.Property(x => x.DataNascimento).HasColumnName("vci_dataNasc");
            vci.Property(x => x.DataVacinado).HasColumnName("vci_dataVac");
            vci.Property(x => x.Dose).HasColumnName("vci_dose");
            vci.Property(x => x.Disable).HasColumnName("vci_disable");

            var dia = mb.Entity<DiaVacinacao>();
            dia.ToTable("dia_diaVacinacao");
            dia.Property(x => x.Id).HasColumnName("dia_id");
            dia.Property(x => x.DataVacinacao).HasColumnName("dia_dataVascinacao");
            dia.Property(x => x.Disable).HasColumnName("dia_disable");

            var his = mb.Entity<Historico>();
            his.ToTable("his_historico");
            his.Property(x => x.Id).HasColumnName("his_id");
            his.Property(x => x.TabelaNome).HasColumnName("his_tabelaNome");
            his.Property(x => x.Descricao).HasColumnName("his_descricao");
            his.Property(x => x.ItemId).HasColumnName("his_itemId");
            his.Property(x => x.Data).HasColumnName("his_data");
        }
    }
}