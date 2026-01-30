using DataEntities;
using System.Text.Json;

namespace Store.Services;

public class ProductService
{
    HttpClient httpClient;
    public ProductService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<Product?> GetProductById(int id)
    {
        var response = await httpClient.GetAsync($"/api/Product/{id}");
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to retrieve product with id {id}. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
        }

        return await response.Content.ReadFromJsonAsync(ProductSerializerContext.Default.Product);
    }

    public async Task<Product?> CreateProduct(Product product)
    {
        var response = await httpClient.PostAsJsonAsync("/api/Product", product, ProductSerializerContext.Default.Product);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to create product. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
        }

        return await response.Content.ReadFromJsonAsync(ProductSerializerContext.Default.Product);
    }

    public async Task<bool> UpdateProduct(int id, Product product)
    {
        var response = await httpClient.PutAsJsonAsync($"/api/Product/{id}", product, ProductSerializerContext.Default.Product);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteProduct(int id)
    {
        var response = await httpClient.DeleteAsync($"/api/Product/{id}");
        return response.IsSuccessStatusCode;
    }
    public async Task<List<Product>> GetProducts()
    {
        List<Product>? products = null;
        var response = await httpClient.GetAsync("/api/Product");
        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            products = await response.Content.ReadFromJsonAsync(ProductSerializerContext.Default.ListProduct);
        }

        return products ?? new List<Product>();
    }
    
}