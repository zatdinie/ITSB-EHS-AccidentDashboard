using System.Collections.Generic;

namespace ITSB.AccidentDashboard.Web.Models
{
    public class DashboardViewModel
    {
        public int Year { get; set; }
        public int TotalAccidentCases { get; set; }
        public int TotalFirstAidCases { get; set; }
        public int TotalLostWorkDay { get; set; }
        public int TotalRecordableCases { get; set; }
        public List<GroupCaseCount> CasesByPlantGroup { get; set; }
        public List<PlantCaseCount> CasesByPlant { get; set; }
        public List<BodyPartCaseCount> CasesByBodyPart { get; set; }
        public List<int> CasesByMonth { get; set; }
        public List<int> RecordableCasesByMonth { get; set; }
        public List<int> HoursWorkedByMonth { get; set; }
    }

    public class GroupCaseCount { public string DisplayName { get; set; } public int Count { get; set; }}
    public class PlantCaseCount { public string PlantCode { get; set; } public int Count { get; set; }}
    public class BodyPartCaseCount { public string Name { get; set; } public int Count { get; set; }}
}