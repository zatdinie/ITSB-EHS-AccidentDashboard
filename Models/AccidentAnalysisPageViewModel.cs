using ITSB.AccidentDashboard.Core.Repositories;

namespace ITSB.AccidentDashboard.Web.Models
{
    public class AccidentAnalysisPageViewModel
    {
        public int Year { get; set; }
        public CauseByPlantData CauseByPlant { get; set; }
        public AccidentAnalysisData Occupational { get; set; }
        public MonthlyInjuryTypeData MonthlyInjury { get; set; }
    }
}