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
    }
}