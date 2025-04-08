using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LAP.Main.Contexts.Order;

public class OrderContext : ModelBaseContext<Lap.Model.Models.Order.Order>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OrderContext> _logger;

    public OrderContext(DbContextOptions<ModelBaseContext<Lap.Model.Models.Order.Order>> options,
        IServiceProvider serviceProvider, ILogger<OrderContext> logger) : base(options, logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Lap.Model.Models.Order.Order>().HasOne(o => o.Customer).WithMany(c => c.Orders).HasForeignKey(o => o.CustomerId);
    }
    
    public class OrderContextDesignTimeFactory : IDesignTimeDbContextFactory<OrderContext>
    {
        public OrderContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.Order.Order>>();

            builder.UseMySql(args[0], ServerVersion.AutoDetect(args[0]));
            return new OrderContext(builder.Options, null!, null!);
        }
    }

}