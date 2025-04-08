using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LAP.Main.Contexts.Cart;

public class CartContext : ModelBaseContext<Lap.Model.Models.Cart.Cart>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<CartContext> _logger;

    public CartContext(DbContextOptions<ModelBaseContext<Lap.Model.Models.Cart.Cart>> options,
        IServiceProvider serviceProvider, ILogger<CartContext> logger) : base(options, logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Lap.Model.Models.Cart.Cart>().HasOne(c => c.Order).WithMany().HasForeignKey(c => c.OrderId);
        modelBuilder.Entity<Lap.Model.Models.Cart.Cart>().HasMany(c => c.Products).WithMany();
    }
    
    public class CartContextDesignTimeFactory : IDesignTimeDbContextFactory<CartContext>
    {
        public CartContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.Cart.Cart>>();

            builder.UseMySql(args[0], ServerVersion.AutoDetect(args[0]));
            return new CartContext(builder.Options, null!, null!);
        }
    }
}