using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ITSB.AccidentDashboard.Core.Repositories
{
    public class NameCount { public string Name { get; set; } public int Count { get; set; } }

    public class AccidentAnalysisData
    {
        public List<NameCount> ByBodyPart { get; set; }
        public List<NameCount> ByCause { get; set; }
        public List<NameCount> ByInjuryType { get; set; }
        public List<NameCount> ByGender { get; set; }
        public List<NameCount> ByEmploymentStatus { get; set; }
    }

    public class AccidentAnalysisRepository
    {
        public AccidentAnalysisData GetAnalysis(int year)
        {
            using (var db = new EshDbContext())
            {
                var incidents = db.AccidentIncidents
                    .Include(i => i.BodyPart)
                    .Include(i => i.AccidentCause)
                    .Include(i => i.InjuryType)
                    .Where(i => i.DateOfOccurrence.Year == year)
                    .ToList();

                return new AccidentAnalysisData
                {
                    ByBodyPart = incidents.Where(i => i.BodyPart != null)
                        .GroupBy(i => i.BodyPart.Name)
                        .Select(g => new NameCount { Name = g.Key, Count = g.Count() }).ToList(),
                    ByCause = incidents.Where(i => i.AccidentCause != null)
                        .GroupBy(i => i.AccidentCause.Description)
                        .Select(g => new NameCount { Name = g.Key, Count = g.Count() }).ToList(),
                    ByInjuryType = incidents.Where(i => i.InjuryType != null)
                        .GroupBy(i => i.InjuryType.Name)
                        .Select(g => new NameCount { Name = g.Key, Count = g.Count() }).ToList(),
                    ByGender = incidents.GroupBy(i => i.Gender)
                        .Select(g => new NameCount { Name = g.Key, Count = g.Count() }).ToList(),
                    ByEmploymentStatus = incidents.GroupBy(i => i.EmploymentStatus)
                        .Select(g => new NameCount { Name = g.Key, Count = g.Count() }).ToList()
                };
            }
        }
        public CauseByPlantData GetCauseByPlantMatrix(int year)
        {
            using (var db = new EshDbContext())
            {
                var causeNames = db.AccidentCauses.OrderBy(c => c.Id).Select(c => c.Description).ToList();

                var incidents = db.AccidentIncidents
                    .Include(i => i.Plant)
                    .Include(i => i.AccidentCause)
                    .Where(i => i.DateOfOccurrence.Year == year)
                    .ToList();

                var plants = db.Plants.OrderBy(p => p.PlantCode).ToList();

                var rows = plants.Select(p =>
                {
                    var counts = causeNames.ToDictionary(
                        c => c,
                        c => incidents.Count(i => i.PlantId == p.Id && i.AccidentCause != null && i.AccidentCause.Description == c)
                    );
                    return new CauseByPlantRow
                    {
                        PlantCode = p.PlantCode,
                        CauseCounts = counts,
                        Total = counts.Values.Sum()
                    };
                }).ToList();

                var totals = causeNames.ToDictionary(c => c, c => rows.Sum(r => r.CauseCounts[c]));

                return new CauseByPlantData { CauseNames = causeNames, Rows = rows, Totals = totals };
            }
        }

        public MonthlyInjuryTypeData GetInjuryTypeByMonth(int year)
        {
            using (var db = new EshDbContext())
            {
                var injuryNames = db.InjuryTypes.OrderBy(i => i.Id).Select(i => i.Name).ToList();

                var incidents = db.AccidentIncidents
                    .Include(i => i.InjuryType)
                    .Where(i => i.DateOfOccurrence.Year == year)
                    .ToList();

                var rows = Enumerable.Range(1, 12).Select(m =>
                {
                    var counts = injuryNames.ToDictionary(
                        n => n,
                        n => incidents.Count(i => i.DateOfOccurrence.Month == m && i.InjuryType != null && i.InjuryType.Name == n)
                    );
                    return new MonthlyInjuryRow { Month = m, Counts = counts, RowTotal = counts.Values.Sum() };
                }).ToList();

                var colTotals = injuryNames.ToDictionary(n => n, n => rows.Sum(r => r.Counts[n]));

                return new MonthlyInjuryTypeData
                {
                    InjuryTypeNames = injuryNames,
                    Rows = rows,
                    ColumnTotals = colTotals,
                    GrandTotal = rows.Sum(r => r.RowTotal)
                };
            }
        }
    }

    public class CauseByPlantRow
    {
        public string PlantCode { get; set; }
        public Dictionary<string, int> CauseCounts { get; set; }
        public int Total { get; set; }
    }

    public class CauseByPlantData
    {
        public List<string> CauseNames { get; set; }
        public List<CauseByPlantRow> Rows { get; set; }
        public Dictionary<string, int> Totals { get; set; }
    }

    public class MonthlyInjuryRow
    {
        public int Month { get; set; }
        public Dictionary<string, int> Counts { get; set; }
        public int RowTotal { get; set; }
    }

    public class MonthlyInjuryTypeData
    {
        public List<string> InjuryTypeNames { get; set; }
        public List<MonthlyInjuryRow> Rows { get; set; }
        public Dictionary<string, int> ColumnTotals { get; set; }
        public int GrandTotal { get; set; }
    }
}