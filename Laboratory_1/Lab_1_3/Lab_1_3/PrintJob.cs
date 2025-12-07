namespace Lab_1_3;

public enum Priority
{
    Low = 0,
    Normal = 1,
    High = 2
}

public class PrintJob : IComparable<PrintJob>
{
    public string UserName { get; }
    public string DocumentName { get; }
    public Priority Priority { get; }
    public DateTime Timestamp { get; }

    public PrintJob(string userName, string documentName, Priority priority)
    {
        UserName = userName;
        DocumentName = documentName;
        Priority = priority;
        Timestamp = DateTime.Now;
    }
    
    public int CompareTo(PrintJob? other)
    {
        if (other == null) return 1;

        int priorityComparison = other.Priority.CompareTo(this.Priority);
        
        if (priorityComparison == 0)
        {
            return this.Timestamp.CompareTo(other.Timestamp);
        }

        return priorityComparison;
    }

    public override string ToString()
    {
        return $"'{DocumentName}' (Користувач: {UserName}, Пріоритет: {Priority}, Час: {Timestamp:HH:mm:ss})";
    }
}