using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LAP.Main.Contexts.Customer;

public class CustomerContext : ModelBaseContext<Lap.Model.Models.Customer.Customer>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<CustomerContext> _logger;

    public CustomerContext(DbContextOptions<ModelBaseContext<Lap.Model.Models.Customer.Customer>> options,
        IServiceProvider serviceProvider, ILogger<CustomerContext> logger) : base(options, logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Lap.Model.Models.Customer.Customer>().HasOne(c => c.Contact).WithOne()
            .HasForeignKey<Lap.Model.Models.Customer.Customer>(c => c.ContactId);

        modelBuilder.Entity<Lap.Model.Models.Customer.Customer>().HasMany(c => c.Orders).WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId);
    }

    public class CustomerContextDesignTimeFactory : IDesignTimeDbContextFactory<CustomerContext>
    {
        public CustomerContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.Customer.Customer>>();

            builder.UseMySql(args[0], ServerVersion.AutoDetect(args[0]));
            return new CustomerContext(builder.Options, null!, null!);
        }
    }
}