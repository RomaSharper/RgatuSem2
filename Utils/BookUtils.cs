using RgatuSem2.Entities;

namespace RgatuSem2.Utils;

public static class BookUtils
{
    public static List<string> GetNamesByAuthorAfter2010(List<Book> books, string author)
    {
        return [.. books.Where(b => b.AuthorSurname == author && b.Year > 2010).Select(b => b.Name)];
    }

    public static Dictionary<string, int> GetBooksAuthorAndCounts(List<Book> books)
    {
        return books.GroupBy(book => book.AuthorSurname).ToDictionary(
            group => group.Key,
            group => group.Count()
        );
    }

    public static string GetInformaticsInfo(List<Book> books)
    {
        var matches = books.Where(b => b.Name == "Информатика").Select(b => $"{b.AuthorSurname}, {b.Year}").ToList();

        return matches.Count switch
        {
            1 => $"Найдена книга \"Информатика\":\n{matches[0]}",
            > 1 => $"Найдены книги \"Информатика\":\n{string.Join("\n", matches)}",
            _ => "Книги \"Информатика\" не найдено"
        };
    }
}
