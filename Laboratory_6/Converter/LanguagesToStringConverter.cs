using Laboratory_6.Model;
using System.Globalization;
using System.Windows.Data;

namespace Laboratory_6.Converter
{
    public class LanguagesToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<Language> languages)
            {
                if (languages == null || languages.Count == 0)
                {
                    return string.Empty;
                }

                return string.Join(", ", languages.Select(lang => $"{lang.LanguageName} ({lang.FluentIn.ToString().Replace('_', ' ')})"));
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
