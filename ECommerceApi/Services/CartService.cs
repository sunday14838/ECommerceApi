using AutoMapper;
using ECommerceApi.Data;
using ECommerceApi.DTOs;
using ECommerceApi.Models;
using ECommerceApi.Repositories.Interfaces;
using System.Threading.Tasks;

namespace ECommerceApi.Services
{
#pragma warning disable CS1591
    public class CartService
    {
        private readonly ICartRepository cartRepository;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository, IMapper mapper)
        {
            this.cartRepository = cartRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<string> AddToCart(int userId, AddToCartDto dto)
        {
            var product = await productRepository.GetByIdAsync(dto.ProductId);

            if (product == null)
                throw new Exception("Product not found");

            if (product.StockQuantity < dto.Quantity)
                throw new Exception("Insufficient stock");

            var existingCartItem =  await cartRepository.GetItem(userId, dto.ProductId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += dto.Quantity;
            }
            else
            {
                var cartItem = mapper.Map<CartItem>(dto);
                cartItem.UserId = userId;
                await cartRepository.Add(cartItem);
            }

            await cartRepository.SaveChangesAsync();

            return "Item added to cart";
        }

        public async Task<List<CartItem>> GetUserCart(int userId)
        {
            return await cartRepository.GetUserCart(userId);
        }

        public async Task<string> RemoveFromCart(int userId, int cartItemId)
        {
            var cartItem =  await cartRepository.GetItem(userId, cartItemId);

            if (cartItem == null)
                throw new Exception("Cart item not found");

            cartRepository.Delete(cartItem);

            await cartRepository.SaveChangesAsync();

            return "Item removed";
        }
    }
}
