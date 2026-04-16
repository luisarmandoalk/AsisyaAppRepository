using Asisya.Domain.Entities;
using Asisya.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Xunit;

public class ProductGenerateTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public ProductGenerateTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;

        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer("Server=TOSH-Z3T001\\SQLEXPRESS01;Database=AsisyaTestDb;Trusted_Connection=True;TrustServerCertificate=True;");
                });
            });
        }).CreateClient();
    }

    [Fact]
    public async Task GenerateReturnOK()
    {
        var categoryId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var supplierId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        var url = $"/api/product/generate?categoryId={categoryId}&supplierId={supplierId}";

        var response = await _client.PostAsync(url, null);

        var body = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode, body);
    }

    
}