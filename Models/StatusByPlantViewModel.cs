using System.Collections.Generic;
using ITSB.AccidentDashboard.Core.Repositories;

namespace ITSB.AccidentDashboard.Web.Models
{
    public class StatusByPlantViewModel
    {
        public int Year { get; set; }
        public List<PlantStatusResult> PlantStatus { get; set; }
        public List<NameCount> ByBodyPart { get; set; }
        public List<MonthlyRecordable> MonthlyBreakdown { get; set; }
    }
}