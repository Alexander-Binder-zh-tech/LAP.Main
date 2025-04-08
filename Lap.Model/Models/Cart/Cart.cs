using System.ComponentModel.DataAnnotations.Schema;

namespace Lap.Model.Models.Cart;

[Table("lap_cart")]
//Acts as mapping between Products and Orders - also references to customer
public class Cart : ModelBase
{
    public int? OrderId { get; set; }
    
    //Navigation props for EF mapping
    public List<Product.Product>? Products { get; set; }
    public Order.Order Order { get; set; } = null!;
    
    public override void Set(ModelBase model)
    {
        var data = model as Cart;
        if (data == null)
        {
            throw new ArgumentException($"called with wrong type -> should be {nameof(Cart)}");
        }
        
        OrderId = data.OrderId;
    }
}