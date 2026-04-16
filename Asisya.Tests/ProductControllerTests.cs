using Xunit;
using Moq;
using Asisya.Application.Interfaces;
using Asisya.Domain.Entities;
using Asisya.Application.DTOs;
using Asisya.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Asisya.Api.Controllers;

public class ProductControllerTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateProductdOK()
    {
        // Arrange
        var context = GetDbContext();
        var controller = new ProductController(context);

        var dto = new CreateProductDto
        {
            ProductName = "Laptop",
            UnitPrice = 1000,
            CategoryId = Guid.NewGuid(),
            SupplierId = Guid.NewGuid()
        };

        // Act
        var result = await controller.Create(dto);

        // Assert
        Assert.Equal(1, context.Products.Count());
    }
}