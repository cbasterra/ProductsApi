using Microsoft.AspNetCore.Mvc;
using ProductsApi.Models.DTOs.Requests;
using ProductsApi.Models.DTOs.Responses;
using ProductsApi.Services.Interfaces;

namespace ProductsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            List<ProductForReadDto> productos = _service.GetAllProducts();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            ProductForReadDto? producto = _service.GetProductById(id);

            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductForCreateDto dto)
        {
            if (_service.ProductNameExists(dto.Name))
                return Conflict("Ya existe un producto con ese nombre.");

            ProductForReadDto creado = _service.CreateProduct(dto);
            return CreatedAtAction(nameof(GetProductById), new { id = creado.Id }, creado);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductForUpdateDto dto)
        {
            ProductForReadDto? existente = _service.GetProductById(id);

            if (existente == null)
                return NotFound();

            _service.UpdateProduct(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            ProductForReadDto? existente = _service.GetProductById(id);

            if (existente == null)
                return NotFound();

            _service.DeleteProduct(id);
            return NoContent();
        }

        [HttpGet("search")]
        public IActionResult SearchProductsByName([FromQuery] string name)
        {
            List<ProductForReadDto> resultado = _service.SearchProductsByName(name);
            return Ok(resultado);
        }

        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            ProductStatsDto resultado = _service.GetStats();
            return Ok(resultado);
        }
    }
}