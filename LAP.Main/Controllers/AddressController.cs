using LAP.Main.Contexts.Address;
using Lap.Model;
using Lap.Model.Models.Address;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LAP.Main.Controllers;

[Route("/api/master-data/address")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public AddressController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    [HttpGet("get-all")]
    [SwaggerOperation("GetAllAddresses")]
    //[Authorize(Policy = BuildInUserRoles.AdminRole)]
    public async Task<IActionResult> GetAllAddresses([FromQuery] bool addDeleted)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<AddressContext>()!;
        
        var result = await sc.GetAll(addDeleted);
        return result.Error switch
        {
            MasterDataError.None => Ok(result.Entity),
            MasterDataError.NotFound => NotFound(),
            _ => BadRequest("something bad happened")
        };
    }
    
    [HttpGet("get")]
    [SwaggerOperation("GetAddress")]
    // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    public async Task<IActionResult> GetAddress(int id, bool addDeleted)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<AddressContext>()!;
        
        var result = await sc.Get(id, addDeleted);
        return result.Error switch
        {
            MasterDataError.None => Ok(result.Entity),
            MasterDataError.NotFound => NotFound(),
            _ => BadRequest("something bad happened")
        };
    }
    
    [HttpPost("create")]
    [SwaggerOperation("CreateAddress")]
    // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    public async Task<IActionResult> CreateAddress([FromBody]Address address)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<AddressContext>()!;
        
        var result = await sc.Create(address);
        if (result.Error == MasterDataError.None)
        {
            return Ok(result.Entity);
        }
        
        return BadRequest(result.Error);
    }
    
    [HttpPost("edit")]
    [SwaggerOperation("EditAddress")]
    // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    public async Task<IActionResult> EditAddress(int id, [FromBody] Address address)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<AddressContext>()!;
        
        var result = await sc.Edit(id, address);
        if (result.Error == MasterDataError.None)
        {
            return Ok(result.Entity);
        }
        
        return BadRequest(result.Error);
    }

    [HttpDelete("delete")]
    [SwaggerOperation("DeleteAddress")]
    // [Authorize(Policy = BuildInUserRoles.Authenticated)]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        using var scope = _serviceProvider.CreateScope();
        var sc = scope.ServiceProvider.GetService<AddressContext>()!;
        
        var result = await sc.SetDelete(id, true);
        return result.Error switch
        {
            MasterDataError.None => Ok(result.Entity),
            MasterDataError.NotFound => NotFound(id),
            _ => BadRequest("something bad happened")
        };
    }
}