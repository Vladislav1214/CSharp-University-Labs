namespace Laboratory_6.Service
{
    public static class EnumHelper
    {
        public static List<string> GetEnumValuesWithAll<T>() where T : Enum
        {
            var itemsSource = new List<string> { "Всі" };
            var Level = GetEnumValues<T>();
            itemsSource.AddRange(Level);

            return itemsSource;
        }

        public static List<string> GetEnumValues<T>() where T : Enum
        {
            return Enum.GetNames(typeof(T)).Select(name => name.Replace('_', ' ')).ToList();
        }

        public static T GetEnumFromObject<T>(object selectedItem) where T : Enum
        {
            string enumName = selectedItem.ToString()!.Replace(' ', '_');

            return (T)Enum.Parse(typeof(T), enumName, ignoreCase: true);
        }

        public static T? GetEnumFromComboBox<T>(int selectedIndex, object? selectedItem) where T : struct, Enum
        {
            if (selectedIndex <= 0 || selectedItem == null)
                return null;

            string? selectedString = selectedItem.ToString()?.Replace(' ', '_');

            if (selectedString != null)
                if (Enum.TryParse<T>(selectedString, out T result))
                    return result;

            return null;
        }
    }
}
