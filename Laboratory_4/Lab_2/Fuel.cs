namespace Lab_2
{
    public class Fuel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Додамо перевизначений метод ToString(), щоб ComboBox красиво відображав назву.
        public override string ToString()
        {
            return Name;
        }
    }
}
