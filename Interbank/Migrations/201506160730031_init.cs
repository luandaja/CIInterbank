namespace Interbank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                "dbo.Simuladors",
                c => new
                    {
                        SimuladorID = c.Int(nullable: false, identity: true),
                        plazo = c.Int(nullable: false),
                        tipoSeguro = c.Int(nullable: false),
                        tipoCuotaIncial = c.Int(nullable: false),
                        montoDelCredito = c.Double(nullable: false),
                        importeAsegurado = c.Double(nullable: false),
                        val_deuda = c.Double(nullable: false),
                        val_CuotaIncial = c.Double(nullable: false),
                        NCuotas = c.Int(nullable: false),
                        por_CuotaIncial = c.Single(nullable: false),
                        por_SeguroDesgravamen = c.Single(nullable: false),
                        por_SeguroVehicular = c.Single(nullable: false),
                        tea = c.Single(nullable: false),
                        ted = c.Single(nullable: false),
                        tem = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.SimuladorID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Simuladors");
        }
    }
}
