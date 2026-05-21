using ECommerceApi.Data;
using ECommerceApi.DTOs;
using ECommerceApi.Models;

namespace ECommerceApi.Services
{
    public class CartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        public string AddToCart(int userId, AddToCartDto dto)
        {
            var product = _context.Products.Find(dto.ProductId);

            if (product == null)
                throw new Exception("Product not found");

            if (product.StockQuantity < dto.Quantity)
                throw new Exception("Insufficient stock");

            var existingCartItem = _context.CartItems
                .FirstOrDefault(c =>
                    c.UserId == userId &&
                    c.ProductId == dto.ProductId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += dto.Quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                };

                _context.CartItems.Add(cartItem);
            }

            _context.SaveChanges();

            return "Item added to cart";
        }

        public List<CartItem> GetUserCart(int userId)
        {
            return _context.CartItems
                .Where(c => c.UserId == userId)
                .ToList();
        }

        public string RemoveFromCart(int userId, int cartItemId)
        {
            var cartItem = _context.CartItems
                .FirstOrDefault(c =>
                    c.Id == cartItemId &&
                    c.UserId == userId);

            if (cartItem == null)
                throw new Exception("Cart item not found");

            _context.CartItems.Remove(cartItem);

            _context.SaveChanges();

            return "Item removed";
        }
    }
}
