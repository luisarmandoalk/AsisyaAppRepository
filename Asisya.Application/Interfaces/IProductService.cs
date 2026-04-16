using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asisya.Application.DTOs;

namespace Asisya.Application.Interfaces
{
    public interface IProductService
    {
        Task GenerateMassiveProducts(int amount);

        Task<List<ProductDto>> GetAll(int page, int pageSize, string search);

        Task<ProductDto> GetById(int id);

        Task<ProductDto> Create(CreateProductDto dto);

        Task Update(int id, CreateProductDto dto);

        Task Delete(int id);
    }
}
