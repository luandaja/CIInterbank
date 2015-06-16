namespace Interbank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Simuladors", "val_CuotaFinal", c => c.Double(nullable: false));
            AddColumn("dbo.Simuladors", "por_cuotaFinal", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Simuladors", "por_cuotaFinal");
            DropColumn("dbo.Simuladors", "val_CuotaFinal");
        }
    }
}
