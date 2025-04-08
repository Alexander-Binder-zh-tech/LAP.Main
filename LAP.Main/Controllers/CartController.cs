using LAP.Main.Contexts.Cart;
using Lap.Model;
using Lap.Model.Models.Cart;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LAP.Main.Controllers;

[Route("/api/master-data/cart")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public CartController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    [HttpGet("get-all")]
    [SwaggerOperation("GetAllCart")]
    //[Authorize(Policy = BuildInUserRoles.AdminRole)]
    public async Task<IActionResult> GetAllCart([FromQuery] bool addDeleted)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<CartContext>()!;
        
        var result = await sc.GetAll(addDeleted);
        return result.Error switch
        {
            MasterDataError.None => Ok(result.Entity),
            MasterDataError.NotFound => NotFound(),
            _ => BadRequest("something bad happened")
        };
    }
    
    [HttpGet("get")]
    [SwaggerOperation("GetCart")]
    // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    public async Task<IActionResult> GetCart(int id, bool addDeleted)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<CartContext>()!;
        
        var result = await sc.Get(id, addDeleted);
        return result.Error switch
        {
            MasterDataError.None => Ok(result.Entity),
            MasterDataError.NotFound => NotFound(),
            _ => BadRequest("something bad happened")
        };
    }
    
    [HttpPost("create")]
    [SwaggerOperation("CreateCart")]
    // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    public async Task<IActionResult> CreateCart([FromBody]Cart cart)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<CartContext>()!;
        
        var result = await sc.Create(cart);
        if (result.Error == MasterDataError.None)
        {
            return Ok(result.Entity);
        }
        
        return BadRequest(result.Error);
    }
    
    [HttpPost("edit")]
    [SwaggerOperation("EditCart")]
    // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    public async Task<IActionResult> EditCart(int id, [FromBody] Cart cart)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<CartContext>()!;
        
        var result = await sc.Edit(id, cart);
        if (result.Error == MasterDataError.None)
        {
            return Ok(result.Entity);
        }
        
        return BadRequest(result.Error);
    }

    [HttpDelete("delete")]
    [SwaggerOperation("DeleteCart")]
    // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    public async Task<IActionResult> DeleteCart(int id)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<CartContext>()!;
        
        var result = await sc.SetDelete(id, true);
        return result.Error switch
        {
            MasterDataError.None => Ok(result.Entity),
            MasterDataError.NotFound => NotFound(id),
            _ => BadRequest("something bad happened")
        };
    }
}