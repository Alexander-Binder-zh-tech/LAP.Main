using System.ComponentModel.DataAnnotations.Schema;

namespace Lap.Model.Models.Customer;

[Table("lap_customer")]
public class Customer : ModelBase
{
    public required string Name { get; set; }
    public required int ContactId { get; set; }
    
    //Navigation prop for EF mapping
    [ForeignKey(nameof(ContactId))]
    public Contact.Contact Contact { get; set; } = null!;
    
    //Back-Navigation prop for EF mapping
    public List<Order.Order>? Orders { get; set; } = null!;

    public override void Set(ModelBase model)
    {
        var data = model as Customer;
        if (data == null)
        {
            throw new ArgumentException($"called with wrong type -> should be {nameof(Customer)}");
        }
        
        Name = data.Name;
        ContactId = data.ContactId;
    }    
}