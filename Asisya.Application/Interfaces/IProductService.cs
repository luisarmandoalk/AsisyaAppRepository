using Asisya.Application.DTOs;
using Asisya.ProductsApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asisya.Application.Interfaces
{
    public interface IProductService
    {
        Task GenerateMassiveProducts(int amount);

        Task<List<ProductDto>> GetAll(ProductFilter filter);

        Task<ProductDto> GetById(Guid id);

        Task<ProductDto> Create(CreateProductDto dto);

        Task Update(Guid id, CreateProductDto dto);

        Task Delete(Guid id);
    }
}
