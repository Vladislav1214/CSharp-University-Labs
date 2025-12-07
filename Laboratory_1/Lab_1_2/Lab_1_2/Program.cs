namespace Lab_1_2;

class Program
{
    static void Main(string[] args)
        {
            FileAnalyzer analyzer = new FileAnalyzer();
            analyzer.PrepareFiles();

            bool continueAnalysis = true;
            while (continueAnalysis)
            {
                try
                {
                    string[] availableFiles = analyzer.GetAvailableFiles();

                    if (availableFiles.Length == 0)
                    {
                        Console.WriteLine(">> Файл firstFile.txt порожній. Немає файлів для аналізу.");
                        break;
                    }
                    
                    Console.WriteLine("\n--- Аналіз текстових файлів ---");
                    Console.WriteLine("Доступні файли для аналізу:");
                    for (int i = 0; i < availableFiles.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {availableFiles[i]}");
                    }

                    // 4. Отримуємо вибір користувача.
                    Console.Write("Введіть номер файлу для аналізу: ");
                    if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= availableFiles.Length)
                    {
                        string selectedFile = availableFiles[choice - 1];
                        Console.WriteLine($"\n--- Статистика для файлу: {selectedFile} ---");
                        
                        var statistics = analyzer.Analyze(selectedFile);

                        if (!statistics.Any())
                        {
                            Console.WriteLine(">> Файл порожній або не містить слів для аналізу.");
                        }
                        else
                        {
                            foreach (var pair in statistics)
                            {
                                Console.WriteLine($"'{pair.Key}': {pair.Value} раз(и)");
                            }
                            
                            Console.Write("\n>> Зберегти цю статистику у файл? (так/ні): ");
                            if (Console.ReadLine()?.ToLower().StartsWith("т") == true) // "т" від "так"
                            {
                                analyzer.SaveStatistics(selectedFile, statistics);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine(">> Помилка: Неправильний вибір.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($">> Сталася помилка: {ex.Message}");
                }
                
                Console.Write("\n>> Проаналізувати ще один файл? (так/ні): ");
                if (Console.ReadLine()?.ToLower().StartsWith("т") != true)
                {
                    continueAnalysis = false;
                }
            }

            Console.WriteLine("\n--- Роботу програми завершено. Натисніть будь-яку клавішу для виходу. ---");
            Console.ReadKey();
        }
}