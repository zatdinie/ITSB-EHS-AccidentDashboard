namespace ITSB.AccidentDashboard.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLookupValidationConstraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AccidentCauses", "Description", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.BodyParts", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.InjuryTypes", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InjuryTypes", "Name", c => c.String());
            AlterColumn("dbo.BodyParts", "Name", c => c.String());
            AlterColumn("dbo.AccidentCauses", "Description", c => c.String());
        }
    }
}
