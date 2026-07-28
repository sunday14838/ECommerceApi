using AutoMapper;
using ECommerceApi.DTOs;
using ECommerceApi.Models;
using ECommerceApi.Repositories.Interfaces;
using ECommerceApi.Services;
using ECommerceApi.Services.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IImageService> _imageServiceMock;
        private readonly Mock<ILogger<ProductService>> _loggerMock;
        private readonly Mock<IMemoryCache> _cacheMock;

        private readonly ProductService _service;
        public ProductServiceTests()
        {
            _repositoryMock = new Mock<IProductRepository>();
            _mapperMock = new Mock<IMapper>();
            _imageServiceMock = new Mock<IImageService>();
            _loggerMock = new Mock<ILogger<ProductService>>();
            _cacheMock = new Mock<IMemoryCache>();

            _service = new ProductService(
                _repositoryMock.Object,
                _mapperMock.Object,
                _imageServiceMock.Object,
                _loggerMock.Object,
                _cacheMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 1200
            };

            var dto = new ProductResponseDto
            {
                Id = 1,
                Name = "Laptop",
                Price = 1200
            };

            _repositoryMock
                .Setup(x => x.GetById(1))
                .ReturnsAsync(product);

            _mapperMock
                .Setup(x => x.Map<ProductResponseDto>(product))
                .Returns(dto);

            var mockCacheEntry = new Mock<ICacheEntry>();
            _cacheMock
                .Setup(m => m.CreateEntry(It.IsAny<object>()))
                .Returns(mockCacheEntry.Object);

            // Act
            var result = await _service.GetById(1);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("Laptop");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowException_WhenProductDoesNotExist()
        {
            // Arrange
            _repositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync((Product?)null);

            // Act
            Func<Task> action = async () =>
                await _service.GetById(1);

            // Assert
            await action.Should()
                .ThrowAsync<Exception>()
                .WithMessage("Product not found.");
        }

        [Fact]
        public async Task Create_ShouldSaveProduct()
        {
            // Arrange
            var dto = new CreateProductDto
            {
                Name = "Laptop",
                Price = 1200,
                Description = "Gaming Laptop",
                StockQuantity = 5
            };

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                StockQuantity = dto.StockQuantity
            };

            _mapperMock
                .Setup(x => x.Map<Product>(dto))
                .Returns(product);

            // Act
            await _service.Create(dto);

            // Assert
            _repositoryMock.Verify(
                x => x.Add(It.IsAny<Product>()),
                Times.Once);

            _repositoryMock.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldDeleteProduct()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Laptop"
            };

            _repositoryMock
                .Setup(x => x.GetById(1))
                .ReturnsAsync(product);

            await _service.Delete(1);

            _repositoryMock.Verify(
                x => x.Delete(product),
                Times.Once);

            _repositoryMock.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }
    }
}
