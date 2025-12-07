using System.Globalization;
using System.Text.RegularExpressions;

namespace Laboratory_6.Service
{
    public class ValidationService
    {
        public static string? ValideFullName(string fullName)
        {
            var partsOfName = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (partsOfName.Length < 2 || partsOfName.Length > 3)
                return "ПІБ має складатися з 2 або 3 слів.";

            foreach (var part in partsOfName)
            {
                string? error = ValidatePartOfName(part);
                if (error != null)
                    return error;
            }

            return null;
        }

        public static string? ValidatePartOfName(string partOfName)
        {
            if (string.IsNullOrWhiteSpace(partOfName))
                return "Не може бути порожнім.";

            if (partOfName.Length < 2)
            {
                return "Не може мати менше 2 символів";
            }

            if (!Regex.IsMatch(partOfName, @"^[\p{L}'-]+$"))
                return "Може містити тільки букви, дефіси та апострофи.";

            return null;
        }

        public static string FixName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return fullName;

            var partsOfName = fullName.Split(' ');
            string[] formattedParts = new string[partsOfName.Length];

            for (int i = 0; i < partsOfName.Length; i++)
            {
                if (string.IsNullOrEmpty(partsOfName[i])) continue;

                char firstChar = char.ToUpper(partsOfName[i][0]);
                string restOfPart = partsOfName[i].Substring(1).ToLower();
                formattedParts[i] = firstChar + restOfPart;
            }

            return string.Join(" ", formattedParts);
        }

        public static string? ValidateWorkExperience(string experience)
        {
            if (string.IsNullOrWhiteSpace(experience))
                return "Код не може бути порожнім.";

            if (!experience.All(char.IsDigit))
                return "Код може містити тільки цифри";

            if (int.TryParse(experience, out int years) && years > 80)
                return "Стаж не може перевищувати 80 років.";

            return null;
        }

        public static string? ValidateDateOfBirth(DateTime date)
        {
            int age = DateTime.Today.Year - date.Year;

            if (age < 0)
                return $"Вік не може бути меншим за 0 років.";
            if (age > 100)
                return $"Вік не може бути більшим за 100 років.";

            return null;
        }

        public static string? ValidateDateOfBirth(string dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
                return "Будь ласка, виберіть дату народження.";

            string format = "dd.MM.yyyy";

            if (!DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfBirth))
                return $"Некоректний формат дати. Використовуйте {format}";

            int age = DateTime.Today.Year - dateOfBirth.Year;

            if (age < 0)
                return $"Вік не може бути меншим за 0 років.";
            if (age > 120)
                return $"Вік не може бути більшим за 120 років.";

            return null;
        }
    }
}
