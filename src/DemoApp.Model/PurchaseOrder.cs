namespace DemoApp.Model;

public class PurchaseOrder
{
    public Guid OrderId { get; set; }
    public Customer Customer { get; set; }
    public DateTime PurchaseDate { get ; set;} = DateTime.UtcNow;
    public decimal TotalPrice { get; set;}
}
