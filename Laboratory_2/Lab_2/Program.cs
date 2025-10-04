namespace Lab_2;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        bool repeat = true;
        while (repeat)
        {
            Console.Clear();
            Console.WriteLine("Меню:");
            Console.WriteLine("1 - Фірма");
            Console.WriteLine("2 - Телефон");
            Console.WriteLine("3 - Підприємство");
            Console.WriteLine("0 - Вихід");
            Console.Write("Ваш вибір: ");
            if (int.TryParse(Console.ReadLine(), out int number))
                switch (number)
                {
                    case 1:
                    {
                        Task_1();
                        break;
                    }
                    case 2:
                    {
                        Task_2();
                        break;
                    }
                    case 3:
                    {
                        Task_3();
                        break;
                    }
                    case 0:
                    {
                        repeat = false;
                        Console.WriteLine("Роботу завершено");
                        break;
                    }
                    default:
                        Console.WriteLine("Введіть число");
                        break;
                }
            Console.WriteLine("Натисніть...");
            Console.ReadKey();
        }
    }

    static void Task_1()
    {
       Console.WriteLine("--- Завдання 1: Фірми ---");
         
        List<Firm> firms = new List<Firm>
        {
            new Firm { Name = "Google", FoundationDate = new DateTime(1998, 9, 4), BusinessProfile = "IT", DirectorFullName = "Sundar Pichai", EmployeeCount = 140000, Address = "Mountain View, USA" },
            new Firm { Name = "Food Corp", FoundationDate = new DateTime(2020, 5, 15), BusinessProfile = "Food", DirectorFullName = "John Smith", EmployeeCount = 250, Address = "London, UK" },
            new Firm { Name = "Marketing Pro", FoundationDate = new DateTime(2015, 2, 20), BusinessProfile = "Marketing", DirectorFullName = "Jane Doe", EmployeeCount = 50, Address = "New York, USA" },
            new Firm { Name = "Apple", FoundationDate = new DateTime(1976, 4, 1), BusinessProfile = "IT", DirectorFullName = "Tim Cook", EmployeeCount = 150000, Address = "Cupertino, USA" },
            new Firm { Name = "London Food", FoundationDate = new DateTime(2023, 8, 1), BusinessProfile = "Food", DirectorFullName = "Peter White", EmployeeCount = 120, Address = "London, UK" },
            new Firm { Name = "Ad Wizards", FoundationDate = new DateTime(2024, 1, 10), BusinessProfile = "Marketing", DirectorFullName = "Emily Jones", EmployeeCount = 80, Address = "London, UK" },
            new Firm { Name = "White & Black Inc", FoundationDate = new DateTime(2021, 11, 30), BusinessProfile = "Consulting", DirectorFullName = "Adam Black", EmployeeCount = 20, Address = "Chicago, USA" }
        };

        Console.WriteLine("\n1. Інформація про всі фірми:");
        firms.ForEach(f => Console.WriteLine(f));

        Console.WriteLine("\n2. Фірми з назвою 'Food' (або містять це слово):");
        firms.Where(f => f.Name.Contains("Food")).ToList().ForEach(f => Console.WriteLine(f));

        Console.WriteLine("\n3. Фірми, що працюють у галузі маркетингу:");
        firms.Where(f => f.BusinessProfile == "Marketing").ToList().ForEach(f => Console.WriteLine(f));

        Console.WriteLine("\n4. Фірми, що працюють у галузі маркетингу або IT:");
        firms.Where(f => f.BusinessProfile == "Marketing" || f.BusinessProfile == "IT").ToList().ForEach(f => Console.WriteLine(f));

        Console.WriteLine("\n5. Фірми з кількістю співробітників більше 100:");
        firms.Where(f => f.EmployeeCount > 100).ToList().ForEach(f => Console.WriteLine(f));

        Console.WriteLine("\n6. Фірми з кількістю співробітників у діапазоні від 100 до 300:");
        firms.Where(f => f.EmployeeCount >= 100 && f.EmployeeCount <= 300).ToList().ForEach(f => Console.WriteLine(f));

        Console.WriteLine("\n7. Фірми, що знаходяться у Лондоні:");
        firms.Where(f => f.Address.Contains("London")).ToList().ForEach(f => Console.WriteLine(f));

        Console.WriteLine("\n8. Фірми, які мають прізвище директора 'White':");
        firms.Where(f => f.DirectorFullName.Contains("White")).ToList().ForEach(f => Console.WriteLine(f));

        Console.WriteLine("\n9. Фірми, які засновані понад два роки тому:");
        firms.Where(f => f.FoundationDate < DateTime.Now.AddYears(-2)).ToList().ForEach(f => Console.WriteLine(f));

        Console.WriteLine("\n10. Фірми, з дня заснування яких минуло більше 150 днів:");
        firms.Where(f => (DateTime.Now - f.FoundationDate).TotalDays > 150).ToList().ForEach(f => Console.WriteLine(f));

        Console.WriteLine("\n11. Фірми, у яких прізвище директора 'Black' та назва фірми містить слово 'White':");
        firms.Where(f => f.DirectorFullName.Contains("Black") && f.Name.Contains("White")).ToList().ForEach(f => Console.WriteLine(f));
    }

    static void Task_2()
    {
        Console.WriteLine("\n--- Завдання 2: Телефони ---");

        List<Phone> phones = new List<Phone>
        {
            new Phone
            {
                ModelName = "iPhone 14 Pro", Manufacturer = "Apple", Price = 999.99m,
                ReleaseDate = new DateTime(2022, 9, 16)
            },
            new Phone
            {
                ModelName = "Galaxy S23", Manufacturer = "Samsung", Price = 799.50m,
                ReleaseDate = new DateTime(2023, 2, 17)
            },
            new Phone
            {
                ModelName = "Pixel 7", Manufacturer = "Google", Price = 599.00m,
                ReleaseDate = new DateTime(2022, 10, 13)
            },
            new Phone
            {
                ModelName = "iPhone 13", Manufacturer = "Apple", Price = 699.00m,
                ReleaseDate = new DateTime(2021, 9, 24)
            },
            new Phone
            {
                ModelName = "Galaxy A54", Manufacturer = "Samsung", Price = 449.99m,
                ReleaseDate = new DateTime(2023, 3, 24)
            },
            new Phone
            {
                ModelName = "Xperia 5 IV", Manufacturer = "Sony", Price = 999.00m,
                ReleaseDate = new DateTime(2022, 9, 1)
            },
            new Phone
            {
                ModelName = "iPhone SE (3rd gen)", Manufacturer = "Apple", Price = 429.00m,
                ReleaseDate = new DateTime(2022, 3, 18)
            },
            new Phone
            {
                ModelName = "Galaxy Z Fold 4", Manufacturer = "Samsung", Price = 1799.99m,
                ReleaseDate = new DateTime(2022, 8, 25)
            },
            new Phone
            {
                ModelName = "Pixel 6a", Manufacturer = "Google", Price = 349.00m,
                ReleaseDate = new DateTime(2022, 7, 28)
            },
            new Phone
            {
                ModelName = "Nokia G22", Manufacturer = "Nokia", Price = 179.99m,
                ReleaseDate = new DateTime(2023, 2, 25)
            },
            new Phone
            {
                ModelName = "Galaxy S22", Manufacturer = "Samsung", Price = 650.00m,
                ReleaseDate = new DateTime(2022, 2, 25)
            }
        };

        Console.WriteLine("\n1. Загальна кількість телефонів:");
        Console.WriteLine(phones.Count());

        Console.WriteLine("\n2. Кількість телефонів з ціною більше 100:");
        Console.WriteLine(phones.Count(p => p.Price > 100m));

        Console.WriteLine("\n3. Кількість телефонів з ціною в діапазоні від 400 до 700:");
        Console.WriteLine(phones.Count(p => p.Price >= 400m && p.Price <= 700m));

        string manufacturerToFind = "Apple";
        Console.WriteLine($"\n4. Кількість телефонів виробника '{manufacturerToFind}':");
        Console.WriteLine(phones.Count(p => p.Manufacturer == manufacturerToFind));

        Console.WriteLine("\n5. Телефон з мінімальною ціною:");
        Console.WriteLine(phones.OrderBy(p => p.Price).First());

        Console.WriteLine("\n6. Телефон з максимальною ціною:");
        Console.WriteLine(phones.OrderByDescending(p => p.Price).First());

        Console.WriteLine("\n7. Найстаріший телефон:");
        Console.WriteLine(phones.OrderBy(p => p.ReleaseDate).First());

        Console.WriteLine("\n8. Найновіший телефон:");
        Console.WriteLine(phones.OrderByDescending(p => p.ReleaseDate).First());

        Console.WriteLine("\n9. Середня ціна телефону:");
        decimal averagePrice = phones.Average(p => p.Price);
        Console.WriteLine(averagePrice.ToString("C"));

        Console.WriteLine("\n10. П’ять найдорожчих телефонів:");
        phones.OrderByDescending(p => p.Price).Take(5).ToList().ForEach(p => Console.WriteLine(p));

        Console.WriteLine("\n11. П’ять найдешевших телефонів:");
        phones.OrderBy(p => p.Price).Take(5).ToList().ForEach(p => Console.WriteLine(p));

        Console.WriteLine("\n12. Три найстаріші телефони:");
        phones.OrderBy(p => p.ReleaseDate).Take(3).ToList().ForEach(p => Console.WriteLine(p));

        Console.WriteLine("\n13. Три найновіші телефони:");
        phones.OrderByDescending(p => p.ReleaseDate).Take(3).ToList().ForEach(p => Console.WriteLine(p));

        Console.WriteLine("\n14. Статистика щодо кількості телефонів кожного виробника:");
        var manufacturerStats = phones.GroupBy(p => p.Manufacturer);
        foreach (var group in manufacturerStats)
        {
            Console.WriteLine($"- {group.Key}: {group.Count()}");
        }

        Console.WriteLine("\n15. Статистика щодо кількості моделей телефонів:");
        var modelStats = phones.GroupBy(p => p.ModelName);
        foreach (var group in modelStats)
        {
            Console.WriteLine($"- {group.Key}: {group.Count()}");
        }

        Console.WriteLine("\n16. Статистика телефонів за роками:");
        var yearStats = phones.GroupBy(p => p.ReleaseDate.Year);
        foreach (var group in yearStats)
        {
            Console.WriteLine($"- {group.Key}: {group.Count()}");
        }

    }

    static void Task_3()
    {
        Console.WriteLine("--- Завдання 1: Підприємство ---");

        var company = new Company
        {
            Name = "Global Innovations Inc.",
            Employees = new List<Employer>
            {
                new President
                {
                    FirstName = "Арнольд", LastName = "Шварценеггер", BirthDate = new DateTime(1947, 7, 30),
                    Position = "Президент", Salary = 10000, StartDate = new DateTime(2005, 1, 1),
                    HasHigherEducation = true
                },
                
                new Manager
                {
                    FirstName = "Сара", LastName = "Коннор", BirthDate = new DateTime(1985, 5, 10),
                    Position = "Головний менеджер", Salary = 4500, StartDate = new DateTime(2015, 3, 12),
                    HasHigherEducation = true
                },
                new Manager
                {
                    FirstName = "Джон", LastName = "Рембо",
                    BirthDate = new DateTime(1978, 10, 25),
                    Position = "Менеджер проектів", Salary = 4000, StartDate = new DateTime(2018, 7, 20),
                    HasHigherEducation = false
                },
                
                new Worker
                {
                    FirstName = "Елен", LastName = "Ріплі", BirthDate = new DateTime(1990, 1, 7),
                    Position = "Інженер", Salary = 2500, StartDate = new DateTime(2012, 6, 1),
                    HasHigherEducation = true
                },
                new Worker
                {
                    FirstName = "Володимир", LastName = "Дантес", BirthDate = new DateTime(1988, 6, 28),
                    Position = "Розробник", Salary = 3000, StartDate = new DateTime(2019, 9, 1),
                    HasHigherEducation = true
                },
                new Worker
                {
                    FirstName = "Кайл", LastName = "Різ", BirthDate = new DateTime(2002, 11, 5),
                    Position = "Тестувальник", Salary = 1500, StartDate = new DateTime(2023, 2, 10),
                    HasHigherEducation = false
                },
                new Worker
                {
                    FirstName = "Володимир", LastName = "Зеленський", BirthDate = new DateTime(1978, 1, 25),
                    Position = "Юрист", Salary = 3200, StartDate = new DateTime(2010, 8, 15),
                    HasHigherEducation = true
                },
                new Worker
                {
                    FirstName = "Марія", LastName = "Примаченко",
                    BirthDate = new DateTime(1995, 10, 12),
                    Position = "Дизайнер", Salary = 2800, StartDate = new DateTime(2021, 5, 25),
                    HasHigherEducation = true
                },
                new Worker
                {
                    FirstName = "Індіана", LastName = "Джонс", BirthDate = new DateTime(1965, 7, 1),
                    Position = "Археолог", Salary = 3500, StartDate = new DateTime(2008, 11, 11),
                    HasHigherEducation = true
                },
                new Worker
                {
                    FirstName = "Люк", LastName = "Скайвокер", BirthDate = new DateTime(1999, 4, 3),
                    Position = "Інженер", Salary = 2600, StartDate = new DateTime(2022, 1, 18),
                    HasHigherEducation = true
                },
                new Worker
                {
                    FirstName = "Лея", LastName = "Органа", BirthDate = new DateTime(1999, 4, 3),
                    Position = "Аналітик", Salary = 2700, StartDate = new DateTime(2014, 10, 1),
                    HasHigherEducation = true
                },
                new Worker
                {
                    FirstName = "Володимир", LastName = "Кличко", BirthDate = new DateTime(1976, 3, 25),
                    Position = "Охоронець", Salary = 2000, StartDate = new DateTime(2009, 12, 1),
                    HasHigherEducation = true
                }
            }
        };


        Console.WriteLine("\n1. Кількість робітників підприємства:");
        int workerCount = company.Employees.OfType<Worker>().Count();
        Console.WriteLine($"Загальна кількість робітників: {workerCount}");

        Console.WriteLine("\n2. Об’єм заробітної платні для всіх співробітників:");
        decimal totalSalary = company.Employees.Sum(e => e.Salary);
        Console.WriteLine($"Загальний зарплатний фонд: {totalSalary:C}");

        Console.WriteLine("\n3. Наймолодший робітник з вищою освітою серед 10 з найбільшим стажем:");
        var specialWorker = company.Employees.OfType<Worker>().OrderBy(w => w.StartDate).Take(10).
            Where(w => w.HasHigherEducation).OrderByDescending(w => w.BirthDate).FirstOrDefault();

        if (specialWorker != null)
        {
            Console.WriteLine(specialWorker);
        }
        else
        {
            Console.WriteLine("Такого співробітника не знайдено.");
        }

        Console.WriteLine("\n4. Найстарший та наймолодший менеджери:");
        var oldestManager = company.Employees.OfType<Manager>().
            OrderBy(m => m.BirthDate).FirstOrDefault();
        var youngestManager = company.Employees.OfType<Manager>().
            OrderByDescending(m => m.BirthDate).FirstOrDefault();

        if (oldestManager != null)
        {
            Console.WriteLine($"Найстарший менеджер: {oldestManager}");
        }

        if (youngestManager != null)
        {
            Console.WriteLine($"Наймолодший менеджер: {youngestManager}");
        }

        Console.WriteLine("\n5. Робітники, що народилися у жовтні:");
        var octoberWorkers = company.Employees.OfType<Worker>().
            Where(w => w.BirthDate.Month == 10).ToList();
        if (octoberWorkers.Any())
        {
            octoberWorkers.ForEach(w => Console.WriteLine(w));
        }
        else
        {
            Console.WriteLine("Робітників, народжених у жовтні, не знайдено.");
        }

        Console.WriteLine("\n6. Привітання наймолодшого Володимира:");
        var youngestVladimir = company.Employees
            .Where(e => e.FirstName == "Володимир")
            .OrderByDescending(e => e.BirthDate)
            .FirstOrDefault();

        if (youngestVladimir != null)
        {
            decimal bonus = youngestVladimir.Salary / 3;
            Console.WriteLine(
                $"Вітаємо наймолодшого співробітника на ім'я Володимир: {youngestVladimir.FirstName} " +
                $"{youngestVladimir.LastName}!");
            Console.WriteLine($"Йому нараховано премію у розмірі {bonus:C}.");
        }
        else
        {
            Console.WriteLine("Співробітників на ім'я Володимир не знайдено.");
        }
    }
}