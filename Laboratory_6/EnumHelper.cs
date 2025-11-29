namespace Laboratory_6
{
    public static class EnumHelper
    {
        public static List<string> GetEnumValuesWithAll<T>() where T : Enum
        {
            var itemsSource = new List<string> { "Всі" };
            var Level = Enum.GetNames(typeof(T)).Select(name => name.Replace('_', ' '));
            itemsSource.AddRange(Level);

            return itemsSource;
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
