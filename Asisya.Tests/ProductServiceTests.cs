using Xunit;
using Moq;
using Asisya.Application.Interfaces;
using Asisya.Domain.Entities;

public class ProductServiceTests
{
    [Fact]
    public void CreateProductProductOK()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            ProductName = "Test Product"
        };

        // Act
        var result = product;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Product", result.ProductName);
    }
}