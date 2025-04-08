using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LAP.Main.Contexts.Contact;

public class ContactContext : ModelBaseContext<Lap.Model.Models.Contact.Contact>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ContactContext> _logger;

    public ContactContext(DbContextOptions<ModelBaseContext<Lap.Model.Models.Contact.Contact>> options,
        IServiceProvider serviceProvider, ILogger<ContactContext> logger) : base(options, logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Lap.Model.Models.Contact.Contact>().HasOne(c => c.Address).WithOne().HasForeignKey<Lap.Model.Models.Contact.Contact>(c => c.AddressId);
        modelBuilder.Entity<Lap.Model.Models.Contact.Contact>().HasOne(c => c.DeliveryAddress).WithOne().HasForeignKey<Lap.Model.Models.Contact.Contact>(c => c.DeliveryAddressId);
    }
    
    public class ContactContextDesignTimeFactory : IDesignTimeDbContextFactory<ContactContext>
    {
        public ContactContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.Contact.Contact>>();

            builder.UseMySql(args[0], ServerVersion.AutoDetect(args[0]));
            return new ContactContext(builder.Options, null!, null!);
        }
    }
}