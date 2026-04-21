using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asisya.Application.Interfaces
{
    namespace Asisya.Application.Interfaces
    {
        public interface IProductJobService
        {
            void Enqueue(Guid categoryId, Guid supplierId);
        }
    }
}
