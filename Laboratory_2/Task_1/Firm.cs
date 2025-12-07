namespace Laboratory_2.Task_1;

public class Firm
{
    public string Name { get; set; }
    public DateTime FoundationDate { get; set; }
    public string BusinessProfile { get; set; }
    public string DirectorFullName { get; set; }
    public int EmployeeCount { get; set; }
    public string Address { get; set; }

    public override string ToString()
    {
        return $"Назва: {Name}, Профіль: {BusinessProfile}, Директор: {DirectorFullName}, Співробітників: {EmployeeCount}, Адреса: {Address}, Заснована: {FoundationDate:dd.MM.yyyy}";
    }
}