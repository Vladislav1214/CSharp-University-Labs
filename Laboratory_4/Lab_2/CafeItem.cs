namespace Lab_2
{
    public class CafeItem
    {
        public string Name { get; set; }
        public decimal Price { get; }
        public int Quantity { get; set; }

        public CafeItem(string name, decimal price)
        {
            Name = name;
            Price = price;
            Quantity = 0;
        }
    }
}
