using Asisya.Application.BackgroundJobs;
using Asisya.Domain.Entities;
using Asisya.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asisya.Infrastructure.BackgroundServices
{
    public class ProductGeneratorWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ProductGeneratorWorker(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var job = ProductJobQueue.Dequeue();

                if (job != null)
                {
                    using var scope = _scopeFactory.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    await GenerateProducts(db, job.Value.categoryId, job.Value.supplierId);
                }

                await Task.Delay(500);
            }
        }

        private async Task GenerateProducts(AppDbContext db, Guid categoryId, Guid supplierId)
        {
            const int total = 5;
            const int batchSize = 5000;

            for (int i = 0; i < total; i += batchSize)
            {
                var batch = new List<Product>();

                for (int j = 0; j < batchSize; j++)
                {
                    batch.Add(new Product
                    {
                        Id = Guid.NewGuid(),
                        ProductName = $"Product {i + j}",
                        UnitPrice = (i + j) % 1000,
                        CategoryId = categoryId,
                        SupplierId = supplierId
                    });
                }

                await db.Products.AddRangeAsync(batch);
                await db.SaveChangesAsync();
            }
        }
    }
}
