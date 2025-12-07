using System.Text;
using System.Text.Json;

namespace Lab_1_4;

public class DictionaryManager
{
    private List<DictionaryEntry> _entries = new();
    private string _currentDictionaryPath = string.Empty;

    public bool LoadDictionary(string type)
    {
        _currentDictionaryPath = $"{type}.json";
        if (!File.Exists(_currentDictionaryPath))
        {
            _entries = new List<DictionaryEntry>();
            SaveChanges();
            Console.WriteLine($">> Створено новий словник: {_currentDictionaryPath}");
        }
        else
        {
            string json = File.ReadAllText(_currentDictionaryPath);
            _entries = JsonSerializer.Deserialize<List<DictionaryEntry>>(json) ?? new List<DictionaryEntry>();
            Console.WriteLine($">> Завантажено словник: {_currentDictionaryPath}");
        }
        return true;
    }

    private void SaveChanges()
    {
        var options = new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
        string json = JsonSerializer.Serialize(_entries, options);
        File.WriteAllText(_currentDictionaryPath, json);
    }
    
    public void DisplayAllWords()
    {
        if (!_entries.Any())
        {
            Console.WriteLine(">> Словник порожній.");
            return;
        }

        Console.WriteLine("\n--- Вміст словника ---");
        int counter = 1;
        foreach (var entry in _entries.OrderBy(e => e.Word))
        {
            Console.WriteLine($"{counter}. {entry.Word} -> [{string.Join(", ", entry.Translations)}]");
            counter++;
        }
        Console.WriteLine("----------------------");
    }

    public DictionaryEntry? FindWord(string word)
    {
        return _entries.FirstOrDefault(e => e.Word.Equals(word, StringComparison.OrdinalIgnoreCase));
    }

    public void AddWord(string word, string translation)
    {
        var entry = FindWord(word);
        if (entry != null)
        {
            if (!entry.Translations.Contains(translation))
            {
                entry.Translations.Add(translation);
                Console.WriteLine(">> Переклад успішно додано.");
            }
            else
            {
                Console.WriteLine(">> Такий переклад вже існує.");
            }
        }
        else
        {
            _entries.Add(new DictionaryEntry(word, new List<string> { translation }));
            Console.WriteLine(">> Слово та його переклад успішно додано.");
        }
        SaveChanges();
    }

    public bool RemoveWord(string word)
    {
        var entry = FindWord(word);
        if (entry != null)
        {
            _entries.Remove(entry);
            SaveChanges();
            return true;
        }
        return false;
    }

    public bool RemoveTranslation(string word, string translation)
    {
        var entry = FindWord(word);
        if (entry != null)
        {
            if (entry.Translations.Count > 1)
            {
                bool removed = entry.Translations.Remove(translation);
                if (removed)
                {
                    SaveChanges();
                }
                return removed;
            }
            else
            {
                Console.WriteLine(">> Помилка: Не можна видалити останній переклад слова.");
                return false;
            }
        }
        return false;
    }

    public bool UpdateWord(string oldWord, string newWord)
    {
        var entry = FindWord(oldWord);
        if (entry != null && FindWord(newWord) == null)
        {
            entry.Word = newWord;
            SaveChanges();
            return true;
        }
        return false;
    }
    
    public bool UpdateTranslation(string word, string oldTranslation, string newTranslation)
    {
        var entry = FindWord(word);
        if (entry != null)
        {
            int index = entry.Translations.IndexOf(oldTranslation);
            if (index != -1)
            {
                entry.Translations[index] = newTranslation;
                SaveChanges();
                return true;
            }
        }
        return false;
    }

    public void ExportWord(string word)
    {
        var entry = FindWord(word);
        if (entry == null)
        {
            Console.WriteLine(">> Слово не знайдено.");
            return;
        }

        string filename = $"{word}_export.txt";
        try
        {
            using (StreamWriter writer = new StreamWriter(filename, false, Encoding.UTF8))
            {
                writer.WriteLine($"Слово: {entry.Word}");
                writer.WriteLine("Переклади:");
                foreach (var translation in entry.Translations)
                {
                    writer.WriteLine($"- {translation}");
                }
            }
            Console.WriteLine($">> Слово успішно експортовано у файл: {filename}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($">> Помилка експорту: {ex.Message}");
        }
    }
}