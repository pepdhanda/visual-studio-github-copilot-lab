using DataEntities;
using Microsoft.EntityFrameworkCore;
using Products.Data;

namespace Products.Endpoints;

/// <summary>
/// Provides extension methods for mapping product-related API endpoints to an endpoint route builder in a minimal API
/// application.
/// </summary>
/// <remarks>This static class contains methods for registering RESTful endpoints for product operations, such as
/// retrieving, creating, updating, and deleting products. The endpoints are typically used in ASP.NET Core minimal API
/// applications to expose product data over HTTP. Endpoints are grouped under the "/api/Product" route
/// prefix.</remarks>
public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Product");

        group.MapGet("/", async (ProductDataContext db) =>
        {
            return await db.Product.ToListAsync();
        })
        .WithName("GetAllProducts")
        .Produces<List<Product>>(StatusCodes.Status200OK);

        // Additional endpoints (e.g., Get by ID, Create, Update, Delete) can be added here
        group.MapGet("/{productID}", async (int productID, ProductDataContext db) =>
        {
            return await db.Product.FindAsync(productID)
                is Product model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
            .WithName("GetProductById")
            .Produces<Product>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", async (Product product, ProductDataContext db) =>
        {
            var errors = new Dictionary<string, string[]>();

            if (string.IsNullOrWhiteSpace(product.Name))
            {
                errors["Name"] = new[] { "Name is required." };
            }

            if (product.Price <= 0)
            {
                errors["Price"] = new[] { "Price must be greater than zero." };
            }

            if (errors.Count > 0)
            {
                return Results.ValidationProblem(errors);
            }
            db.Product.Add(product);
            await db.SaveChangesAsync();

            return Results.CreatedAtRoute("GetProductById", new { productID = product.Id }, product);
        })
        .WithName("CreateProduct")
        .Produces<Product>(StatusCodes.Status201Created);

        group.MapPut("/{id}", async (int id, Product input, ProductDataContext db) =>
        {
            if (input is null)
            {
                return Results.BadRequest("Product payload is required.");
            }

            var errors = new Dictionary<string, string[]>();

            if (string.IsNullOrWhiteSpace(input.Name))
            {
                errors["Name"] = new[] { "Name is required." };
            }

            if (input.Price < 0)
            {
                errors["Price"] = new[] { "Price must be greater than or equal to 0." };
            }

            if (errors.Count > 0)
            {
                return Results.ValidationProblem(errors);
            }
                      
            var product = await db.Product.FindAsync(id);
            if (product is null)
                return Results.NotFound();

            // update fields
            product.Name = input.Name;
            product.Description = input.Description;
            product.Price = input.Price;
            product.ImageUrl = input.ImageUrl;

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateProduct")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("/{id}", async (int id, ProductDataContext db) =>
        {
            var product = await db.Product.FindAsync(id);
            if (product is null)
                return Results.NotFound();

            db.Product.Remove(product);
            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("DeleteProduct")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
