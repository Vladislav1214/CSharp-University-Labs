namespace Lab_1_3;

public class PrintLog
{
    public string UserName { get; }
    public string DocumentName { get; }
    public DateTime PrintTime { get; }

    public PrintLog(string userName, string documentName, DateTime printTime)
    {
        UserName = userName;
        DocumentName = documentName;
        PrintTime = printTime;
    }

    public override string ToString()
    {
        return $"[{PrintTime:G}] - Користувач '{UserName}' надрукував(ла) документ '{DocumentName}'";
    }
}