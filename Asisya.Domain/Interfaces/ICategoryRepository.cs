using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asisya.Domain.Entities;

namespace Asisya.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();

        Task<Category> GetById(int id);

        Task<Category> Create(Category category);

        Task Update(Category category);

        Task Delete(Category category);
    }
}
