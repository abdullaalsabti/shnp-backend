namespace WebApplication1.Data.Entities.Random;

public class Club
{
    public int PlayedMatches;
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Rating { get; set; }
    public string Description { get; set; } = string.Empty;
}
