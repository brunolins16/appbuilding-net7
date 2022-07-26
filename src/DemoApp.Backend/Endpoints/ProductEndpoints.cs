using Microsoft.EntityFrameworkCore;
using DemoApp.Model;

namespace DemoApp.Backend.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/product", async (OrderContext db) =>
        {
            return await db.Products.ToListAsync();
        })
        .WithTags(nameof(Product))
        .WithName("GetAllProducts")
        .Produces<List<Product>>(StatusCodes.Status200OK);

        routes.MapGet("/product/{id}", async (int Id, OrderContext db) =>
        {
            return await db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == Id)
                is Product model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithTags(nameof(Product))
        .WithName("GetProductById")
        .Produces<Product>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/product/{id}", async (int Id, Product product, OrderContext db) =>
        {
            var foundModel = await db.Products.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            //update model properties here

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithTags(nameof(Product))
        .WithName("UpdateProduct")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/product/", async (Product product, OrderContext db) =>
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return Results.Created($"/Products/{product.Id}", product);
        })
        .WithTags(nameof(Product))
        .WithName("CreateProduct")
        .Produces<Product>(StatusCodes.Status201Created);

        routes.MapDelete("/product/{id}", async (int Id, OrderContext db) =>
        {
            if (await db.Products.FindAsync(Id) is Product product)
            {
                db.Products.Remove(product);
                await db.SaveChangesAsync();
                return Results.Ok(product);
            }

            return Results.NotFound();
        })
        .WithTags(nameof(Product))
        .WithName("DeleteProduct")
        .Produces<Product>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
