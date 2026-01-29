namespace RgatuSem2.Entities;

public record Book(string AuthorSurname, string Name, int Year)
{
    public override string ToString() => $"{AuthorSurname} \"{Name}\" ({Year})";
}
