using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LAP.Main.Contexts.Address;

public class AddressContext : ModelBaseContext<Lap.Model.Models.Address.Address>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AddressContext> _logger;
    
    public AddressContext(DbContextOptions<ModelBaseContext<Lap.Model.Models.Address.Address>> options,
        IServiceProvider serviceProvider, ILogger<AddressContext> logger) : base(options, logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    public class AddressContextDesignTimeFactory : IDesignTimeDbContextFactory<AddressContext>
    {
        public AddressContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.Address.Address>>();

            builder.UseMySql(args[0], ServerVersion.AutoDetect(args[0]));
            return new AddressContext(builder.Options, null!, null!);
        }
    }
}