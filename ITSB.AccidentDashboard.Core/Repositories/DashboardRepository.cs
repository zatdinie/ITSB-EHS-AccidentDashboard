using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ITSB.AccidentDashboard.Core.Entities;

namespace ITSB.AccidentDashboard.Core.Repositories
{
    public class DashboardData
    {
        public int TotalAccidentCases { get; set; }
        public int TotalFirstAidCases { get; set; }
        public int TotalLostWorkDay { get; set; }
        public int TotalRecordableCases { get; set; }
        public List<GroupResult> CasesByPlantGroup { get; set; }
        public List<PlantResult> CasesByPlant { get; set; }
        public List<BodyPartResult> CasesByBodyPart { get; set; }
        public List<int> CasesByMonth { get; set; }
    }
    public class GroupResult { public string DisplayName { get; set; } public int Count { get; set; } }
    public class PlantResult { public string PlantCode { get; set; } public int Count { get; set; } }
    public class BodyPartResult { public string Name { get; set; } public int Count { get; set; } }

    public class DashboardRepository
    {
        public DashboardData GetData(int year)
        {
            using (var db = new EshDbContext())
            {
                var incidents = db.AccidentIncidents
                    .Include(i => i.Plant.PlantGroup)
                    .Include(i => i.BodyPart)
                    .Where(i => i.DateOfOccurrence.Year == year)
                    .ToList();

                var allGroups = db.PlantGroups.ToList();

                return new DashboardData
                {
                    TotalAccidentCases = incidents.Count,
                    TotalFirstAidCases = incidents.Count(i => i.IsFirstAidCase),
                    TotalLostWorkDay = incidents.Sum(i => i.LostWorkDays),
                    TotalRecordableCases = incidents.Count(i => i.IsRecordableCase),
                    CasesByPlantGroup = allGroups.Select(g => new GroupResult
                    {
                        DisplayName = g.DisplayName,
                        Count = incidents.Count(i => i.Plant.PlantGroupId == g.Id)
                    }).ToList(),
                    CasesByPlant = incidents.GroupBy(i => i.Plant.PlantCode)
                        .Select(g => new PlantResult { PlantCode = g.Key, Count = g.Count() })
                        .OrderBy(p => p.PlantCode).ToList(),
                    CasesByBodyPart = incidents.Where(i => i.BodyPart != null)
                        .GroupBy(i => i.BodyPart.Name)
                        .Select(g => new BodyPartResult { Name = g.Key, Count = g.Count() })
                        .ToList(),
                    CasesByMonth = Enumerable.Range(1, 12)
                        .Select(m => incidents.Count(i => i.DateOfOccurrence.Month == m))
                        .ToList()
                };
            }
        }
    }
}