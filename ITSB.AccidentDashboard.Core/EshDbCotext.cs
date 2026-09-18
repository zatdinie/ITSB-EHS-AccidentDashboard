using System.Data.Entity;
using ITSB.AccidentDashboard.Core.Entities;

namespace ITSB.AccidentDashboard.Core
{
    public class EshDbContext : DbContext
    {
        // Portal database (ESH); this module owns the ACCIDENT schema in it.
        public EshDbContext() : base("name=DefaultConnection") { }

        public DbSet<Plant> Plants { get; set; }
        public DbSet<PlantGroup> PlantGroups { get; set; }
        public DbSet<AccidentCause> AccidentCauses { get; set; }
        public DbSet<BodyPart> BodyParts { get; set; }
        public DbSet<InjuryType> InjuryTypes { get; set; }
        public DbSet<AccidentIncident> AccidentIncidents { get; set; }
        public DbSet<MonthlyManHours> MonthlyManHours { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ACCIDENT");
            modelBuilder.Entity<PlantGroup>().HasIndex(p => p.Label).IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}

