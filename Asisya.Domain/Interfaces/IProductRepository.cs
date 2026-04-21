using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Asisya.Domain.Entities;

namespace Asisya.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task BulkInsert(List<Product> products);

        Task<List<Product>> GetAll(int page, int pageSize, string search);

        Task<Product> GetById(Guid id);

        Task<Product> Create(Product product);

        Task Update(Product product);

        Task Delete(Guid id);
    }
}