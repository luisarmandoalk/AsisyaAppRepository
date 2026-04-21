using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asisya.Application.BackgroundJobs;
using Asisya.Application.Interfaces;
using Asisya.Application.Interfaces.Asisya.Application.Interfaces;

namespace Asisya.Application.Services
{
    public class ProductJobService : IProductJobService
    {
        public void Enqueue(Guid categoryId, Guid supplierId)
        {
            ProductJobQueue.Enqueue(categoryId, supplierId);
        }
    }
}
