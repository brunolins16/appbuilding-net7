using DemoApp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using DemoApp.Backend.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDb") ?? throw new InvalidOperationException("Connection string 'OrderDb' not found."), b => b.MigrationsAssembly("DemoApp.Backend"));
    options.UseModel(DemoApp.Model.CompiledModels.OrderContextModel.Instance);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var apis = app.MapGroup("/api");
apis.MapProductEndpoints();
apis.MapCategoryEndpoints();

app.MapCustomerEndpoints();

app.Run();