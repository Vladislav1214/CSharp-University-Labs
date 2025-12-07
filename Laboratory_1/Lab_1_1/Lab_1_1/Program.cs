using System.Text;

namespace Lab_1_1;

class Program
{
    private static readonly AppointmentService AppointmentService = new AppointmentService();

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        InitializeSampleData(AppointmentService);
        
        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine("\n--- ГОЛОВНЕ МЕНЮ ---");
            Console.WriteLine("1. Переглянути всі записи");
            Console.WriteLine("2. Додати новий запис");
            Console.WriteLine("3. Редагувати запис");
            Console.WriteLine("4. Видалити запис");
            Console.WriteLine("5. Знайти записи за прізвищем лікаря");
            Console.WriteLine("6. Зберегти дані у JSON файл");
            Console.WriteLine("7. Завантажити дані з JSON файлу");
            Console.WriteLine("0. Вихід");
            Console.Write("Ваш вибір: ");
            
            int.TryParse(Console.ReadLine(), out int menuChoice);

            switch (menuChoice)
            {
                case 1:
                    ViewAllAppointments();
                    break;
                case 2:
                    ShowAddMenu();
                    break;
                case 3:
                    EditAppointment();
                    break;
                case 4:
                    DeleteAppointment();
                    break;
                case 5:
                    FindAppointmentsByDoctor();
                    break;
                case 6:
                    DoctorAppointmentSerializer.SerializeJSON(AppointmentService.GetAllAppointments());
                    break;
                case 7:
                    List<DoctorAppointment>? loadedAppointments = DoctorAppointmentSerializer.DeserializeJSON(null);
                    if (loadedAppointments != null)
                    {
                        AppointmentService.Appointments = loadedAppointments;
                        Console.WriteLine(">> Дані успішно завантажено.");
                    }
                    break;
                case 0:
                    isRunning = false;
                    Console.WriteLine(">> Завершення роботи програми.");
                    break;
                default:
                    Console.WriteLine(">> Помилка: Невірний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }

    private static void ViewAllAppointments()
    {
        List<DoctorAppointment> allAppointments = AppointmentService.GetAllAppointments();
        if (allAppointments.Count == 0)
        {
            Console.WriteLine("\n>> Список записів порожній.");
            return;
        }

        Console.WriteLine("\n--- Список усіх записів ---");
        for (int i = 0; i < allAppointments.Count; i++)
        {
            Console.WriteLine($"[{i + 1}] -----------------------------");
            Console.WriteLine(allAppointments[i]);
        }
        Console.WriteLine(new string('-', 18));
    }

    private static void ShowAddMenu()
    {
        Console.WriteLine("\n--- Меню додавання ---");
        Console.WriteLine("1. Додати один запис (вручну)");
        Console.WriteLine("2. Додати один запис (автоматично)");
        Console.WriteLine("3. Додати декілька записів (автоматично)");
        Console.Write("Ваш вибір: ");

        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            switch (choice)
            {
                case 1:
                    AddNewAppointment(); 
                    break;
                case 2:
                    AppointmentService.AddDoctor(DoctorAppointmentUtility.CreateAppointment());
                    Console.WriteLine("\n>> Один випадковий запис успішно додано!");
                    break;
                case 3:
                    Console.Write("Скільки випадкових записів додати? ");
                    if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
                    {
                        List<DoctorAppointment> newAppointments = DoctorAppointmentUtility.CreateAppointmentArray(count);
                        AppointmentService.AddDoctors(newAppointments);
                        Console.WriteLine($"\n>> {count} випадкових записів успішно додано!");
                    }
                    else
                    {
                        Console.WriteLine(">> Помилка: Введено некоректну кількість.");
                    }
                    break;
                default:
                    Console.WriteLine(">> Помилка: Невірний вибір.");
                    break;
            }
        }
        else
        {
            Console.WriteLine(">> Помилка: Введено не число.");
        }
    }
    
    private static void AddNewAppointment()
    {
        try
        {
            Console.WriteLine("\n--- Додавання нового запису ---");
            Console.Write("Введіть ПІБ лікаря: ");
            string doctorFullName = Console.ReadLine();

            Console.Write("Введіть кваліфікацію лікаря: ");
            string qualification = Console.ReadLine();

            Console.Write("Введіть дату відвідування (дд.мм.рррр): ");
            DateTime visitDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Введіть номер кабінету: ");
            int officeNumber = int.Parse(Console.ReadLine());

            Console.Write("Введіть час на огляд (в хвилинах): ");
            int examTime = int.Parse(Console.ReadLine());
            
            Console.Write("Скільки пацієнтів додати? ");
            int patientCount = int.Parse(Console.ReadLine());
            List<string> patients = new List<string>();
            for (int i = 0; i < patientCount; i++)
            {
                Console.Write($"Введіть ПІБ пацієнта №{i + 1}: ");
                patients.Add(Console.ReadLine());
            }
            
            DoctorAppointment newAppointment = new DoctorAppointment(doctorFullName, qualification, visitDate, officeNumber, examTime, patients);

            AppointmentService.AddDoctor(newAppointment);
            Console.WriteLine("\n>> Запис успішно додано!");
        }
        catch (FormatException)
        {
            Console.WriteLine(">> Помилка: Неправильний формат вводу. Спробуйте ще раз.");
        }
    }

    private static void EditAppointment()
    {
        Console.Write("\nВведіть номер запису, який хочете редагувати: ");
        if (int.TryParse(Console.ReadLine(), out int index))
        {
            int actualIndex = index - 1;
            Console.WriteLine(">> Створення нового запису для заміни...");
            DoctorAppointment updatedAppointment = DoctorAppointmentUtility.CreateAppointment();

            AppointmentService.UpdateAppointment(actualIndex, updatedAppointment);
        }
        else
        {
            Console.WriteLine(">> Помилка: Введено не число.");
        }
    }

    private static void DeleteAppointment()
    {
        Console.Write("\nВведіть номер запису, який хочете видалити: ");
        if (int.TryParse(Console.ReadLine(), out int index))
        {
            AppointmentService.DeleteAppointment(index - 1);
        }
        else
        {
            Console.WriteLine(">> Помилка: Введено не число.");
        }
    }

    private static void FindAppointmentsByDoctor()
    {
        Console.Write("\nВведіть прізвище лікаря для пошуку: ");
        string doctorName = Console.ReadLine();

        List<DoctorAppointment> results = AppointmentService.FindAppointmentsByDoctor(doctorName);

        if (results.Count == 0)
        {
            Console.WriteLine($">> Записів для лікаря, що містить '{doctorName}', не знайдено.");
            return;
        }

        Console.WriteLine($"\n--- Результати пошуку для '{doctorName}' ---");
        foreach (DoctorAppointment appointment in results)
        {
            Console.WriteLine(appointment);
        }
    }

    private static void InitializeSampleData(AppointmentService service)
    {
        string defaultFilePath = Path.Combine("..", "..", "..", "Data", "DoctorAppointments.json");
    
        List<DoctorAppointment>? loadedAppointments = DoctorAppointmentSerializer.DeserializeJSON(defaultFilePath);
        if (loadedAppointments != null && loadedAppointments.Count > 0)
        {
            service.Appointments = loadedAppointments;
            Console.WriteLine($">> Дані успішно завантажено з файлу");
        }
        else
        {
            Console.WriteLine(">> Файл за замовчуванням не знайдено або порожній. Створення випадкових даних...");
            service.Appointments = DoctorAppointmentUtility.CreateAppointmentArray(5);
        }
    }
}
