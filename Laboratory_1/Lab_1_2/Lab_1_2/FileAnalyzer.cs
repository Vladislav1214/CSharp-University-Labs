using System.Text;
using System.Text.RegularExpressions;

namespace Lab_1_2;

public class FileAnalyzer
{
    private readonly string _dataDirectory;
    private readonly string _resultsDirectory;
    
    public FileAnalyzer(string dataDirectory = "Data", string resultsDirectory = "Results")
    {
        _dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), dataDirectory);
        _resultsDirectory = Path.Combine(Directory.GetCurrentDirectory(), resultsDirectory);
        
        Directory.CreateDirectory(_dataDirectory);
        Directory.CreateDirectory(_resultsDirectory);
    }
    
    public void PrepareFiles()
    {
        string path1 = Path.Combine(_dataDirectory, "text1.txt");
        string path2 = Path.Combine(_dataDirectory, "text2.txt");
        string indexPath = Path.Combine(_dataDirectory, "firstFile.txt");

        if (!File.Exists(path1))
        {
            File.WriteAllText(path1, "Це перший текст. Просто текст для аналізу. Аналізу аналізу! Don't stop me now.",
                Encoding.UTF8);
        }

        if (!File.Exists(path2))
        {
            File.WriteAllText(path2,
                "Інший файл, інший текст. Тут слова не повторюються так часто, як у першому файлі.", Encoding.UTF8);
        }

        if (!File.Exists(indexPath))
        {
            File.WriteAllLines(indexPath, new string[] { "text1.txt", "text2.txt" });
        }
    }

    public string[] GetAvailableFiles()
    {
        string indexPath = Path.Combine(_dataDirectory, "firstFile.txt");
        return File.ReadAllLines(indexPath);
    }

    public IOrderedEnumerable<KeyValuePair<string, int>> Analyze(string filename)
    {
        string filePath = Path.Combine(_dataDirectory, filename);
        string text = File.ReadAllText(filePath);

        if (string.IsNullOrWhiteSpace(text))
        {
            return Enumerable.Empty<KeyValuePair<string, int>>().OrderBy(x => 1);
        }

        Dictionary<string, int> wordCounts = new Dictionary<string, int>();
        var matches = Regex.Matches(text.ToLower(), @"\b[\w']+\b");

        foreach (Match match in matches)
        {
            string word = match.Value;
            wordCounts.TryGetValue(word, out int currentCount);
            wordCounts[word] = currentCount + 1;
        }

        return wordCounts.OrderByDescending(p => p.Value);
    }

    public void SaveStatistics(string originalFilename, IOrderedEnumerable<KeyValuePair<string, int>> statistics)
    {
        string resultFilename = $"stats_for_{Path.GetFileNameWithoutExtension(originalFilename)}.txt";
        string resultPath = Path.Combine(_resultsDirectory, resultFilename);

        using (StreamWriter writer = new StreamWriter(resultPath, false, Encoding.UTF8))
        {
            writer.WriteLine($"--- Статистика для файлу: {originalFilename} ---");
            writer.WriteLine($"--- Дата аналізу: {DateTime.Now} ---");
            writer.WriteLine();

            foreach (var pair in statistics)
            {
                writer.WriteLine($"'{pair.Key}': {pair.Value} раз(и)");
            }
        }

        Console.WriteLine($">> Статистику успішно збережено у файл: {resultPath}");
    }
}