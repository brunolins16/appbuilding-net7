using Microsoft.EntityFrameworkCore;
using DemoApp.Model;
namespace DemoApp.Backend.Endpoints;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/customer", async (OrderContext db) =>
        {
            return await db.Customers.ToListAsync();
        })
        .WithTags(nameof(Customer))
        .WithName("GetAllCustomers")
        .Produces<List<Customer>>(StatusCodes.Status200OK);

        routes.MapGet("/customer/{id}", async (Guid Id, OrderContext db) =>
        {
            return await db.Customers.FindAsync(Id)
                is Customer model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithTags(nameof(Customer))
        .WithName("GetCustomerById")
        .Produces<Customer>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/customer/{id}", async (Guid Id, Customer customer, OrderContext db) =>
        {
            var foundModel = await db.Customers.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            //update model properties here

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithTags(nameof(Customer))
        .WithName("UpdateCustomer")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/customer/", async (Customer customer, OrderContext db) =>
        {
            db.Customers.Add(customer);
            await db.SaveChangesAsync();
            return Results.Created($"/Customers/{customer.Id}", customer);
        })
        .WithTags(nameof(Customer))
        .WithName("CreateCustomer")
        .Produces<Customer>(StatusCodes.Status201Created);

        routes.MapDelete("/customer/{id}", async (Guid Id, OrderContext db) =>
        {
            if (await db.Customers.FindAsync(Id) is Customer customer)
            {
                db.Customers.Remove(customer);
                await db.SaveChangesAsync();
                return Results.Ok(customer);
            }

            return Results.NotFound();
        })
        .WithTags(nameof(Customer))
        .WithName("DeleteCustomer")
        .Produces<Customer>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
