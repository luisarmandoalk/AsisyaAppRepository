using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

public class ProductApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetProductEndpointOK()
    {
        var response = await _client.GetAsync("/api/product");

        var body = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode, $"STATUS: {response.StatusCode} | BODY: {body}");
    }
}