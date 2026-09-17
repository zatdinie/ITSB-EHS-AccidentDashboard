using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ITSB.AccidentDashboard.Core.Repositories
{
    public class MonthlyRate
    {
        public int Month { get; set; }
        public int TotalHours { get; set; }
        public int LostWorkDayCases { get; set; }
        public int RecordableCases { get; set; }
        public double Lwdi { get; set; }
        public double Tcir { get; set; }
    }

    public class LwrRepository
    {
        private const int OshaBase = 200000;

        public List<MonthlyRate> GetRates(int year)
        {
            using (var db = new EshDbContext())
            {
                var incidents = db.AccidentIncidents
                    .Where(i => i.DateOfOccurrence.Year == year)
                    .ToList();
                var hours = db.MonthlyManHours
                    .Where(m => m.Year == year)
                    .ToList();

                var result = new List<MonthlyRate>();
                for (int m = 1; m <= 12; m++)
                {
                    var monthHours = hours.Where(h => h.Month == m)
                        .Sum(h => h.DirectLaborHours + h.IndirectLaborHours);
                    var lwdCases = incidents.Count(i => i.DateOfOccurrence.Month == m && i.IsLostWorkDayCase);
                    var recCases = incidents.Count(i => i.DateOfOccurrence.Month == m && i.IsRecordableCase);

                    result.Add(new MonthlyRate
                    {
                        Month = m,
                        TotalHours = monthHours,
                        LostWorkDayCases = lwdCases,
                        RecordableCases = recCases,
                        Lwdi = monthHours > 0 ? (double)lwdCases * OshaBase / monthHours : 0,
                        Tcir = monthHours > 0 ? (double)recCases * OshaBase / monthHours : 0
                    });
                }
                return result;
            }
        }
    }
}