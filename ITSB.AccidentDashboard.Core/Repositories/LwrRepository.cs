using System.Collections.Generic;
using System.Linq;
using ITSB.AccidentDashboard.Core.Entities;

namespace ITSB.AccidentDashboard.Core.Repositories
{
    public class MonthlyRate
    {
        public int Month { get; set; }
        public int TotalAccidents { get; set; }        // A
        public int LostWorkDayCases { get; set; }       // B
        public int LostWorkdaysCount { get; set; }       // C
        public double SeverityRate { get; set; }         // D
        public double? TargetSeverityRate { get; set; }  // E (prior year * 0.9, null if no prior data)
        public double Lwdi { get; set; }                 // F
        public double Tcir { get; set; }                 // G

        public int DLHeadcount { get; set; }
        public int DLHours { get; set; }
        public int IDLHeadcount { get; set; }
        public int IDLHours { get; set; }
        public int TotalHours { get; set; }
        public int TotalHeadcount { get; set; }
    }

    public class LwrRepository
    {
        private const int OshaBase = 200000;

        public List<MonthlyRate> GetRates(int year)
        {
            using (var db = new EshDbContext())
            {
                var current = ComputeRatesForYear(db, year);
                var prior = ComputeRatesForYear(db, year - 1);

                for (int i = 0; i < current.Count; i++)
                {
                    current[i].TargetSeverityRate = prior[i].TotalHours > 0
                        ? (double?)(prior[i].SeverityRate * 0.9)
                        : null;
                }
                return current;
            }
        }

        private List<MonthlyRate> ComputeRatesForYear(EshDbContext db, int year)
        {
            var incidents = db.AccidentIncidents.Where(i => i.DateOfOccurrence.Year == year).ToList();
            var hours = db.MonthlyManHours.Where(h => h.Year == year).ToList();

            var result = new List<MonthlyRate>();
            for (int m = 1; m <= 12; m++)
            {
                var monthHours = hours.Where(h => h.Month == m).ToList();
                int dlHc = monthHours.Sum(h => h.DirectLaborHeadcount);
                int dlHrs = monthHours.Sum(h => h.DirectLaborHours);
                int idlHc = monthHours.Sum(h => h.IndirectLaborHeadcount);
                int idlHrs = monthHours.Sum(h => h.IndirectLaborHours);
                int totalHours = dlHrs + idlHrs;

                int totalAccidents = incidents.Count(i => i.DateOfOccurrence.Month == m);
                int lwdCases = incidents.Count(i => i.DateOfOccurrence.Month == m && i.IsLostWorkDayCase);
                int lostWorkdays = incidents.Where(i => i.DateOfOccurrence.Month == m).Sum(i => i.LostWorkDays);

                result.Add(new MonthlyRate
                {
                    Month = m,
                    TotalAccidents = totalAccidents,
                    LostWorkDayCases = lwdCases,
                    LostWorkdaysCount = lostWorkdays,
                    SeverityRate = totalHours > 0 ? (double)lostWorkdays * OshaBase / totalHours : 0,
                    Lwdi = totalHours > 0 ? (double)lwdCases * OshaBase / totalHours : 0,
                    Tcir = totalHours > 0 ? (double)totalAccidents * OshaBase / totalHours : 0,
                    DLHeadcount = dlHc,
                    DLHours = dlHrs,
                    IDLHeadcount = idlHc,
                    IDLHours = idlHrs,
                    TotalHours = totalHours,
                    TotalHeadcount = dlHc + idlHc
                });
            }
            return result;
        }
    }
}