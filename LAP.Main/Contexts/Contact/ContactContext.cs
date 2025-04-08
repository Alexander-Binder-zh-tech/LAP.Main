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