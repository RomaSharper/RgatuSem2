using RgatuSem2.Entities;
using RgatuSem2.Utils;
using static RgatuSem2.Utils.Logger;

namespace RgatuSem2;

class Program
{
    private static List<Book> Books => [
        new Book("Пушкин", "Евгений Онегин", 1833),
        new Book("Толстой", "Война и мир", 1869),
        new Book("Достоевский", "Преступление и наказание", 1866),
        new Book("Гоголь", "Мёртвые души", 1842),
        new Book("Чехов", "Вишнёвый сад", 1904),
        new Book("Тургенев", "Отцы и дети", 1862),
        new Book("Лермонтов", "Герой нашего времени", 1840),

        new Book("Глуховский", "Текст", 2017),
        new Book("Сорокин", "Теллурия", 2013),
        new Book("Рубина", "Русская канарейка", 2014),
        new Book("Трамп", "Информатика", 2018),
        new Book("Трамп", "Искусство мира", 2025),
    ];

    private static readonly FileResolver Resolver = new("file.dat");

    static void Main()
    {
        Log();
        Log("0. Запись и получение:");
        Resolver.SetBooks(Books);
        Log(Resolver.Books);

        Log("\n1. Добавление книги:");
        Log(Resolver.AddBook(Books[0]));
        Log(Resolver.AddBook(new Book("Булгаков", "Собачье сердце", 1925)));
        Log();
        Log(Resolver.Books);

        Log("\n2. Удаление книги:");
        Log(Resolver.RemoveBook(Books[0]));
        Log();
        Log(Resolver.Books);

        Log("\n3. Названия книг, изданных позднее 2010:");
        Log(BookUtils.GetBookNamesYearLaterThan(Resolver.Books));
        
        Log("\n4. Кол-во книг каждого автора:");
        Log(BookUtils.GetBooksAuthorAndCounts(Resolver.Books));

        Log($"\n5. Книга/и с названием {InformaticsBook.Name}:");
        Log(BookUtils.GetInformaticsBooks(Resolver.Books));
        Log();
    }
}
