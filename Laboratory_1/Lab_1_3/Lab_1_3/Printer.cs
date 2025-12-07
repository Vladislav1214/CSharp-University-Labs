using System.Text;

namespace Lab_1_3;

public class Printer
{
    private readonly List<PrintJob> _queue = new();
    private readonly List<PrintLog> _printHistory = new();

    public void AddJob(PrintJob job)
    {
        _queue.Add(job);
        Console.WriteLine($">> Додано в чергу: {job}");
    }

    public void ProcessQueue()
    {
        Console.WriteLine("\n--- Початок друку ---");
        
        _queue.Sort();

        while (_queue.Count > 0)
        {
            PrintJob jobToPrint = _queue[0];
            
            Console.WriteLine($"-> Друкується... {jobToPrint}");
            Thread.Sleep(1000);

            _printHistory.Add(new PrintLog(jobToPrint.UserName, jobToPrint.DocumentName, DateTime.Now));
            
            _queue.RemoveAt(0);
            
            Console.WriteLine("-> Готово!");
        }
        Console.WriteLine("--- Друк завершено ---");
    }
    
    public List<PrintLog> GetStatistics()
    {
        return _printHistory;
    }
    
    public void SaveStatisticsToFile(string filename = "print_log.txt")
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), filename);
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.WriteLine("--- Статистика друку ---");
                foreach (var log in _printHistory)
                {
                    writer.WriteLine(log.ToString());
                }
            }
            Console.WriteLine($">> Статистику успішно збережено у файл: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($">> Помилка збереження файлу: {ex.Message}");
        }
    }
}