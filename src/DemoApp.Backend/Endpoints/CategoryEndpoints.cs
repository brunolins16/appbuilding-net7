using Microsoft.EntityFrameworkCore;
using DemoApp.Model;
using System.Linq;

namespace DemoApp.Backend.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("category", async (OrderContext db) =>
        {
            return await db.Categories.ToListAsync();
        })
        .WithTags(nameof(Category))
        .WithName("GetAllCategorys")
        .Produces<List<Category>>(StatusCodes.Status200OK);

        routes.MapGet("category/{id}", async (int Id, OrderContext db) =>
        {
            return await db.Categories.FindAsync(Id)
                is Category model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithTags(nameof(Category))
        .WithName("GetCategoryById")
        .Produces<Category>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        CleanUpMetadata(routes.MapPut("category/{id}", async (int Id, Category category, OrderContext db) =>
        {
            var foundModel = await db.Categories.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            //update model properties here
            foundModel.Name = category.Name;

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithTags(nameof(Category))
        .WithName("UpdateCategory")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent));

        CleanUpMetadata(routes.MapPost("category/", async (Category category, OrderContext db) =>
        {
            db.Categories.Add(category);
            await db.SaveChangesAsync();
            return Results.Created($"/Categorys/{category.Id}", category);
        })
        .WithTags(nameof(Category))
        .WithName("CreateCategory")
        .Produces<Category>(StatusCodes.Status201Created));

        CleanUpMetadata(routes.MapDelete("category/{id}", async (int Id, OrderContext db) =>
        {
            if (await db.Categories.FindAsync(Id) is Category category)
            {
                db.Categories.Remove(category);
                await db.SaveChangesAsync();
                return Results.Ok(category);
            }

            return Results.NotFound();
        })
        .WithTags(nameof(Category))
        .WithName("DeleteCategory")
        .Produces<Category>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound));
    }

    private static void CleanUpMetadata(RouteHandlerBuilder endpoint)
    {
        endpoint
            .Add(builder => {
                var metadata = builder.Metadata.OfType<HttpMethodMetadata>().First();
                builder.Metadata.Remove(metadata);
            });

    }
}
