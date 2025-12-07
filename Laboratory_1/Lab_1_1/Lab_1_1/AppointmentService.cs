using System.Text;

namespace Lab_1_1;

public class AppointmentService
{
    private List<DoctorAppointment> _appointments;

    public AppointmentService()
    {
        _appointments = new List<DoctorAppointment>();
    }

    public AppointmentService(List<DoctorAppointment> appointments)
    {
        _appointments = new List<DoctorAppointment>(appointments);
    }

    public void AddDoctor(DoctorAppointment appointment)
    {
        _appointments.Add((DoctorAppointment)appointment.Clone());
    }

    public void AddDoctors(List<DoctorAppointment> appointments)
    {
        foreach (var appointment in appointments)
        {
            _appointments.Add((DoctorAppointment)appointment.Clone());
        }
    }

    public List<DoctorAppointment> GetAllAppointments()
    {
        return new List<DoctorAppointment>(_appointments);
    }
    
    public void UpdateAppointment(int index, DoctorAppointment updatedAppointment)
    {
        if (index >= 0 && index < _appointments.Count)
            _appointments[index] = (DoctorAppointment)updatedAppointment.Clone();
        else
        {
            Console.WriteLine("Помилка: Неправильний індекс для оновлення.");
        }
    }
    
    public void DeleteAppointment(int index)
    {
        if (index >= 0 && index < _appointments.Count)
            _appointments.RemoveAt(index);
        else
        {
            Console.WriteLine("Помилка: Неправильний індекс для видалення.");
        }
    }

    public List<DoctorAppointment> FindAppointmentsByDoctor(string doctorName)
    {
        List<DoctorAppointment> appointmentsList = _appointments.Where(appointments => appointments.DoctorFullName.Contains(doctorName)).ToList();
        return appointmentsList;
    }
    
    public List<DoctorAppointment> Appointments
    {
        get { return _appointments; }
        set { _appointments = new List<DoctorAppointment>(value); }
    }

    public override string ToString()
    {
        StringBuilder appointments = new StringBuilder();
        int i = 1;
        foreach (DoctorAppointment appointment in _appointments)
        {
            appointments.AppendLine($"Запис №{i}\n" + appointment + "\n");
            i++;
        }
        return appointments.ToString();
    }
}