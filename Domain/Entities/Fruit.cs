namespace Domain.Entities;

public class Fruit
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public Taste Taste { get; set; } = Taste.Unknown;
}

public enum Taste
{
    Unknown,
    Sweet,
    Sour,
    Bitter,
    Salty
}