using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ITSB.AccidentDashboard.Core.Entities;

namespace ITSB.AccidentDashboard.Core.Repositories
{
    public class PlantStatusResult
    {
        public string PlantCode { get; set; }
        public string PlantName { get; set; }
        public int TotalCases { get; set; }
        public int FirstAidCases { get; set; }
        public int LostWorkDayCases { get; set; }
        public int RecordableCases { get; set; }
        public int TotalLostWorkDays { get; set; }
    }

    public class StatusByPlantRepository
    {
        public List<PlantStatusResult> GetStatus(int year)
        {
            using (var db = new EshDbContext())
            {
                var incidents = db.AccidentIncidents
                    .Include(i => i.Plant)
                    .Where(i => i.DateOfOccurrence.Year == year)
                    .ToList();

                return db.Plants.ToList().Select(p => new PlantStatusResult
                {
                    PlantCode = p.PlantCode,
                    PlantName = p.Name,
                    TotalCases = incidents.Count(i => i.PlantId == p.Id),
                    FirstAidCases = incidents.Count(i => i.PlantId == p.Id && i.IsFirstAidCase),
                    LostWorkDayCases = incidents.Count(i => i.PlantId == p.Id && i.IsLostWorkDayCase),
                    RecordableCases = incidents.Count(i => i.PlantId == p.Id && i.IsRecordableCase),
                    TotalLostWorkDays = incidents.Where(i => i.PlantId == p.Id).Sum(i => i.LostWorkDays)
                })
                .OrderBy(r => r.PlantCode)
                .ToList();
            }
        }

        public List<MonthlyRecordable> GetMonthlyBreakdown(int year)
        {
            using (var db = new EshDbContext())
            {
                var incidents = db.AccidentIncidents
                    .Where(i => i.DateOfOccurrence.Year == year)
                    .ToList();

                return Enumerable.Range(1, 12).Select(m => new MonthlyRecordable
                {
                    Month = m,
                    FirstAidCases = incidents.Count(i => i.DateOfOccurrence.Month == m && i.IsFirstAidCase),
                    LostWorkDayCases = incidents.Count(i => i.DateOfOccurrence.Month == m && i.IsLostWorkDayCase),
                    LostWorkdaysCount = incidents.Where(i => i.DateOfOccurrence.Month == m).Sum(i => i.LostWorkDays),
                    RecordableCases = incidents.Count(i => i.DateOfOccurrence.Month == m && i.IsRecordableCase)
                }).ToList();
            }
        }
    }

    public class MonthlyRecordable
    {
        public int Month { get; set; }
        public int FirstAidCases { get; set; }
        public int LostWorkDayCases { get; set; }
        public int LostWorkdaysCount { get; set; }
        public int RecordableCases { get; set; }
    }
}