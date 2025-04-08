using LAP.Main.Contexts.Product;
using Lap.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LAP.Main.Controllers;

[Route("/api/statistics")]
[ApiController]
public class StatisticController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public StatisticController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    //TODO finish for 5 lowest, 5 highest and orders in last 4 weeks
    
    // [HttpGet("rarest-product")]
    // [SwaggerOperation("GetRareProducts")]
    // //[Authorize(Policy = BuildInUserRoles.AdminRole)]
    // public async Task<IActionResult> GetRareProducts()
    // {
    //     using var scope = _serviceProvider.CreateScope();
    //     var sc = scope.ServiceProvider.GetService<ProductContext>()!;
    //     
    //     var result = await sc.GetAll(addDeleted);
    //     return result.Error switch
    //     {
    //         MasterDataError.None => Ok(result.Entity),
    //         MasterDataError.NotFound => NotFound(),
    //         _ => BadRequest("something bad happened")
    //     };
    // }
    //
    // [HttpGet("get")]
    // [SwaggerOperation("GetProduct")]
    // // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    // public async Task<IActionResult> GetProduct(int id, bool addDeleted)
    // {
    //     using var scope = _serviceProvider.CreateScope();
    //     var sc = scope.ServiceProvider.GetService<ProductContext>()!;
    //     
    //     var result = await sc.Get(id, addDeleted);
    //     return result.Error switch
    //     {
    //         MasterDataError.None => Ok(result.Entity),
    //         MasterDataError.NotFound => NotFound(),
    //         _ => BadRequest("something bad happened")
    //     };
    // }
    //
    // [HttpPost("create")]
    // [SwaggerOperation("CreateProduct")]
    // // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    // public async Task<IActionResult> CreateProduct([FromBody] Product product)
    // {
    //     using var scope = _serviceProvider.CreateScope();
    //     var sc = scope.ServiceProvider.GetService<ProductContext>()!;
    //     
    //     var result = await sc.Create(product);
    //     if (result.Error == MasterDataError.None)
    //     {
    //         return Ok(result.Entity);
    //     }
    //     
    //     return BadRequest(result.Error);
    // }
}