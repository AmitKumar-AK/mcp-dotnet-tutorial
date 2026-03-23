namespace McpSitecoreAiExample;

public class Selling
{
    public int Id { get; set; }
    public string ProductName { get; set; } = "";
    public int Quantity { get; set; }
    public double PricePerUnit { get; set; }
    public double TotalPrice => Quantity * PricePerUnit;
    public DateTime SoldDate { get; set; }
    public string SupplierName { get; set; } = "";
    public string BuyerName { get; set; } = "";
    public string PaymentMethod { get; set; } = "";
    public string Remarks { get; set; } = "";
}