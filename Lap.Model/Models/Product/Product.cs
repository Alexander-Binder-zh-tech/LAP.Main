using System.ComponentModel.DataAnnotations.Schema;

namespace Lap.Model.Models.Product;

[Table("lap_product")]
public class Product : ModelBase
{
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public string? Description { get; set; }
    
    public override void Set(ModelBase model)
    {
        var data = model as Product;
        if (data == null)
        {
            throw new ArgumentException($"called with wrong type -> should be {nameof(Product)}");
        }
        
        Name = data.Name;
        Price = data.Price;
        Description = data.Description;
    }
}