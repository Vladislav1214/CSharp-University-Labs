using System.Globalization;
using System.Text.RegularExpressions;

namespace Lab_1
{
    public static class ValidationService
    {

        public static string? ValidatePartOfName(string partOfName)
        {
            if (string.IsNullOrWhiteSpace(partOfName))
                return "Не може бути порожнім.";

            if (!Regex.IsMatch(partOfName, @"^[\p{L}'-]+$"))
                return "Може містити тільки букви, дефіси та апострофи.";

            return null;
        }

        public static string FixFullNameCase(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return fullName;

            char[] parts = fullName.ToCharArray();

            parts[0] = char.ToUpper(parts[0]);

            return string.Join("", parts);
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
        public static string? ValidateCode(string zipCode)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
                return "Код не може бути порожнім.";

            if (!zipCode.All(char.IsDigit))
                return "Код може містити тільки цифри";

            if (zipCode.Length != 6)
                return "Довжина соду має бути 6 символів";

            return null;
        }

        public static string? ValidateId(string idNumber)
        {
            if (string.IsNullOrWhiteSpace(idNumber))
                return "ID не може бути порожнім.";

            if (!idNumber.All(char.IsDigit))
                return "ID може містити тільки цифри.";

            if (idNumber.Length != 9)
                return $"Довжина має бути 9 символів.";

            return null;
        }

        public static string? ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return "Email не може бути порожнім.";

            var parts = email.Split('@');
            if (parts.Length != 2)
                return "Email повинен містити один символ '@'.";

            string localPart = parts[0];
            string domainPart = parts[1];

            if (string.IsNullOrWhiteSpace(localPart) || localPart.Length > 64)
                return "Некоректна частина email до '@'.";
            if (!Regex.IsMatch(localPart, @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+$"))
                return "Локальна частина email містить неприпустимі символи.";

            if (string.IsNullOrWhiteSpace(domainPart))
                return "Email повинен містити домен після '@'.";

            var allowedDomains = new[] { "gmail.com", "ukr.net", "outlook.com", "nlu.edu.ua" };
            if (!allowedDomains.Contains(domainPart.ToLower()))
                return $"Домен '{domainPart}' не підтримується. Дозволені: {string.Join(", ", allowedDomains)}.";

            return null;
        }


        public static string? ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return "Пароль не може бути порожнім.";
            if (password.Length < 8)
                return "Пароль має містити щонайменше 8 символів.";
            if (!password.Any(char.IsUpper))
                return "Пароль має містити хоча б одну велику літеру.";
            if (!password.Any(char.IsLower))
                return "Пароль має містити хоча б одну маленьку літеру.";
            if (!password.Any(char.IsDigit))
                return "Пароль має містити хоча б одну цифру.";

            return null;
        }
    }

}
