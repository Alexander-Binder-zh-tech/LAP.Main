using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LAP.Main.Contexts.Product;

public class ProductContext : ModelBaseContext<Lap.Model.Models.Product.Product>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ProductContext> _logger;

    public ProductContext(DbContextOptions<ModelBaseContext<Lap.Model.Models.Product.Product>> options,
        IServiceProvider serviceProvider, ILogger<ProductContext> logger) : base(options, logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    public class ContactContextDesignTimeFactory : IDesignTimeDbContextFactory<ProductContext>
    {
        public ProductContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.Product.Product>>();

            builder.UseMySql(args[0], ServerVersion.AutoDetect(args[0]));
            return new ProductContext(builder.Options, null!, null!);
        }
    }

}