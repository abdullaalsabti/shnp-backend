namespace WebApplication1.Data.Entities;

public class RestaurantType
{
    public int TypeId { get; set; }
    public string Name { get; set; } = string.Empty;

    //Back reference:
    public ICollection<User> Users { get; set; } = new List<User>();
}
