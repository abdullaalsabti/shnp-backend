namespace WebApplication1.Data.Entities;

public class WorkingDetail
{
    public int WorkingDetailId { get; set; }
    public int UserId { get; set; }
    public int Day { get; set; }
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
}
