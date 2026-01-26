namespace RgatuSem2.Entities;

public record InformaticsBook(string AuthorSurname, int Year)
{
    public const string Name = "Информатика";
    public static InformaticsBook FromBook(Book book) => new(book.AuthorSurname, book.Year);
    public override string ToString() => $"{AuthorSurname} ({Year})";
}

public record Book(string AuthorSurname, string Name, int Year)
{
    public override string ToString() => $"{AuthorSurname} \"{Name}\" ({Year})";
}
