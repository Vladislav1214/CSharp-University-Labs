namespace Lab_1_1;

public class DoctorAppointmentUtility
{
    private static readonly Random Rand = new Random();

    private static readonly string[] DoctorNames = [ 
        "Іванов Іван Іванович", "Петрова Анна Сергіївна", "Сидоренко Олег Петрович", 
        "Коваленко Марія Іванівна", "Шевченко Тарас Григорович", "Мельник Віктор Степанович", 
        "Бондаренко Ірина Олексіївна", "Ткаченко Олександр Андрійович", "Ковальчук Наталія Володимирівна", 
    ];
    private static readonly string[] Qualifications = [ 
        "Терапевт", "Хірург", "Педіатр", "Кардіолог", "Невролог", 
        "Гастроентеролог", "Офтальмолог", "Отоларинголог", 
        "Дерматолог", "Алерголог", "Ортопед", "Психіатр", "Стоматолог"
    ];
    private static readonly string[] PatientNames = [
        "Козлов Микола", "Савчук Олена", "Бондаренко Ігор", "Лисенко Юлія", 
        "Мороз Андрій", "Ковальчук Тетяна", "Шевчук Василь", "Дубовий Сергій", 
        "Мельник Оксана", "Пономаренко Роман", "Кравченко Ліна", "Захарчук Євген", 
    ];
    private static DoctorAppointment CreateRandomAppointment(int doctorIndex)
    {
        // Обираємо випадкових 0-4 пацієнтів
        List<string> registeredPatients = new List<string>();
        for (int i = 0; i < Rand.Next(0, 5); i++)
        {
            registeredPatients.Add(PatientNames[Rand.Next(PatientNames.Length)]);
        }

        // Створюємо об'єкт запису до лікаря
        DoctorAppointment appointment = new DoctorAppointment(
            DoctorNames[Rand.Next(DoctorNames.Length)],
            Qualifications[Rand.Next(Qualifications.Length)],
            DateTime.Today.AddDays(Rand.Next(1, 30)).AddHours((doctorIndex) % 2 == 0 ? Rand.Next(8, 13) : Rand.Next(13, 18)),
            Rand.Next(1, 20),
            Rand.Next(10, 31),
            registeredPatients
        );
        
        return appointment;
    }

    public static DoctorAppointment CreateAppointment()
    {
        return CreateRandomAppointment(1);
    }
    
    public static List<DoctorAppointment> CreateAppointmentArray(int doctorCount)
    {
        List<DoctorAppointment> appointments = new List<DoctorAppointment>();
        for (int i = 0; i < doctorCount; i++)
        {
            appointments.Add(CreateRandomAppointment(i)); 
        }
        return appointments;
    }
}