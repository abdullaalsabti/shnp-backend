namespace WebApplication1.Data.Entities;

public class RestaurantCount
{
    public int UserId { get; set; }
    public int NumberOfSubscriptions { get; set; }
    public int NumberOfDeliverySubscriptions { get; set; }
    public int NumberOfPickupSubscriptions { get; set; }
    public int NumberOfDeliveredDailyOrders { get; set; }
    public int NumberOfPaidAmount { get; set; }
}
