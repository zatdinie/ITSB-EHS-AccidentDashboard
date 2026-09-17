namespace ITSB.AccidentDashboard.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlantGroupUniqueIndex : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PlantGroups", "Label", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.PlantGroups", "DisplayName", c => c.String(maxLength: 100));
            CreateIndex("dbo.PlantGroups", "Label", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.PlantGroups", new[] { "Label" });
            AlterColumn("dbo.PlantGroups", "DisplayName", c => c.String());
            AlterColumn("dbo.PlantGroups", "Label", c => c.String());
        }
    }
}
