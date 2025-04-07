using Lap.Components.Handler;
using LAP.Main.Contexts.Customer;
using LAP.Main.Contexts.Holiday;
using LAP.Main.Contexts.HourEntry;
using LAP.Main.Contexts.Project;
using LAP.Main.Contexts.Tag;
using LAP.Main.Contexts.WorkTime;
using Microsoft.EntityFrameworkCore;

namespace LAP.Main.Contexts;

public static class ModelContextHelper
{
    public static void ConfigureModelContexts(WebApplicationBuilder webApplicationBuilder, string? connectionString)
    {
        webApplicationBuilder.Services.AddHttpContextAccessor();

        //HourEntryContext
        webApplicationBuilder.Services.AddScoped(sp =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.HourEntry.HourEntry>>();
            optionsBuilder.UseMySql(connectionString!, ServerVersion.AutoDetect(connectionString));

            return new HourEntryContext(optionsBuilder.Options, sp.GetService<ILogger<HourEntryContext>>()!, sp);
        });
        //WorkTimeContext
        webApplicationBuilder.Services.AddScoped(sp =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.WorkTime.WorkTime>>();
            optionsBuilder.UseMySql(connectionString!, ServerVersion.AutoDetect(connectionString));

            return new WorkTimeContext(optionsBuilder.Options, sp.GetService<ILogger<WorkTimeContext>>()!, sp);
        });
        //HolidayContext
        webApplicationBuilder.Services.AddScoped(sp =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.Holiday.Holiday>>();
            optionsBuilder.UseMySql(connectionString!, ServerVersion.AutoDetect(connectionString));

            return new HolidayContext(optionsBuilder.Options, sp.GetService<ILogger<HolidayContext>>()!, sp);
        });
        //CustomerContext
        webApplicationBuilder.Services.AddScoped(sp =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.Customer.Customer>>();
            optionsBuilder.UseMySql(connectionString!, ServerVersion.AutoDetect(connectionString));

            return new CustomerContext(optionsBuilder.Options, sp.GetService<ILogger<CustomerContext>>()!, sp);
        });
        //ProjectContext
        webApplicationBuilder.Services.AddScoped(sp =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.Project.Project>>();
            optionsBuilder.UseMySql(connectionString!, ServerVersion.AutoDetect(connectionString));

            return new ProjectContext(optionsBuilder.Options, sp.GetService<ILogger<ProjectContext>>()!, sp);
        });
        //TagContext
        webApplicationBuilder.Services.AddScoped(sp =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.Tag.Tag>>();
            optionsBuilder.UseMySql(connectionString!, ServerVersion.AutoDetect(connectionString));

            return new TagContext(optionsBuilder.Options, sp.GetService<ILogger<TagContext>>()!, sp);
        });
        
        //Register remaining handlers
        webApplicationBuilder.Services.AddSingleton<ModelChecker>();

        webApplicationBuilder.Services.AddScoped<ILocalStorageHandler, LocalStorageHandler>();
        webApplicationBuilder.Services.AddScoped<LocalUserStorage>();
    }

    public static void MigrateModelDb(WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();

        //Migrates HourEntry and all dependent tables!
        var hourEntryContext = scope.ServiceProvider.GetService<HourEntryContext>()!;
        hourEntryContext.Database.Migrate();

        var workTimeContext = scope.ServiceProvider.GetService<WorkTimeContext>()!;
        workTimeContext.Database.Migrate();

        var holidayContext = scope.ServiceProvider.GetService<HolidayContext>()!;
        holidayContext.Database.Migrate();

        var tagContext = scope.ServiceProvider.GetService<TagContext>()!;
        tagContext.Database.Migrate();
    }
}