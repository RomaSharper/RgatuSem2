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
        Cli.Run(Resolver);
    }
}
