using LAP.Main.Contexts.Product;
using Lap.Model;
using Lap.Model.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LAP.Main.Controllers;

[Route("/api/master-data/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public ProductController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    [HttpGet("get-all")]
    [SwaggerOperation("GetAllProducts")]
    //[Authorize(Policy = BuildInUserRoles.AdminRole)]
    public async Task<IActionResult> GetAllProducts([FromQuery] bool addDeleted)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<ProductContext>()!;
        
        var result = await sc.GetAll(addDeleted);
        return result.Error switch
        {
            MasterDataError.None => Ok(result.Entity),
            MasterDataError.NotFound => NotFound(),
            _ => BadRequest("something bad happened")
        };
    }
    
    [HttpGet("get")]
    [SwaggerOperation("GetProduct")]
    // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    public async Task<IActionResult> GetProduct(int id, bool addDeleted)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<ProductContext>()!;
        
        var result = await sc.Get(id, addDeleted);
        return result.Error switch
        {
            MasterDataError.None => Ok(result.Entity),
            MasterDataError.NotFound => NotFound(),
            _ => BadRequest("something bad happened")
        };
    }
    
    [HttpPost("create")]
    [SwaggerOperation("CreateProduct")]
    // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    public async Task<IActionResult> CreateProduct([FromBody] Product product)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<ProductContext>()!;
        
        var result = await sc.Create(product);
        if (result.Error == MasterDataError.None)
        {
            return Ok(result.Entity);
        }
        
        return BadRequest(result.Error);
    }
    
    [HttpPost("edit")]
    [SwaggerOperation("EditProduct")]
    // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    public async Task<IActionResult> EditProduct(int id, [FromBody] Product product)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<ProductContext>()!;
        
        var result = await sc.Edit(id, product);
        if (result.Error == MasterDataError.None)
        {
            return Ok(result.Entity);
        }
        
        return BadRequest(result.Error);
    }

    [HttpDelete("delete")]
    [SwaggerOperation("DeleteProduct")]
    // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<ProductContext>()!;
        
        var result = await sc.SetDelete(id, true);
        return result.Error switch
        {
            MasterDataError.None => Ok(result.Entity),
            MasterDataError.NotFound => NotFound(id),
            _ => BadRequest("something bad happened")
        };
    }
}