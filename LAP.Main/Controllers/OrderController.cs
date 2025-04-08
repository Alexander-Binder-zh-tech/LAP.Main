using LAP.Main.Contexts.Address;
using LAP.Main.Contexts.Order;
using Lap.Model;
using Lap.Model.Models.Address;
using Lap.Model.Models.Order;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LAP.Main.Controllers;

[Route("/api/master-data/order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public OrderController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    [HttpGet("get-all")]
    [SwaggerOperation("GetAllOrders")]
    //[Authorize(Policy = BuildInUserRoles.AdminRole)]
    public async Task<IActionResult> GetAllOrders([FromQuery] bool addDeleted)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<OrderContext>()!;
        
        var result = await sc.GetAll(addDeleted);
        return result.Error switch
        {
            MasterDataError.None => Ok(result.Entity),
            MasterDataError.NotFound => NotFound(),
            _ => BadRequest("something bad happened")
        };
    }
    
    [HttpGet("get")]
    [SwaggerOperation("GetOrder")]
    // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    public async Task<IActionResult> GetOrder(int id, bool addDeleted)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<OrderContext>()!;
        
        var result = await sc.Get(id, addDeleted);
        return result.Error switch
        {
            MasterDataError.None => Ok(result.Entity),
            MasterDataError.NotFound => NotFound(),
            _ => BadRequest("something bad happened")
        };
    }
    
    [HttpPost("create")]
    [SwaggerOperation("CreateOrder")]
    // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<OrderContext>()!;
        
        var result = await sc.Create(order);
        if (result.Error == MasterDataError.None)
        {
            return Ok(result.Entity);
        }
        
        return BadRequest(result.Error);
    }
    
    [HttpPost("edit")]
    [SwaggerOperation("EditOrder")]
    // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    public async Task<IActionResult> EditOrder(int id, [FromBody] Order order)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<OrderContext>()!;
        
        var result = await sc.Edit(id, order);
        if (result.Error == MasterDataError.None)
        {
            return Ok(result.Entity);
        }
        
        return BadRequest(result.Error);
    }

    [HttpDelete("delete")]
    [SwaggerOperation("DeleteOrder")]
    // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<OrderContext>()!;
        
        var result = await sc.SetDelete(id, true);
        return result.Error switch
        {
            MasterDataError.None => Ok(result.Entity),
            MasterDataError.NotFound => NotFound(id),
            _ => BadRequest("something bad happened")
        };
    }
}