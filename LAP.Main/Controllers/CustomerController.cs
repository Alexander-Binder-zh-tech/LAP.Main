using LAP.Main.Contexts.Customer;
using Lap.Model;
using Lap.Model.Models.Customer;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LAP.Main.Controllers;

[Route("/api/master-data/customer")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public CustomerController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    [HttpGet("get-all")]
    [SwaggerOperation("GetAllCustomers")]
    //[Authorize(Policy = BuildInUserRoles.AdminRole)]
    public async Task<IActionResult> GetAllCustomers([FromQuery] bool addDeleted)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<CustomerContext>()!;
        
        var result = await sc.GetAll(addDeleted);
        return result.Error switch
        {
            MasterDataError.None => Ok(result.Entity),
            MasterDataError.NotFound => NotFound(),
            _ => BadRequest("something bad happened")
        };
    }
    
    [HttpGet("get")]
    [SwaggerOperation("GetCustomer")]
    // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    public async Task<IActionResult> GetCustomer(int id, bool addDeleted)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<CustomerContext>()!;
        
        var result = await sc.Get(id, addDeleted);
        return result.Error switch
        {
            MasterDataError.None => Ok(result.Entity),
            MasterDataError.NotFound => NotFound(),
            _ => BadRequest("something bad happened")
        };
    }
    
    [HttpPost("create")]
    [SwaggerOperation("CreateCustomer")]
    // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    public async Task<IActionResult> CreateCustomer([FromBody]Customer customer)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<CustomerContext>()!;
        
        var result = await sc.Create(customer);
        if (result.Error == MasterDataError.None)
        {
            return Ok(result.Entity);
        }
        
        return BadRequest(result.Error);
    }
    
    [HttpPost("edit")]
    [SwaggerOperation("EditCustomer")]
    // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    public async Task<IActionResult> EditCustomer(int id, [FromBody] Customer customer)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<CustomerContext>()!;
        
        var result = await sc.Edit(id, customer);
        if (result.Error == MasterDataError.None)
        {
            return Ok(result.Entity);
        }
        
        return BadRequest(result.Error);
    }

    [HttpDelete("delete")]
    [SwaggerOperation("DeleteCustomer")]
    // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<CustomerContext>()!;
        
        var result = await sc.SetDelete(id, true);
        return result.Error switch
        {
            MasterDataError.None => Ok(result.Entity),
            MasterDataError.NotFound => NotFound(id),
            _ => BadRequest("something bad happened")
        };
    }
}