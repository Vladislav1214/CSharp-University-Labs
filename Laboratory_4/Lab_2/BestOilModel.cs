using System.Collections.Generic;

namespace Lab_2
{
    public class BestOilModel
    {
        // Список доступних видів пального
        public List<Fuel> Fuels { get; private set; }

        // Список доступних товарів у кафе
        public List<CafeItem> CafeItems { get; private set; }

        // Загальна виручка за день
        public decimal DailyIncome { get; private set; }

        // --- Конструктор для ініціалізації даних ---

        public BestOilModel()
        {
            // Ініціалізуємо і заповнюємо списки початковими даними
            Fuels = new List<Fuel>
            {
                new Fuel { Name = "А-95", Price = 55.40m }, // m - означає, що це decimal
                new Fuel { Name = "А-92", Price = 52.10m },
                new Fuel { Name = "ДП", Price = 58.90m },
                new Fuel { Name = "Газ", Price = 28.50m }
            };

            CafeItems = new List<CafeItem>
            {
                new CafeItem("Хот-дог", 4.00m),
                new CafeItem("Гамбургер", 5.40m),
                new CafeItem("Картопля-фрі", 7.20m),
                new CafeItem("Кока-кола", 4.40m)
            };

            DailyIncome = 0;
        }

        // --- Методи для обчислень ---

        /// <summary>
        /// Розраховує вартість пального на основі літрів.
        /// </summary>
        public decimal CalculateFuelCostByLiters(Fuel fuel, double liters)
        {
            if (fuel == null || liters < 0) return 0;
            return fuel.Price * (decimal)liters;
        }

        /// <summary>
        /// Розраховує кількість літрів на основі суми.
        /// </summary>
        public decimal CalculateLitersBySum(Fuel fuel, decimal sum)
        {
            if (fuel == null || fuel.Price == 0 || sum < 0) return 0;
            return (decimal)(sum / fuel.Price);
        }
    }
}
