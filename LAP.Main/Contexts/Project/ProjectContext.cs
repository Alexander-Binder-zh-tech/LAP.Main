using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LAP.Main.Contexts.Project
{
    public class ProjectContext : ModelBaseContext<Lap.Model.Models.Project.Project>
    {
        public ProjectContext(DbContextOptions<ModelBaseContext<Lap.Model.Models.Project.Project>> options,
            ILogger<ProjectContext> logger, IServiceProvider serviceProvider) : base(options, logger)
        {
        }

        public async Task<bool> IsProjectNumberFree(int projectNumber, int? id)
        {
            var exists = await Entities.Where(p => p.Deleted == false  && p.Id != id).Select(p => p.ProjectNumber)
                .AnyAsync(number => number == projectNumber);
            
            return exists;
        }

        public class ProjectContextDesignTimeFactory : IDesignTimeDbContextFactory<ProjectContext>
        {
            public ProjectContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.Project.Project>>();

                builder.UseMySql(args[0], ServerVersion.AutoDetect(args[0]));
                return new ProjectContext(builder.Options, null!, null!);
            }
        }
    }
}