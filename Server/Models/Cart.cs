namespace Durian.Models;

public class Cart
{
    public int Id { get; set; }
    public int UserID { get; set; }
    public decimal HargaBarang { get; set;}
    public int BanyakBarang { get; set; }
    public decimal Total { get; set; }

}