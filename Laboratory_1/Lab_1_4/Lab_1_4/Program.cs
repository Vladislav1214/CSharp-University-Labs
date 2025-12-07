namespace Lab_1_4;

class Program
{
    static DictionaryManager manager = new DictionaryManager();

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        MainMenu();

        Console.WriteLine("\n--- Роботу програми завершено. ---");
    }

    static void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Головне меню ---");
            Console.WriteLine("1. Створити / Завантажити словник");
            Console.WriteLine("2. Вихід");
            Console.Write("Ваш вибір: ");

            switch (Console.ReadLine())
            {
                case "1":
                    SelectDictionaryMenu();
                    break;
                case "2":
                    return;
                default:
                    Console.WriteLine(">> Неправильний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }

    static void SelectDictionaryMenu()
    {
        Console.Write("Введіть тип словника (напр., en-uk, uk-en): ");
        string? type = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(type))
        {
            if (manager.LoadDictionary(type))
            {
                DictionaryActionsMenu();
            }
        }
        else
        {
            Console.WriteLine(">> Тип словника не може бути порожнім.");
        }
    }

    static void DictionaryActionsMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Меню словника ---");
            Console.WriteLine("1. Переглянути всі слова");
            Console.WriteLine("2. Додати слово/переклад");
            Console.WriteLine("3. Знайти переклад слова");
            Console.WriteLine("4. Замінити слово/переклад");
            Console.WriteLine("5. Видалити слово/переклад");
            Console.WriteLine("6. Експортувати слово");
            Console.WriteLine("7. Повернутися до головного меню");
            Console.Write("Ваш вибір: ");

            switch (Console.ReadLine())
            {
                case "1":
                    manager.DisplayAllWords();
                    break;
                case "2":
                    AddMenu();
                    break;
                case "3":
                    FindMenu();
                    break;
                case "4":
                    UpdateMenu();
                    break;
                case "5":
                    RemoveMenu();
                    break;
                case "6":
                    ExportMenu();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine(">> Неправильний вибір.");
                    break;
            }
        }
    }

    static void AddMenu()
    {
        Console.Write("Введіть слово: ");
        string? wordToAdd = Console.ReadLine();
        Console.Write("Введіть його переклад: ");
        string? translationToAdd = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(wordToAdd) && !string.IsNullOrWhiteSpace(translationToAdd))
            manager.AddWord(wordToAdd, translationToAdd);
        else
            Console.WriteLine(">> Слово та переклад не можуть бути порожніми.");
    }

    static void FindMenu()
    {
        Console.Write("Введіть слово для пошуку: ");
        string? wordToFind = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(wordToFind))
        {
            var entry = manager.FindWord(wordToFind);
            if (entry != null)
            {
                Console.WriteLine($">> Переклади для '{entry.Word}': {string.Join(", ", entry.Translations)}");
            }
            else
            {
                Console.WriteLine(">> Слово не знайдено.");
            }
        }
    }

    static void UpdateMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Меню заміни ---");
            Console.WriteLine("1. Замінити слово");
            Console.WriteLine("2. Замінити переклад");
            Console.WriteLine("3. Повернутися до меню словника");
            Console.Write("Ваш вибір: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Введіть слово, яке потрібно замінити: ");
                    string? oldWord = Console.ReadLine();
                    Console.Write("Введіть нове слово: ");
                    string? newWord = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(oldWord) && !string.IsNullOrWhiteSpace(newWord))
                    {
                        if (manager.UpdateWord(oldWord, newWord))
                            Console.WriteLine(">> Слово успішно замінено.");
                        else
                            Console.WriteLine(
                                ">> Помилка: не вдалося замінити слово (можливо, воно не існує або нове слово вже є в словнику).");
                    }

                    break;
                case "2":
                    Console.Write("Введіть слово, у якого потрібно замінити переклад: ");
                    string? wordForUpdate = Console.ReadLine();
                    Console.Write("Введіть переклад, який потрібно замінити: ");
                    string? oldTranslation = Console.ReadLine();
                    Console.Write("Введіть новий переклад: ");
                    string? newTranslation = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(wordForUpdate) && !string.IsNullOrWhiteSpace(oldTranslation) &&
                        !string.IsNullOrWhiteSpace(newTranslation))
                    {
                        if (manager.UpdateTranslation(wordForUpdate, oldTranslation, newTranslation))
                            Console.WriteLine(">> Переклад успішно замінено.");
                        else
                            Console.WriteLine(
                                ">> Помилка: не вдалося замінити переклад (можливо, слово або переклад не знайдено).");
                    }

                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine(">> Неправильний вибір.");
                    break;
            }
        }
    }

    static void RemoveMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Меню видалення ---");
            Console.WriteLine("1. Видалити слово (разом з усіма перекладами)");
            Console.WriteLine("2. Видалити конкретний переклад");
            Console.WriteLine("3. Повернутися до меню словника");
            Console.Write("Ваш вибір: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Введіть слово, яке потрібно видалити: ");
                    string? wordToRemove = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(wordToRemove))
                    {
                        if (manager.RemoveWord(wordToRemove))
                            Console.WriteLine(">> Слово та всі його переклади успішно видалено.");
                        else
                            Console.WriteLine(">> Помилка: слово не знайдено.");
                    }

                    break;
                case "2":
                    Console.Write("Введіть слово, у якого потрібно видалити переклад: ");
                    string? wordForRemove = Console.ReadLine();
                    Console.Write("Введіть переклад, який потрібно видалити: ");
                    string? translationToRemove = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(wordForRemove) && !string.IsNullOrWhiteSpace(translationToRemove))
                    {
                        if (manager.RemoveTranslation(wordForRemove, translationToRemove))
                            Console.WriteLine(">> Переклад успішно видалено.");
                        else
                            Console.WriteLine(
                                ">> Помилка: не вдалося видалити переклад (перевірте правильність вводу або чи не є він останнім).");
                    }

                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine(">> Неправильний вибір.");
                    break;
            }
        }
    }

    static void ExportMenu()
    {
        Console.Write("Введіть слово для експорту: ");
        string? wordToExport = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(wordToExport))
            manager.ExportWord(wordToExport);
    }
}