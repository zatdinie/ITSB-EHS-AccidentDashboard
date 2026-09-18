namespace ITSB.AccidentDashboard.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ACCIDENT.AccidentCauses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ACCIDENT.AccidentIncidents",
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
                .ForeignKey("ACCIDENT.AccidentCauses", t => t.AccidentCauseId)
                .ForeignKey("ACCIDENT.BodyParts", t => t.BodyPartId)
                .ForeignKey("ACCIDENT.InjuryTypes", t => t.InjuryTypeId)
                .ForeignKey("ACCIDENT.Plants", t => t.PlantId, cascadeDelete: true)
                .Index(t => t.PlantId)
                .Index(t => t.AccidentCauseId)
                .Index(t => t.BodyPartId)
                .Index(t => t.InjuryTypeId);
            
            CreateTable(
                "ACCIDENT.BodyParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ACCIDENT.InjuryTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ACCIDENT.Plants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlantCode = c.String(nullable: false, maxLength: 10),
                        Name = c.String(nullable: false, maxLength: 100),
                        PlantGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ACCIDENT.PlantGroups", t => t.PlantGroupId, cascadeDelete: true)
                .Index(t => t.PlantGroupId);
            
            CreateTable(
                "ACCIDENT.PlantGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Label = c.String(nullable: false, maxLength: 50),
                        DisplayName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Label, unique: true);
            
            CreateTable(
                "ACCIDENT.MonthlyManHours",
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
                .ForeignKey("ACCIDENT.Plants", t => t.PlantId, cascadeDelete: true)
                .Index(t => t.PlantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("ACCIDENT.MonthlyManHours", "PlantId", "ACCIDENT.Plants");
            DropForeignKey("ACCIDENT.AccidentIncidents", "PlantId", "ACCIDENT.Plants");
            DropForeignKey("ACCIDENT.Plants", "PlantGroupId", "ACCIDENT.PlantGroups");
            DropForeignKey("ACCIDENT.AccidentIncidents", "InjuryTypeId", "ACCIDENT.InjuryTypes");
            DropForeignKey("ACCIDENT.AccidentIncidents", "BodyPartId", "ACCIDENT.BodyParts");
            DropForeignKey("ACCIDENT.AccidentIncidents", "AccidentCauseId", "ACCIDENT.AccidentCauses");
            DropIndex("ACCIDENT.MonthlyManHours", new[] { "PlantId" });
            DropIndex("ACCIDENT.PlantGroups", new[] { "Label" });
            DropIndex("ACCIDENT.Plants", new[] { "PlantGroupId" });
            DropIndex("ACCIDENT.AccidentIncidents", new[] { "InjuryTypeId" });
            DropIndex("ACCIDENT.AccidentIncidents", new[] { "BodyPartId" });
            DropIndex("ACCIDENT.AccidentIncidents", new[] { "AccidentCauseId" });
            DropIndex("ACCIDENT.AccidentIncidents", new[] { "PlantId" });
            DropTable("ACCIDENT.MonthlyManHours");
            DropTable("ACCIDENT.PlantGroups");
            DropTable("ACCIDENT.Plants");
            DropTable("ACCIDENT.InjuryTypes");
            DropTable("ACCIDENT.BodyParts");
            DropTable("ACCIDENT.AccidentIncidents");
            DropTable("ACCIDENT.AccidentCauses");
        }
    }
}
