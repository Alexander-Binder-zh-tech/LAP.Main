using System.ComponentModel.DataAnnotations.Schema;

namespace Lap.Model.Models.Contact;

[Table("lap_contact")]
public class Contact : ModelBase
{
    public string? TelephoneNumber { get; set; }
    public required string Email { get; set; }
    public required int AddressId { get; set; }
    public required int DeliveryAddressId { get; set; }
    
    //Navigation props for EF mapping
    [ForeignKey(nameof(AddressId))]
    public Address.Address Address { get; set; } = null!;

    [ForeignKey(nameof(DeliveryAddressId))]
    public Address.Address DeliveryAddress { get; set; } = null!;

    public override void Set(ModelBase model)
    {
        var data = model as Contact;
        if (data == null)
        {
            throw new ArgumentException($"called with wrong type -> should be {nameof(Contact)}");
        }
        
        TelephoneNumber = data.TelephoneNumber;
        Email = data.Email;
        AddressId = data.AddressId;
        DeliveryAddressId = data.DeliveryAddressId;
    }
}