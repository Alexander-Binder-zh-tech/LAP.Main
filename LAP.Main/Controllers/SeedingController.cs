using LAP.Main.Contexts.Address;
using LAP.Main.Contexts.Contact;
using LAP.Main.Contexts.Customer;
using LAP.Main.Contexts.Product;
using Lap.Model;
using Lap.Model.Models.Address;
using Lap.Model.Models.Contact;
using Lap.Model.Models.Customer;
using Lap.Model.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LAP.Main.Controllers;

//TODO if theres is time add seeding

[Route("/api/master-data/seeding")]
[ApiController]
public class SeedingController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public SeedingController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [HttpPost("seed-products")]
    [SwaggerOperation("SeedProduct")]
    public async Task<IActionResult> SeedProduct()
    {
        var ok = await SeedProducts();
        if (!ok)
        {
            return BadRequest("Error - if you read this good job :3");
        }

        return Ok();
    }
    
    [HttpPost("seed-customers")]
    [SwaggerOperation("SeedCustomer")]
    public async Task<IActionResult> SeedCustomer()
    {
        var ok = await SeedCustomers();
        if (!ok)
        {
            return BadRequest("Error - if you read this good job :3");
        }

        return Ok();
    }
    
    [HttpPost("seed-orders")]
    [SwaggerOperation("SeedOrder")]
    public async Task<IActionResult> SeedOrder()
    {
        var ok = await SeedOrders();
        if (!ok)
        {
            return BadRequest("Error - if you read this good job :3");
        }

        return Ok();
    }
    
    #region SeedMethods

    private async Task<bool> SeedProducts()
    {
        var products = new List<Product>();
        for (var i = 1; i <= 20; i++)
        {
            var product = new Product
            {
                Name = $"Test product {i}",
                Price = Random.Shared.NextDouble() * 100,
                Description = $"Test description {i}"
            };
            
            products.Add(product);
        }

        using var scope = _serviceProvider.CreateScope();
        var productContext = scope.ServiceProvider.GetService<ProductContext>()!;
        foreach (var product in products)
        {
            var result = await productContext.Create(product);
            if (result.Error != MasterDataError.None)
            {
                return false;
            }
        }

        return true;
    }
    
    private async Task<bool> SeedCustomers()
    {
        var addresses = new List<Address>();
        for (var i = 1; i <= 5; i++)
        {
            var address = new Address
            {
                Street = $"test street {i}",
                City = $"test city {i}",
                State = $"test state {i}",
                Country = $"test country {i}",
                PostalCode = $"test postal code {i}"
            };
            
            addresses.Add(address);
        }

        var contacts = new List<Contact>();
        for (var i = 1; i <= 5; i++)
        {
            var contact = new Contact
            {
                TelephoneNumber = Random.Shared.Next(10000, 99999).ToString(),
                Email = $"test{i}@test.com",
                AddressId = i,
                DeliveryAddressId = i
            };
            
            contacts.Add(contact);
        }
        
        var customers = new List<Customer>();
        for (var i = 1; i <= 5; i++)
        {
            var product = new Customer
            {
                Name = $"Test customer {i}",
                ContactId = i
            };
            
            customers.Add(product);
        }

        using var scope = _serviceProvider.CreateScope();
        var addressContext = scope.ServiceProvider.GetService<AddressContext>()!;
        foreach (var address in addresses)
        {
            var result = await addressContext.Create(address);
            if (result.Error != MasterDataError.None)
            {
                return false;
            }
        }
        
        var contactContext = scope.ServiceProvider.GetService<ContactContext>()!;
        foreach (var contact in contacts)
        {
            var result = await contactContext.Create(contact);
            if (result.Error != MasterDataError.None)
            {
                return false;
            }
        }
        
        var customerContext = scope.ServiceProvider.GetService<CustomerContext>()!;
        foreach (var customer in customers)
        {
            var result = await customerContext.Create(customer);
            if (result.Error != MasterDataError.None)
            {
                return false;
            }
        }

        return true;
    }

    private async Task<bool> SeedOrders()
    {
        var products = new List<Product>();
        for (var i = 1; i <= 20; i++)
        {
            var product = new Product
            {
                Name = $"Test product {i}",
                Price = Random.Shared.NextDouble() * 100,
                Description = $"Test description {i}"
            };
            
            products.Add(product);
        }

        using var scope = _serviceProvider.CreateScope();
        var productContext = scope.ServiceProvider.GetService<ProductContext>()!;
        foreach (var product in products)
        {
            var result = await productContext.Create(product);
            if (result.Error != MasterDataError.None)
            {
                return false;
            }
        }

        return true;
    }
    
    #endregion
}