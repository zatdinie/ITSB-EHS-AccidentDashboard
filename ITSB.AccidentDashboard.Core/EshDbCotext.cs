using System.Data.Entity;

public class EshDbContext : DbContext
{
    public EshDbContext() : base("name=EshDb"){}

    public DbSet<Plant> Plants { get; set; }
    public DbSet<AccidentCause> AccidentCauses { get; set; }
    public DbSet<BodyPart> BodyParts { get; set; }
    public DbSet<InjuryType> InjuryTypes { get; set; }
    public DbSet<AccidentIncident> AccidentIncidents { get; set; }
    public DbSet<MonthlyManHours> MonthlyManHours { get; set; }
}