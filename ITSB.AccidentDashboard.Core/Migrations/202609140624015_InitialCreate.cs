namespace ITSB.AccidentDashboard.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccidentCauses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AccidentIncidents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkerName = c.String(),
                        IcPassportNo = c.String(),
                        EmployeeId = c.String(),
                        PlantId = c.Int(nullable: false),
                        Gender = c.String(),
                        Age = c.Int(nullable: false),
                        Nationality = c.String(),
                        EmploymentStatus = c.String(),
                        DateOfOccurrence = c.DateTime(nullable: false),
                        TimeOfOccurrence = c.Time(nullable: false, precision: 7),
                        HowAccidentHappened = c.String(),
                        AccidentCauseId = c.Int(),
                        BodyPartId = c.Int(),
                        InjuryTypeId = c.Int(),
                        IsFirstAidCase = c.Boolean(nullable: false),
                        IsLostWorkDayCase = c.Boolean(nullable: false),
                        IsRecordableCase = c.Boolean(nullable: false),
                        LostWorkDays = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccidentCauses", t => t.AccidentCauseId)
                .ForeignKey("dbo.BodyParts", t => t.BodyPartId)
                .ForeignKey("dbo.InjuryTypes", t => t.InjuryTypeId)
                .ForeignKey("dbo.Plants", t => t.PlantId, cascadeDelete: true)
                .Index(t => t.PlantId)
                .Index(t => t.AccidentCauseId)
                .Index(t => t.BodyPartId)
                .Index(t => t.InjuryTypeId);
            
            CreateTable(
                "dbo.BodyParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InjuryTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Plants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlantCode = c.String(nullable: false, maxLength: 10),
                        Name = c.String(nullable: false, maxLength: 100),
                        PlantGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PlantGroups", t => t.PlantGroupId, cascadeDelete: true)
                .Index(t => t.PlantGroupId);
            
            CreateTable(
                "dbo.PlantGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        DisplayName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MonthlyManHours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlantId = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        DirectLaborHeadcount = c.Int(nullable: false),
                        DirectLaborHours = c.Int(nullable: false),
                        IndirectLaborHeadcount = c.Int(nullable: false),
                        IndirectLaborHours = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Plants", t => t.PlantId, cascadeDelete: true)
                .Index(t => t.PlantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MonthlyManHours", "PlantId", "dbo.Plants");
            DropForeignKey("dbo.AccidentIncidents", "PlantId", "dbo.Plants");
            DropForeignKey("dbo.Plants", "PlantGroupId", "dbo.PlantGroups");
            DropForeignKey("dbo.AccidentIncidents", "InjuryTypeId", "dbo.InjuryTypes");
            DropForeignKey("dbo.AccidentIncidents", "BodyPartId", "dbo.BodyParts");
            DropForeignKey("dbo.AccidentIncidents", "AccidentCauseId", "dbo.AccidentCauses");
            DropIndex("dbo.MonthlyManHours", new[] { "PlantId" });
            DropIndex("dbo.Plants", new[] { "PlantGroupId" });
            DropIndex("dbo.AccidentIncidents", new[] { "InjuryTypeId" });
            DropIndex("dbo.AccidentIncidents", new[] { "BodyPartId" });
            DropIndex("dbo.AccidentIncidents", new[] { "AccidentCauseId" });
            DropIndex("dbo.AccidentIncidents", new[] { "PlantId" });
            DropTable("dbo.MonthlyManHours");
            DropTable("dbo.PlantGroups");
            DropTable("dbo.Plants");
            DropTable("dbo.InjuryTypes");
            DropTable("dbo.BodyParts");
            DropTable("dbo.AccidentIncidents");
            DropTable("dbo.AccidentCauses");
        }
    }
}
