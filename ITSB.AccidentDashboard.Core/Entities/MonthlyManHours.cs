namespace ITSB.AccidentDashboard.Core.Entities
{
    public class MonthlyManHours
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        public virtual Plant Plant { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int DirectLaborHeadcount { get; set; }
        public int DirectLaborHours { get; set; }
        public int IndirectLaborHeadcount { get; set; }
        public int IndirectLaborHours { get; set; }
    }
}