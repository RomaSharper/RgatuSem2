using RgatuSem2.Entities;
using static RgatuSem2.Utils.Logger;

namespace RgatuSem2.Utils;

public static class Cli
{
    public static void Run(FileResolver resolver)
    {
        while (true)
        {
            Log("\n" + new string('=', 50));
            Log("КАТАЛОГ КНИГ".PadLeft(31));
            Log(new string('=', 50));
            Log("1. Добавить книгу");
            Log("2. Удалить книгу");
            Log("3. Найти книги автора после 2010 года");
            Log("4. Показать количество книг по авторам");
            Log("5. Найти книгу(и) «Информатика»");
            Log("6. Показать все книги");
            Log("0. Выход");
            Log();

            Console.Write("Выберите действие (0-6): ");
            string? input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input)) continue;

            switch (input)
            {
                case "0":
                    Log("\nРабота завершена");
                    return;

                case "1":
                    AddBook(resolver);
                    break;

                case "2":
                    RemoveBook(resolver);
                    break;

                case "3":
                    FindBooksByAuthorAfter2010(resolver);
                    break;

                case "4":
                    ShowAuthorStatistics(resolver);
                    break;

                case "5":
                    ShowInformaticsBooks(resolver);
                    break;

                case "6":
                    ShowAllBooks(resolver);
                    break;

                default:
                    Log("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    private static void AddBook(FileResolver resolver)
    {
        Log("\n--- Добавление новой книги ---");
        Console.Write("Фамилия автора: ");
        string? author = Console.ReadLine()?.Trim();

        Console.Write("Название книги: ");
        string? title = Console.ReadLine()?.Trim();

        Console.Write("Год издания: ");
        if (!int.TryParse(Console.ReadLine(), out int year))
        {
            Log("Ошибка: год должен быть числом.");
            return;
        }

        if (string.IsNullOrWhiteSpace(author) || string.IsNullOrWhiteSpace(title))
        {
            Log("Автор и название не могут быть пустыми.");
            return;
        }

        var book = new Book(author, title, year);
        Result res = resolver.AddBook(book);

        Log(res.Success
            ? $"Книга успешно добавлена: {book}"
            : $"Не удалось добавить книгу: {res.Exception?.Message ?? "Ошибка ввода/вывода."}");
    }

    private static void RemoveBook(FileResolver resolver)
    {
        Log("\n--- Удаление книги ---");
        LogInline("Фамилия автора: ");
        string? author = Console.ReadLine()?.Trim();

        LogInline("Название книги: ");
        string? title = Console.ReadLine()?.Trim();

        LogInline("Год издания: ");
        if (!int.TryParse(Console.ReadLine(), out int year))
        {
            Log("Ошибка: год должен быть числом.");
            return;
        }

        var book = new Book(author ?? "", title ?? "", year);
        Result res = resolver.RemoveBook(book);

        Log(res.Success
            ? $"Книга удалена: {book}"
            : "Книга с такими данными не найдена.");
    }

    private static void FindBooksByAuthorAfter2010(FileResolver resolver)
    {
        Log("\n--- Поиск книг автора после 2010 ---");
        Console.Write("Фамилия автора: ");
        string? author = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(author))
        {
            Log("Автор не указан.");
            return;
        }

        var titles = BookUtils.GetNamesByAuthorAfter2010(resolver.Books, author);
        Log($"\nКниги автора \"{author}\" после 2010 года:");

        if (titles.Count == 0)
        {
            Log("Не найдено.");
        }
        else
        {
            titles.ForEach(t => Log($"* {t}"));
        }
    }

    private static void ShowAuthorStatistics(FileResolver resolver)
    {
        Log("\n--- Количество книг по авторам ---");
        var stats = BookUtils.GetBooksAuthorAndCounts(resolver.Books);

        if (stats.Count == 0)
        {
            Log("Библиотека пуста.");
            return;
        }

        foreach (var (author, count) in stats)
            Log($"{author,-20} : {count,3} книг(и)");
    }

    private static void ShowInformaticsBooks(FileResolver resolver)
    {
        Log("\n--- Поиск книги «Информатика» ---");
        Log(BookUtils.GetInformaticsInfo(resolver.Books));
    }

    private static void ShowAllBooks(FileResolver resolver)
    {
        Log("\n--- Все книги в библиотеке ---");
        var books = resolver.Books;

        if (books.Count == 0)
        {
            Log("Библиотека пуста.");
            return;
        }

        books.ForEach(b => Log($"* {b}"));
    }
}
