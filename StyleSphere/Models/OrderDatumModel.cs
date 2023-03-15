namespace StyleSphere.Models
{
    public class OrderDatumViewModel
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public string ShippingAddress { get; set; } = null!;

    public string BillingAddress { get; set; } = null!;

    public string TrackingId { get; set; } = null!;

    public decimal NetAmount { get; set; }

    public string ProductName { get; set; }

    public string thumbnailImage { get; set; }

    public int Quantity { get; set; }

    public string color { get; set; }

    public string EUSize { get; set; }

    public string USSize { get; set; }

    public decimal price { get; set; }

}
}