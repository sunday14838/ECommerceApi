namespace ECommerceApi.Models
{
#pragma warning disable CS1591
    public class Order
    {
#pragma warning disable CS1591
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
