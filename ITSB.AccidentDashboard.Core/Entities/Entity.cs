public class Plant
{
    public int Id { get; set; }
    public string Code { get; set; }   // "P5"
    public string Name { get; set; }   // "Plant 5"
}

public class AccidentCause
{
    public int Id { get; set; }
    public string Description { get; set; }
}

public class BodyPart
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class InjuryType
{
    public int Id { get; set; }
    public string Name { get; set; }
}

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

public class MonthlyManHours
{
    public int Id { get; set; }
    public int PlantId { get; set; }
    public virtual Plant Plant { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public int DirectLaborHeadcount { get; set; }
    public int DirectLaborHours { get; set; }
    public int IndirectLaborHeadcount { get; set; }
    public int IndirectLaborHours { get; set; }
}