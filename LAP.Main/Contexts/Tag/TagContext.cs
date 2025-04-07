using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LAP.Main.Contexts.Tag
{
    public class TagContext : ModelBaseContext<Lap.Model.Models.Tag.Tag>
    {
        public TagContext(DbContextOptions<ModelBaseContext<Lap.Model.Models.Tag.Tag>> options,
            ILogger<TagContext> logger, IServiceProvider serviceProvider) : base(options, logger)
        {
        }

        public async Task<bool> IsTagNameFree(string tagName, int? id)
        {
            var exists = await Entities.Where(t => t.Deleted == false && t.Id != id).Select(t => t.Name)
                .AnyAsync(name => name == tagName);

            return exists;
        }

        public class TagContextDesignTimeFactory : IDesignTimeDbContextFactory<TagContext>
        {
            public TagContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.Tag.Tag>>();

                builder.UseMySql(args[0], ServerVersion.AutoDetect(args[0]));
                return new TagContext(builder.Options, null!, null!);
            }
        }
    }
}