using System.Text;
using RgatuSem2.Entities;
using static RgatuSem2.Utils.Logger;

namespace RgatuSem2.Utils;

public class FileResolver(string filePath)
{
    private readonly string _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));

    private static void WriteBook(BinaryWriter writer, Book book)
    {
        writer.Write(book.AuthorSurname ?? string.Empty);
        writer.Write(book.Name ?? string.Empty);
        writer.Write(book.Year);
    }

    private static Book ReadBook(BinaryReader reader)
    {
        var authorSurname = reader.ReadString();
        var name = reader.ReadString();
        var year = reader.ReadInt32();
        return new Book(authorSurname, name, year);
    }

    public List<Book> Books
    {
        get
        {
            List<Book> books = [];
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
                return books;
            }

            try
            {
                using Stream stream = File.Open(_filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                using BinaryReader reader = new(stream, Encoding.UTF8);

                while (stream.Position < stream.Length)
                {
                    books.Add(ReadBook(reader));
                }
            }
            catch (Exception ex)
            {
                Log($"Ошибка чтения файла: {ex.Message}");
            }
            return books;
        }
    }

    public void SetBooks(List<Book> books)
    {
        try
        {
            using Stream stream = File.Create(_filePath);
            using BinaryWriter writer = new(stream, Encoding.UTF8);
            foreach (var book in books)
            {
                WriteBook(writer, book);
            }
        }
        catch (Exception ex)
        {
            Log($"Ошибка записи файла: {ex.Message}");
        }
    }

    public Result AddBook(Book book)
    {
        try
        {
            using Stream stream = File.Open(_filePath, FileMode.Append, FileAccess.Write);
            using BinaryWriter writer = new(stream, Encoding.UTF8);
            WriteBook(writer, book);
            return new Result(true);
        }
        catch (Exception ex)
        {
            return new Result(false, new ArgumentException("Ошибка при добавлении книги", ex));
        }
    }

    public Result RemoveBook(Book bookToRemove)
    {
        var updated = Books.Where(b => b != bookToRemove).ToList();
        if (updated.Count == Books.Count) return new Result(false);
        SetBooks(updated);
        return new Result(true);
    }
}
