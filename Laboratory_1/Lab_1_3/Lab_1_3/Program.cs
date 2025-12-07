namespace Lab_1_3;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Printer printer = new Printer();

        int timeSleep = 1000;
        
        printer.AddJob(new PrintJob("Іван", "Річний звіт.docx", Priority.Normal));
        Thread.Sleep(timeSleep);
        printer.AddJob(new PrintJob("Марія", "Квитки на літак.pdf", Priority.High));
        Thread.Sleep(timeSleep);
        printer.AddJob(new PrintJob("Петро", "Курсова робота_v1.docx", Priority.Normal));
        Thread.Sleep(timeSleep);
        printer.AddJob(new PrintJob("Олена", "Рецепт торта.txt", Priority.Low));
        Thread.Sleep(timeSleep);
        printer.AddJob(new PrintJob("Директор", "Наказ №123.pdf", Priority.High));
        
        printer.ProcessQueue();
        
        Console.WriteLine("\n--- Статистика друку на екрані ---");
        var stats = printer.GetStatistics();
        if (stats.Count > 0)
        {
            foreach (var log in stats)
            {
                Console.WriteLine(log);
            }
        }
        else
        {
            Console.WriteLine(">> Статистика порожня.");
        }

        Console.Write("\n>> Зберегти статистику у файл? (так/ні): ");
        if (Console.ReadLine()?.ToLower().StartsWith("т") == true)
        {
            printer.SaveStatisticsToFile();
        }

        Console.WriteLine("\n--- Роботу програми завершено. ---");
        Console.ReadKey();
    }
}