namespace DemoApp.Model;

using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

public class OrderContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<PurchaseOrder> Orders => Set<PurchaseOrder>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    public string DbPath { get; }

    public OrderContext() { }

    public OrderContext(DbContextOptions<OrderContext> options)
        : base(options)
    {
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PurchaseOrderLine>()
            .ToTable("OrderLines")
            .HasKey(x => new { x.OrderId, x.LineNumber });

        modelBuilder.Entity<PurchaseOrder>()
            .ToTable("Orders")
            .HasKey(x => x.OrderId);

        modelBuilder.Entity<Person>()
            .ToTable("People")
            .HasDiscriminator<char>("personType")
            .HasValue<Person>('N')
            .HasValue<Employee>('E')
            .HasValue<Customer>('C');

        base.OnModelCreating(modelBuilder);
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("test");
}
