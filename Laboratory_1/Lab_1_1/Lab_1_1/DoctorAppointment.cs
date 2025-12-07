namespace Lab_1_1;

[Serializable]
public class DoctorAppointment : ICloneable
{
    public string? DoctorFullName { get; set; }
    public string? DoctorQualification { get; set; }
    public DateTime VisitDate { get; set; }
    public int OfficeNumber { get; set; }
    public int PatientExaminationTime { get; set; }
    public List<string> RegisteredPatients { get; set; }

    public DoctorAppointment()
    {
        RegisteredPatients = new List<string>();
    }
    
    public DoctorAppointment(string doctorFullName, string doctorQualification, DateTime visitDate, int officeNumber, 
        int patientExaminationTime, List<string> registeredPatients)
    {
        DoctorFullName = doctorFullName;
        DoctorQualification = doctorQualification;
        VisitDate = visitDate;
        OfficeNumber = officeNumber;
        PatientExaminationTime = patientExaminationTime;
        RegisteredPatients = new List<string>(registeredPatients);
    }
    
    public object Clone()
    {
        DoctorAppointment clone = (DoctorAppointment)this.MemberwiseClone();
        clone.RegisteredPatients = new List<string>(this.RegisteredPatients);
        return clone;
    }

    public override string ToString()
    {
        string patients = RegisteredPatients.Count > 0 ? string.Join(", ", RegisteredPatients) : "пацієнтів нема";
        
        return $"ПІБ лікаря: {DoctorFullName?? "No FullName"}, " +
               $"Кваліфікація лікаря: {DoctorQualification ?? "No Qualification"},\n" +
               $"Дата відвідування: {VisitDate:yyyy-MM-dd HH:mm}, " +
               $"Номер кабінету: {OfficeNumber}, " +
               $"Час огляду пацієнта: {PatientExaminationTime} хвилин,\n" +
               $"Записані пацієнти: {patients}";
    }
}