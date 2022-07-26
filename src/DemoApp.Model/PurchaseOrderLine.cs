namespace DemoApp.Model;

public class PurchaseOrderLine
{
    public Guid OrderId { get; set; }
    public PurchaseOrder Order { get; set; }
    public int LineNumber { get; set; }
    public Product Product { get; set; }
    public decimal UnitPrice { get; set; }
    public int Qty { get; set; }
}