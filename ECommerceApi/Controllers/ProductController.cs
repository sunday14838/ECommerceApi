using Asp.Versioning;
using ECommerceApi.DTOs;
using ECommerceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerceApi.Controllers
{
    /// <summary>
    /// Manages the catalog items including searching, adding, editing, and removing products.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService service;

        /// <param name="service">The business logic service managing product inventory data.</param>
        public ProductController(ProductService service)
        {
            this.service = service;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var result = await service.GetAll();
        //    return Ok(result);
        //}


        /// <summary>
        /// Retrieves a filtered, paginated list of products based on query criteria.
        /// </summary>
        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> GetProducts([FromQuery] ProductQueryParameters parameters)
        {
            var products = await service.GetProductsAsync(parameters);

            return Ok(products);
        }

        /// <summary>
        /// Retrieves the details of a single product item using its unique identifier.
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await service.GetById(id);
            return Ok(result);
        }

        /// <summary>
        /// Creates and adds a new product item to the store catalog.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProductDto dto)
        {
            return Ok(await service.Create(dto));
        }

        /// <summary>
        /// Updates the data properties or images of an existing product.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateProductDto dto)
        {
            return Ok(await service.Update(id, dto));
        }

        /// <summary>
        /// Deletes a specific product item from the system inventory.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await service.Delete(id));
        }

    }

}
