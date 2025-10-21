namespace Lab_2
{
    public class CafeItem
    {
        public string Name { get; set; }
        public decimal Price { get; } // Ціна незмінна, тому set можна прибрати
        public int Quantity { get; set; }

        // Конструктор, щоб зручно створювати товари
        public CafeItem(string name, decimal price)
        {
            Name = name;
            Price = price;
            Quantity = 0; // За замовчуванням кількість 0
        }
    }
}
