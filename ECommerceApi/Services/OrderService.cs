using ECommerceApi.Data;
using ECommerceApi.DTOs;
using ECommerceApi.Models;

namespace ECommerceApi.Services
{
    public class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public CheckoutResponseDto Checkout(int userId)
        {
            var cartItems = _context.CartItems
                .Where(c => c.UserId == userId)
                .ToList();

            if (!cartItems.Any())
                throw new Exception("Cart is empty");

            decimal total = 0;

            foreach (var item in cartItems)
            {
                var product = _context.Products.Find(item.ProductId);

                if (product == null)
                    throw new Exception("Product not found");

                if (product.StockQuantity < item.Quantity)
                    throw new Exception($"{product.Name} is out of stock");

                total += product.Price * item.Quantity;
            }

            var order = new Order
            {
                UserId = userId,
                TotalAmount = total
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var item in cartItems)
            {
                var product = _context.Products.Find(item.ProductId)!;

                product.StockQuantity -= item.Quantity;

                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = product.Price
                };

                _context.OrderItems.Add(orderItem);
            }

            _context.CartItems.RemoveRange(cartItems);

            _context.SaveChanges();

            return new CheckoutResponseDto
            {
                OrderId = order.Id,
                TotalAmount = total
            };
        }

        public List<Order> GetUserOrders(int userId)
        {
            return _context.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToList();
        }
    }
}
