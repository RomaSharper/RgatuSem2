using RgatuSem2.Entities;

namespace RgatuSem2.Utils;

static class BookUtils
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

    public static List<Book> GetInformaticsBooks(List<Book> books)
        => [.. books.Where(book => book.Name == "Информатика")];
}
