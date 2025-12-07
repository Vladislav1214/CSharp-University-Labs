using System.Text.Json;

namespace Lab_1_1;

public class DoctorAppointmentSerializer
{

    private static string GetFileName()
    {
        Console.Write("Введіть назву файлу: ");
        string filename = Console.ReadLine() + ".json";
        
        string filePath = Path.Combine("..", "..", "..", "Data", filename);
        return filePath;
    }
    
    public static void SerializeJSON(List<DoctorAppointment> appointments)
    {
        string filePath = GetFileName();

        try
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                JsonSerializer.Serialize(fs, appointments, new JsonSerializerOptions { WriteIndented = true });
            }
            Console.WriteLine($"\n>> Дані успішно серіалізовано до файлу {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($">> Помилка під час збереження файлу: {ex.Message}");
        }
    }
    
    public static List<DoctorAppointment>? DeserializeJSON(string? filePath)
    {
        bool isInteractive = (filePath == null);

        if (isInteractive)
            GetFileName();

        if (!File.Exists(filePath))
        {
            if (isInteractive)
                Console.WriteLine($">> Помилка: Файл {filePath} не знайдено.");
            
            return null;
        }

        try
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                var loadedAppointments = JsonSerializer.Deserialize<List<DoctorAppointment>>(fs);
                return loadedAppointments;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($">> Помилка під час читання файлу: {ex.Message}");
            return null;
        }
    }
}