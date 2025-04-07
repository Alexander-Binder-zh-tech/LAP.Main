using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LAP.Main.Contexts.WorkTime
{
    public class WorkTimeContext : ModelBaseContext<Lap.Model.Models.WorkTime.WorkTime>
    {
        public WorkTimeContext(DbContextOptions<ModelBaseContext<Lap.Model.Models.WorkTime.WorkTime>> options,
            ILogger<WorkTimeContext> logger, IServiceProvider serviceProvider) : base(options, logger)
        {
        }
        
        public async Task<Lap.Model.Models.WorkTime.WorkTime?> GetActiveConfig()
        {
            var entity = await Entities.FirstOrDefaultAsync(wt => wt.Deleted == false);
            return entity;
        }

        public class WorkTimeContextDesignTimeFactory : IDesignTimeDbContextFactory<WorkTimeContext>
        {
            public WorkTimeContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.WorkTime.WorkTime>>();

                builder.UseMySql(args[0], ServerVersion.AutoDetect(args[0]));
                return new WorkTimeContext(builder.Options, null!, null!);
            }
        }
    }
}