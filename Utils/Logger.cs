namespace RgatuSem2.Utils;

public static class Logger
{
    public static void Log<T>(T obj)
    {
        Console.WriteLine(obj);
    }

    public static void Log()
    {
        Log("");
    }

    public static void Log<T>(List<T> list)
    {
        list.ForEach(Log);
    }

    public static void Log<K, V>(Dictionary<K, V> dict) where K : notnull
    {
        foreach (var (authorSurname, count) in dict)
        {
            Log($"{authorSurname}: {count}");
        }
    }
}
