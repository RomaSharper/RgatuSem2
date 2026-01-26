using RgatuSem2.Entities;

namespace RgatuSem2.Utils;

static class BookUtils
{
    public static List<string> GetBookNamesYearLaterThan(List<Book> books, int year = 2010)
        => [.. books.Where(book => book.Year > year).Select(book => book.Name)];

    public static Dictionary<string, int> GetBooksAuthorAndCounts(List<Book> books)
    {
        return books.GroupBy(book => book.AuthorSurname).ToDictionary(
            group => group.Key,
            group => group.Count()
        );
    }

    public static List<InformaticsBook> GetInformaticsBooks(List<Book> books)
        => [.. books.Where(book => book.Name == InformaticsBook.Name).Select(InformaticsBook.FromBook)];
}
