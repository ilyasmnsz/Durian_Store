namespace Durian.Models;

public class Order
{
    public int Id { get; set; } 
    public int UserID { get; set; }
    public int OrderNo { get; set; }
    public string? OrderTotal { get; set; }
    public string? OrderStatus { get; set; } 
}