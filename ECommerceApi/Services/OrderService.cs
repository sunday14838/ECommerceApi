using ECommerceApi.Data;
using ECommerceApi.DTOs;
using ECommerceApi.Models;
using ECommerceApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerceApi.Services
{
#pragma warning disable CS1591
    public class OrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICartRepository cartRepository;
        private readonly IProductRepository productRepository;
        private readonly IOrderItemRepository orderItemRepository;
        private readonly AppDbContext context;
        private readonly ILogger<OrderService> logger;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository,
            IProductRepository productRepository, 
            IOrderItemRepository orderItemRepository, 
            AppDbContext context,
            ILogger<OrderService> logger)
        {
            this.orderRepository = orderRepository;
            this.cartRepository = cartRepository;
            this.productRepository = productRepository;
            this.orderItemRepository = orderItemRepository;
            this.context = context;
            this.logger = logger;
        }

        public async Task<CheckoutResponseDto> Checkout(int userId)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            
            logger.LogInformation("User {UserId} started checkout", userId);

            try
            {
                var cartItems = await cartRepository.GetUserCart(userId);

                if (!cartItems.Any())
                    throw new Exception("Cart is empty");

                decimal total = 0;

                foreach (var item in cartItems)
                {
                    var product = await productRepository.GetByIdAsync(item.ProductId);

                    logger.LogInformation("Processing product {ProductName}", product?.Name);

                    if (product == null)
                        throw new Exception("Product not found");

                    if (product.StockQuantity < item.Quantity)
                        throw new Exception($"{product.Name} is out of stock");

                    total += product.Price * item.Quantity;

                    logger.LogInformation("Stock updated for product {ProductName}", product.Name);
                }

                var order = new Order
                {
                    UserId = userId,
                    TotalAmount = total
                };

                await orderRepository.Add(order);
                await orderRepository.SaveChangesAsync();


                foreach (var item in cartItems)
                {
                    var product = await productRepository.GetByIdAsync(item.ProductId);

                    if (product == null)
                    {
                        throw new Exception($"Product with ID {item.ProductId} was not found.");
                    }

                    product.StockQuantity -= item.Quantity;

                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = product.Price
                    };

                    await orderItemRepository.Add(orderItem);
                }

                cartRepository.DeleteRange(cartItems);

                await cartRepository.SaveChangesAsync();
                logger.LogInformation("Checkout completed successfully for user {UserId}", userId);

                await transaction.CommitAsync();

                return new CheckoutResponseDto
                {
                    OrderId = order.Id,
                    TotalAmount = total
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Checkout failed for user {UserId}", userId);
                await transaction.RollbackAsync();
                throw;
            }

        }



        public async Task<List<Order>> GetUserOrders(int userId)
        {
            return await orderRepository.GetUserOrders(userId);
        }
    }
}
