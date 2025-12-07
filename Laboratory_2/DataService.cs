using Laboratory_2.Task_1;
using Laboratory_2.Task_2;
using Laboratory_2.Task_3;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Laboratory_2
{
    public static class DataService
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        public static List<Firm> LoadFirms()
        {
            string fileName = "firms.json";
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                return JsonSerializer.Deserialize<List<Firm>>(json, Options) ?? new List<Firm>();
            }

            var defaults = GetDefaultFirms();
            SaveData(fileName, defaults);
            return defaults;
        }

        public static List<Phone> LoadPhones()
        {
            string fileName = "phones.json";
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                return JsonSerializer.Deserialize<List<Phone>>(json, Options) ?? new List<Phone>();
            }

            var defaults = GetDefaultPhones();
            SaveData(fileName, defaults);
            return defaults;
        }

        public static Company LoadCompany()
        {
            string fileName = "company.json";
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                return JsonSerializer.Deserialize<Company>(json, Options) ?? new Company();
            }

            var defaults = GetDefaultCompany();
            SaveData(fileName, defaults);
            return defaults;
        }

        private static void SaveData<T>(string fileName, T data)
        {
            string json = JsonSerializer.Serialize(data, Options);
            File.WriteAllText(fileName, json);
        }

        private static List<Firm> GetDefaultFirms()
        {
            return new List<Firm>
            {
                new Firm { Name = "Google", FoundationDate = new DateTime(1998, 9, 4), BusinessProfile = "IT", DirectorFullName = "Sundar Pichai", EmployeeCount = 140000, Address = "Mountain View, USA" },
                new Firm { Name = "Food Corp", FoundationDate = new DateTime(2020, 5, 15), BusinessProfile = "Food", DirectorFullName = "John Smith", EmployeeCount = 250, Address = "London, UK" },
                new Firm { Name = "Marketing Pro", FoundationDate = new DateTime(2015, 2, 20), BusinessProfile = "Marketing", DirectorFullName = "Jane Doe", EmployeeCount = 50, Address = "New York, USA" },
                new Firm { Name = "Apple", FoundationDate = new DateTime(1976, 4, 1), BusinessProfile = "IT", DirectorFullName = "Tim Cook", EmployeeCount = 150000, Address = "Cupertino, USA" },
                new Firm { Name = "London Food", FoundationDate = new DateTime(2023, 8, 1), BusinessProfile = "Food", DirectorFullName = "Peter White", EmployeeCount = 120, Address = "London, UK" },
                new Firm { Name = "Ad Wizards", FoundationDate = new DateTime(2024, 1, 10), BusinessProfile = "Marketing", DirectorFullName = "Emily Jones", EmployeeCount = 80, Address = "London, UK" },
                new Firm { Name = "White & Black Inc", FoundationDate = new DateTime(2021, 11, 30), BusinessProfile = "Consulting", DirectorFullName = "Adam Black", EmployeeCount = 20, Address = "Chicago, USA" }
            };
        }

        private static List<Phone> GetDefaultPhones()
        {
            return new List<Phone>
            {
                new Phone { ModelName = "iPhone 14 Pro", Manufacturer = "Apple", Price = 999.99m, ReleaseDate = new DateTime(2022, 9, 16) },
                new Phone { ModelName = "Galaxy S23", Manufacturer = "Samsung", Price = 799.50m, ReleaseDate = new DateTime(2023, 2, 17) },
                new Phone { ModelName = "Pixel 7", Manufacturer = "Google", Price = 599.00m, ReleaseDate = new DateTime(2022, 10, 13) },
                new Phone { ModelName = "iPhone 13", Manufacturer = "Apple", Price = 699.00m, ReleaseDate = new DateTime(2021, 9, 24) },
                new Phone { ModelName = "Galaxy A54", Manufacturer = "Samsung", Price = 449.99m, ReleaseDate = new DateTime(2023, 3, 24) },
                new Phone { ModelName = "Xperia 5 IV", Manufacturer = "Sony", Price = 999.00m, ReleaseDate = new DateTime(2022, 9, 1) },
                new Phone { ModelName = "iPhone SE (3rd gen)", Manufacturer = "Apple", Price = 429.00m, ReleaseDate = new DateTime(2022, 3, 18) },
                new Phone { ModelName = "Galaxy Z Fold 4", Manufacturer = "Samsung", Price = 1799.99m, ReleaseDate = new DateTime(2022, 8, 25) },
                new Phone { ModelName = "Pixel 6a", Manufacturer = "Google", Price = 349.00m, ReleaseDate = new DateTime(2022, 7, 28) },
                new Phone { ModelName = "Nokia G22", Manufacturer = "Nokia", Price = 179.99m, ReleaseDate = new DateTime(2023, 2, 25) },
                new Phone { ModelName = "Galaxy S22", Manufacturer = "Samsung", Price = 650.00m, ReleaseDate = new DateTime(2022, 2, 25) }
            };
        }

        private static Company GetDefaultCompany()
        {
            return new Company
            {
                Name = "Global Innovations Inc.",
                Employees = new List<Employer>
                {
                    new President { FirstName = "Арнольд", LastName = "Шварценеггер", BirthDate = new DateTime(1947, 7, 30), Position = "Президент", Salary = 10000, StartDate = new DateTime(2005, 1, 1), HasHigherEducation = true },
                    new Manager { FirstName = "Сара", LastName = "Коннор", BirthDate = new DateTime(1985, 5, 10), Position = "Головний менеджер", Salary = 4500, StartDate = new DateTime(2015, 3, 12), HasHigherEducation = true },
                    new Manager { FirstName = "Джон", LastName = "Рембо", BirthDate = new DateTime(1978, 10, 25), Position = "Менеджер проектів", Salary = 4000, StartDate = new DateTime(2018, 7, 20), HasHigherEducation = false },
                    new Worker { FirstName = "Елен", LastName = "Ріплі", BirthDate = new DateTime(1990, 1, 7), Position = "Інженер", Salary = 2500, StartDate = new DateTime(2012, 6, 1), HasHigherEducation = true },
                    new Worker { FirstName = "Володимир", LastName = "Дантес", BirthDate = new DateTime(1988, 6, 28), Position = "Розробник", Salary = 3000, StartDate = new DateTime(2019, 9, 1), HasHigherEducation = true },
                    new Worker { FirstName = "Кайл", LastName = "Різ", BirthDate = new DateTime(2002, 11, 5), Position = "Тестувальник", Salary = 1500, StartDate = new DateTime(2023, 2, 10), HasHigherEducation = false },
                    new Worker { FirstName = "Володимир", LastName = "Зеленський", BirthDate = new DateTime(1978, 1, 25), Position = "Юрист", Salary = 3200, StartDate = new DateTime(2010, 8, 15), HasHigherEducation = true },
                    new Worker { FirstName = "Марія", LastName = "Примаченко", BirthDate = new DateTime(1995, 10, 12), Position = "Дизайнер", Salary = 2800, StartDate = new DateTime(2021, 5, 25), HasHigherEducation = true },
                    new Worker { FirstName = "Індіана", LastName = "Джонс", BirthDate = new DateTime(1965, 7, 1), Position = "Археолог", Salary = 3500, StartDate = new DateTime(2008, 11, 11), HasHigherEducation = true },
                    new Worker { FirstName = "Люк", LastName = "Скайвокер", BirthDate = new DateTime(1999, 4, 3), Position = "Інженер", Salary = 2600, StartDate = new DateTime(2022, 1, 18), HasHigherEducation = true },
                    new Worker { FirstName = "Лея", LastName = "Органа", BirthDate = new DateTime(1999, 4, 3), Position = "Аналітик", Salary = 2700, StartDate = new DateTime(2014, 10, 1), HasHigherEducation = true },
                    new Worker { FirstName = "Володимир", LastName = "Кличко", BirthDate = new DateTime(1976, 3, 25), Position = "Охоронець", Salary = 2000, StartDate = new DateTime(2009, 12, 1), HasHigherEducation = true }
                }
            };
        }
    }
}