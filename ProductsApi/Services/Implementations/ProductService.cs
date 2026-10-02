using ProductsApi.Entities;
using ProductsApi.Models.DTOs.Requests;
using ProductsApi.Models.DTOs.Responses;
using ProductsApi.Repositories.Interfaces;
using ProductsApi.Services.Interfaces;

namespace ProductsApi.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public List<ProductForReadDto> GetAllProducts()
        {
            List<Product> productos = _repository.GetAllProducts();
            List<ProductForReadDto> resultado = new List<ProductForReadDto>();

            foreach (Product p in productos)
            {
                resultado.Add(new ProductForReadDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                });
            }

            return resultado;
        }

        public ProductForReadDto? GetProductById(int id)
        {
            Product? producto = _repository.GetProductById(id);

            if (producto == null)
                return null;

            return new ProductForReadDto
            {
                Id = producto.Id,
                Name = producto.Name,
                Price = producto.Price
            };
        }

        public ProductForReadDto CreateProduct(ProductForCreateDto dto)
        {
            Product nuevo = new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };

            _repository.AddProduct(nuevo);

            return new ProductForReadDto
            {
                Id = nuevo.Id,
                Name = nuevo.Name,
                Price = nuevo.Price
            };
        }

        public void UpdateProduct(int id, ProductForUpdateDto dto)
        {
            Product? existente = _repository.GetProductById(id);

            if (existente != null)
            {
                existente.Name = dto.Name;
                existente.Price = dto.Price;
                _repository.UpdateProduct(existente);
            }
        }

        public void DeleteProduct(int id)
        {
            Product? existente = _repository.GetProductById(id);

            if (existente != null)
            {
                _repository.DeleteProduct(existente);
            }
        }

        public List<ProductForReadDto> SearchProductsByName(string name)
        {
            List<Product> productos = _repository.SearchProductsByName(name);
            List<ProductForReadDto> resultado = new List<ProductForReadDto>();

            foreach (Product p in productos)
            {
                resultado.Add(new ProductForReadDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                });
            }

            return resultado;
        }

        public ProductStatsDto GetStats()
        {
            List<Product> productos = _repository.GetAllProducts();

            if (productos.Count == 0)
            {
                return new ProductStatsDto
                {
                    Total = 0,
                    AveragePrice = 0,
                    MostExpensiveName = "N/A"
                };
            }

            Product masCaro = productos.OrderByDescending(p => p.Price).First();

            return new ProductStatsDto
            {
                Total = productos.Count,
                AveragePrice = productos.Average(p => p.Price),
                MostExpensiveName = masCaro.Name
            };
        }

        public bool ProductNameExists(string name)
        {
            List<Product> productos = _repository.GetAllProducts();
            return productos.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}