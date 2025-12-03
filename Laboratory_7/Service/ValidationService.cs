using System.Text.RegularExpressions;

namespace Laboratory_7.Service
{
    public class ValidationService
    {
        public static string? ValidatePartOfName(string partOfName)
        {
            if (string.IsNullOrWhiteSpace(partOfName))
                return "Не може бути порожнім.";

            if (!Regex.IsMatch(partOfName, @"^[\p{L}'-]+$"))
                return "Може містити тільки букви, дефіси та апострофи.";

            if (partOfName.Length < 2)
                return "Повинно мати більше 2 символів";

            return null;
        }

        public static string FixFullNameCase(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return fullName;

            char[] parts = fullName.ToCharArray();

            parts[0] = char.ToUpper(parts[0]);

            return string.Join("", parts);
        }

        public static string? ValidateAge(int? age)
        {
            if (age == null)
                return "Вік не може бути пустим";

            if (age < 16 || age > 100)
                return "Вік має бути в діапазоні [16, 100].";

            return null;
        }
    }
}
