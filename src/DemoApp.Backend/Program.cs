using DemoApp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using DemoApp.Backend.Endpoints;

const string CorsPolicyName = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsPolicyName,
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:7280").AllowAnyHeader().AllowAnyMethod();
                      });
});

builder.Services.AddDbContext<OrderContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDb") ?? throw new InvalidOperationException("Connection string 'OrderDb' not found."), b => b.MigrationsAssembly("DemoApp.Backend"));
    options.UseModel(DemoApp.Model.CompiledModels.OrderContextModel.Instance);
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


var apis = app.MapGroup("/api").RequireCors(CorsPolicyName);
apis.MapProductEndpoints();
apis.MapCategoryEndpoints();
apis.MapCustomerEndpoints();


app.Run();