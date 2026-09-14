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
            context.SaveChanges();
        }
    }
}