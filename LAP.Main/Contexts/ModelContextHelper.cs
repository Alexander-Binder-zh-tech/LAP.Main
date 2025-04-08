using Lap.Components.Handler;
using LAP.Main.Contexts.Address;
using LAP.Main.Contexts.Cart;
using LAP.Main.Contexts.Contact;
using LAP.Main.Contexts.Customer;
using LAP.Main.Contexts.Order;
using LAP.Main.Contexts.Product;
using Microsoft.EntityFrameworkCore;

namespace LAP.Main.Contexts;

public static class ModelContextHelper
{
    public static void ConfigureModelContexts(WebApplicationBuilder webApplicationBuilder, string? connectionString)
    {
        webApplicationBuilder.Services.AddHttpContextAccessor();

        //AddressContext
        webApplicationBuilder.Services.AddScoped(sp =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.Address.Address>>();
            optionsBuilder.UseMySql(connectionString!, ServerVersion.AutoDetect(connectionString));

            return new AddressContext(optionsBuilder.Options, sp,sp.GetService<ILogger<AddressContext>>()!);
        });
        //CartContext
        webApplicationBuilder.Services.AddScoped(sp =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.Cart.Cart>>();
            optionsBuilder.UseMySql(connectionString!, ServerVersion.AutoDetect(connectionString));

            return new CartContext(optionsBuilder.Options, sp,sp.GetService<ILogger<CartContext>>()!);
        });
        //ContactContext
        webApplicationBuilder.Services.AddScoped(sp =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.Contact.Contact>>();
            optionsBuilder.UseMySql(connectionString!, ServerVersion.AutoDetect(connectionString));

            return new ContactContext(optionsBuilder.Options, sp,sp.GetService<ILogger<ContactContext>>()!);
        });
        //CustomerContext
        webApplicationBuilder.Services.AddScoped(sp =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.Customer.Customer>>();
            optionsBuilder.UseMySql(connectionString!, ServerVersion.AutoDetect(connectionString));

            return new CustomerContext(optionsBuilder.Options, sp,sp.GetService<ILogger<CustomerContext>>()!);
        });
        //OrderContext
        webApplicationBuilder.Services.AddScoped(sp =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.Order.Order>>();
            optionsBuilder.UseMySql(connectionString!, ServerVersion.AutoDetect(connectionString));

            return new OrderContext(optionsBuilder.Options, sp,sp.GetService<ILogger<OrderContext>>()!);
        });
        //ProductContext
        webApplicationBuilder.Services.AddScoped(sp =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<ModelBaseContext<Lap.Model.Models.Product.Product>>();
            optionsBuilder.UseMySql(connectionString!, ServerVersion.AutoDetect(connectionString));

            return new ProductContext(optionsBuilder.Options, sp,sp.GetService<ILogger<ProductContext>>()!);
        });
        
        //Register remaining handlers
        webApplicationBuilder.Services.AddSingleton<ModelChecker>();

        webApplicationBuilder.Services.AddScoped<ILocalStorageHandler, LocalStorageHandler>();
        webApplicationBuilder.Services.AddScoped<LocalUserStorage>();
    }

    public static void MigrateModelDb(WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();

        //Migrates models with mapping and all dependent tables!
        var cartContext = scope.ServiceProvider.GetService<CartContext>()!;
        cartContext.Database.Migrate();

    }
}