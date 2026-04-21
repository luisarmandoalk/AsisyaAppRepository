using Asisya.Application.DTOs;
using Asisya.Application.Interfaces;
using Asisya.Domain.Entities;
using Asisya.Domain.Interfaces;
using Asisya.ProductsApi.Filters;
using AutoMapper;

namespace Asisya.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task GenerateMassiveProducts(int amount)
        {
            var batchSize = 5000;

            for (int i = 0; i < amount; i += batchSize)
            {
                var list = new List<Product>();

                for (int j = 0; j < batchSize && (i + j) < amount; j++)
                {
                    list.Add(new Product
                    {
                        ProductName = $"Product {i + j}",
                        UnitPrice = Random.Shared.Next(10, 1000),

                        CategoryId = Guid.NewGuid(), // temporal
                        SupplierId = Guid.NewGuid()  // obligatorio
                    });
                }

                await _repo.BulkInsert(list);
            }
        }

        public async Task<List<ProductDto>> GetAll(ProductFilter filter)
        {
            var data = await _repo.GetAll(filter.Page, filter.PageSize, filter.Search);

            if (!string.IsNullOrEmpty(filter.Category))
                data = data.Where(x => x.Category.CategoryName == filter.Category).ToList();           

            return _mapper.Map<List<ProductDto>>(data);
        }

        public async Task<ProductDto> GetById(Guid id)
        {
            var product = await _repo.GetById(id);

            if (product == null)
                throw new Exception("Producto no encontrado");

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> Create(CreateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);

            var created = await _repo.Create(entity);

            return _mapper.Map<ProductDto>(created);
        }

        public async Task Update(Guid id, CreateProductDto dto)
        {
            var product = await _repo.GetById(id);

            if (product == null)
                throw new Exception("Producto no encontrado");

            product.ProductName = dto.ProductName;
            product.UnitPrice = dto.UnitPrice;
            product.CategoryId = dto.CategoryId; // Guid

            await _repo.Update(product);
        }

        public async Task Delete(Guid id)
        {
            var product = await _repo.GetById(id);

            if (product == null)
                throw new Exception("Producto no encontrado");

            await _repo.Delete(id);
        }

     
    }
}