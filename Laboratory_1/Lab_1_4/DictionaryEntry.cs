namespace Lab_1_4;

public class DictionaryEntry
{
    public string Word { get; set; }
    public List<string> Translations { get; set; }

    public DictionaryEntry()
    {
        Word = string.Empty;
        Translations = new List<string>();
    }

    public DictionaryEntry(string word, List<string> translations)
    {
        Word = word;
        Translations = translations;
    }
}