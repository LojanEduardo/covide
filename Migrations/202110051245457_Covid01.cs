namespace Covid.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Covid01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.dia_diaVacinacao",
                c => new
                    {
                        dia_id = c.Int(nullable: false, identity: true),
                        dia_dataVascinacao = c.DateTime(nullable: false, precision: 0),
                        dia_disable = c.Int(nullable: false),
                        LoteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.dia_id)
                .ForeignKey("dbo.lot_lote", t => t.LoteId, cascadeDelete: true);
            
            CreateTable(
                "dbo.lot_lote",
                c => new
                    {
                        lot_id = c.Int(nullable: false, identity: true),
                        lot_quantidade = c.Int(nullable: false),
                        lot_disable = c.Int(nullable: false),
                        VacinaId = c.Int(nullable: false),
                        LocalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.lot_id)
                .ForeignKey("dbo.loc_local", t => t.LocalId, cascadeDelete: true)
                .ForeignKey("dbo.vac_vacina", t => t.VacinaId, cascadeDelete: true);
            
            CreateTable(
                "dbo.loc_local",
                c => new
                    {
                        loc_id = c.Int(nullable: false, identity: true),
                        loc_cidade = c.String(nullable: false, unicode: false),
                        loc_bairro = c.String(nullable: false, unicode: false),
                        loc_rua = c.String(nullable: false, unicode: false),
                        loc_numero = c.String(nullable: false, unicode: false),
                        loc_complemento = c.String(unicode: false),
                        loc_disable = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.loc_id);
            
            CreateTable(
                "dbo.vac_vacina",
                c => new
                    {
                        vac_Id = c.Int(nullable: false, identity: true),
                        vac_quantDose = c.String(nullable: false, unicode: false),
                        QuantidadeDose = c.Int(nullable: false),
                        vac_disable = c.Int(nullable: false),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.vac_Id)
                .ForeignKey("dbo.emp_empresa", t => t.EmpresaId, cascadeDelete: true);
            
            CreateTable(
                "dbo.emp_empresa",
                c => new
                    {
                        emp_id = c.Int(nullable: false, identity: true),
                        emp_nome = c.String(nullable: false, unicode: false),
                        emp_disable = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.emp_id);
            
            CreateTable(
                "dbo.vci_vacinado",
                c => new
                    {
                        vci_id = c.Int(nullable: false, identity: true),
                        Cpf = c.String(nullable: false, maxLength: 14, storeType: "nvarchar"),
                        vci_nome = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        vci_dataNasc = c.DateTime(nullable: false, precision: 0),
                        vci_dataVac = c.DateTime(nullable: false, precision: 0),
                        vci_dose = c.Int(nullable: false),
                        vci_disable = c.Int(nullable: false),
                        LoteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.vci_id)
                .ForeignKey("dbo.lot_lote", t => t.LoteId, cascadeDelete: true);
            
            CreateTable(
                "dbo.his_historico",
                c => new
                    {
                        his_id = c.Int(nullable: false, identity: true),
                        his_tabelaNome = c.String(nullable: false, unicode: false),
                        his_descricao = c.String(unicode: false),
                        his_itemId = c.Int(nullable: false),
                        his_data = c.DateTime(nullable: false, precision: 0),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.his_id)
                .ForeignKey("dbo.usu_usuario", t => t.UsuarioId, cascadeDelete: true);
            
            CreateTable(
                "dbo.usu_usuario",
                c => new
                    {
                        usu_id = c.Int(nullable: false, identity: true),
                        usu_email = c.String(unicode: false),
                        usu_senha = c.String(unicode: false),
                        usu_hash = c.String(unicode: false),
                        usu_disable = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.usu_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.his_historico", "UsuarioId", "dbo.usu_usuario");
            DropForeignKey("dbo.vci_vacinado", "LoteId", "dbo.lot_lote");
            DropForeignKey("dbo.lot_lote", "VacinaId", "dbo.vac_vacina");
            DropForeignKey("dbo.vac_vacina", "EmpresaId", "dbo.emp_empresa");
            DropForeignKey("dbo.lot_lote", "LocalId", "dbo.loc_local");
            DropForeignKey("dbo.dia_diaVacinacao", "LoteId", "dbo.lot_lote");
            DropIndex("dbo.his_historico", new[] { "UsuarioId" });
            DropIndex("dbo.vci_vacinado", new[] { "LoteId" });
            DropIndex("dbo.vac_vacina", new[] { "EmpresaId" });
            DropIndex("dbo.lot_lote", new[] { "LocalId" });
            DropIndex("dbo.lot_lote", new[] { "VacinaId" });
            DropIndex("dbo.dia_diaVacinacao", new[] { "LoteId" });
            DropTable("dbo.usu_usuario");
            DropTable("dbo.his_historico");
            DropTable("dbo.vci_vacinado");
            DropTable("dbo.emp_empresa");
            DropTable("dbo.vac_vacina");
            DropTable("dbo.loc_local");
            DropTable("dbo.lot_lote");
            DropTable("dbo.dia_diaVacinacao");
        }
    }
}
