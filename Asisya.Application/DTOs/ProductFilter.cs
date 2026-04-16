namespace Asisya.ProductsApi.Filters
{
    public class ProductFilter
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 50;

        public string Search { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }
    }
}
