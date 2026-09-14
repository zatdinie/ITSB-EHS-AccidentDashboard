namespace ITSB.AccidentDashboard.Core.Entities
{
    public class AccidentIncident
{
    public int Id { get; set; }
    public string WorkerName { get; set; }
    public string IcPassportNo { get; set; }
    public string EmployeeId { get; set; }
    public int PlantId { get; set; }
    public virtual Plant Plant { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public string Nationality { get; set; }
    public string EmploymentStatus { get; set; }
    public System.DateTime DateOfOccurrence { get; set; }
    public System.TimeSpan TimeOfOccurrence { get; set; }
    public string HowAccidentHappened { get; set; }

    public int? AccidentCauseId { get; set; }
    public virtual AccidentCause AccidentCause { get; set; }
    public int? BodyPartId { get; set; }
    public virtual BodyPart BodyPart { get; set; }
    public int? InjuryTypeId { get; set; }
    public virtual InjuryType InjuryType { get; set; }

    public bool IsFirstAidCase { get; set; }
    public bool IsLostWorkDayCase { get; set; }
    public bool IsRecordableCase { get; set; }
    public int LostWorkDays { get; set; }
}
}