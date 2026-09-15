using System.Data.Entity.Migrations;
using System.Linq;
using ITSB.AccidentDashboard.Core.Entities;

namespace ITSB.AccidentDashboard.Core.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ITSB.AccidentDashboard.Core.EshDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ITSB.AccidentDashboard.Core.EshDbContext context)
        {
            var groupA = new PlantGroup { Label = "P1,3,5", DisplayName = "Accident Case P1,3,5" };
            var groupB = new PlantGroup { Label = "P13,55", DisplayName = "Accident Case P13,55" };
            var groupC = new PlantGroup { Label = "P21", DisplayName = "Accident Case P21" };
            var groupD = new PlantGroup { Label = "P34", DisplayName = "Accident Case P34" };
            context.PlantGroups.AddOrUpdate(p => p.Label, groupA, groupB, groupC, groupD);
            context.SaveChanges();

            void SeedPlant(string code, string name, PlantGroup group)
            {
                var plant = context.Plants.FirstOrDefault(p => p.PlantCode == code)
                    ?? new Plant { PlantCode = code, Name = name };
                plant.PlantGroup = group;
                if (plant.Id == 0) context.Plants.Add(plant);
            }

            SeedPlant("P1", "Plant 1", groupA);
            SeedPlant("P3", "Plant 3", groupA);
            SeedPlant("P5", "Plant 5", groupA);
            SeedPlant("P13", "Plant 13", groupB);
            SeedPlant("P55", "Plant 55", groupB);
            SeedPlant("P21", "Plant 21", groupC);
            SeedPlant("P34", "Plant 34", groupD);

            context.BodyParts.AddOrUpdate(b => b.Name,
                new BodyPart { Name = "Hand" },
                new BodyPart { Name = "Head" },
                new BodyPart { Name = "Lower Limb" },
                new BodyPart { Name = "Upper Limb" },
                new BodyPart { Name = "Others" }
            );

            context.InjuryTypes.AddOrUpdate(i => i.Name,
                new InjuryType { Name = "Acute Chemical Poisoning" },
                new InjuryType { Name = "Burns" },
                new InjuryType { Name = "Concussion" },
                new InjuryType { Name = "Cuts" },
                new InjuryType { Name = "Strains/Sprains" }
            );

            context.AccidentCauses.AddOrUpdate(a => a.Description,
                new AccidentCause { Description = "Distraction/Teasing/Horseplay" },
                new AccidentCause { Description = "Unsafe Lifting" },
                new AccidentCause { Description = "Failure to use the available equipment" },
                new AccidentCause { Description = "Unguarded Hazard" },
                new AccidentCause { Description = "Lack of Personal Protective Equipment" },
                new AccidentCause { Description = "No training/insufficient training" },
                new AccidentCause { Description = "Servicing equipment that have power on" },
                new AccidentCause { Description = "In adequate guard" },
                new AccidentCause { Description = "Work station layout is hazardous" },
                new AccidentCause { Description = "Making a safety device inoperative" },
                new AccidentCause { Description = "Using equipment in a way it was not intended to be used" },
                new AccidentCause { Description = "Tool or equipment defective" },
                new AccidentCause { Description = "Taking unsafe position" },
                new AccidentCause { Description = "Others" }
                
            );
            context.SaveChanges();


        }
    }
}