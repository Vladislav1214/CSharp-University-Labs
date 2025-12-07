using System.Collections.Generic;

namespace Lab_2
{
    public class BestOilModel
    {
        public List<Fuel> Fuels { get; private set; }
        public List<CafeItem> CafeItems { get; private set; }

        public decimal DailyIncome { get; private set; }

        public BestOilModel()
        {
            Fuels = new List<Fuel>
            {
                new Fuel { Name = "А-95", Price = 55.40m },
                new Fuel { Name = "А-92", Price = 52.10m },
                new Fuel { Name = "ДП", Price = 58.90m },
                new Fuel { Name = "Газ", Price = 28.50m }
            };

            CafeItems = new List<CafeItem>
            {
                new CafeItem("Хот-дог", 54.00m),
                new CafeItem("Гамбургер", 75.00m),
                new CafeItem("Картопля-фрі", 45.00m),
                new CafeItem("Кока-кола", 25.00m)
            };

            DailyIncome = 0;
        }

        public decimal CalculateFuelCostByLiters(Fuel fuel, double liters)
        {
            if (fuel == null || liters < 0) return 0;
            return fuel.Price * (decimal)liters;
        }

        public decimal CalculateLitersBySum(Fuel fuel, decimal sum)
        {
            if (fuel == null || fuel.Price == 0 || sum < 0) return 0;
            return sum / fuel.Price;
        }

        public void AddToDailyIncome(decimal amount)
        {
            DailyIncome += amount;
        }
    }
}