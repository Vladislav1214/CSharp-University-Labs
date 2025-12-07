namespace Laboratory_2.Task_2;

public class Phone
{
    public string ModelName { get; set; }
    public string Manufacturer { get; set; }
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }

    public override string ToString()
    {
        return $"Модель: {ModelName}, Виробник: {Manufacturer}, Ціна: {Price:C}, Дата випуску: {ReleaseDate:dd.MM.yyyy}";
    }
}