using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LAP.Main.Contexts.Customer
{
    public class CustomerContext : ModelBaseContext<Lap.Model.Models.Customer.Customer>
    {
        public CustomerContext(DbContextOptions<ModelBaseContext<Lap.Model.Models.Customer.Customer>> options,
            ILogger<CustomerContext> logger, IServiceProvider serviceProvider) : base(options, logger)
        {
        }

        public async Task<bool> IsCustomerNumberFree(int customerNumber, int? id)
        {
            var exists = await Entities.Where(c => c.Deleted == false && c.Id != id).Select(c => c.CustomerNumber)
                .AnyAsync(number => number == customerNumber);
            
            return exists;
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
}