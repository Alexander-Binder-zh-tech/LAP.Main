using System.ComponentModel.DataAnnotations.Schema;

namespace Lap.Model.Models.Address;

[Table("lap_address")]
public class Address : ModelBase
{
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
    public override void Set(ModelBase model)
    {
        var data = model as Address;
        if (data == null)
        {
            throw new ArgumentException($"called with wrong type -> should be {nameof(Address)}");
        }
        
        Street = data.Street;
        City = data.City;
        State = data.State;
        Country = data.Country;
        PostalCode = data.PostalCode;
    }
}