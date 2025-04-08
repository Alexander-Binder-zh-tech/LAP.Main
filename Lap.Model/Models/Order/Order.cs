using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Lap.Model.Models.Order;

[Table("lap_order")]
public class Order : ModelBase
{
    public required int CustomerId { get; set; }
    public required DateTime OrderDate { get; set; }
    public required double Total { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required PaymentType PaymentType { get; set; }
    
    //Navigation prop for EF mapping
    [ForeignKey(nameof(CustomerId))]
    public Customer.Customer Customer { get; set; } = null!;
    
    public override void Set(ModelBase model)
    {
        var data = model as Order;
        if (data == null)
        {
            throw new ArgumentException($"called with wrong type -> should be {nameof(Order)}");
        }
        
        CustomerId = data.CustomerId;
        
        OrderDate = data.OrderDate;
        Total = data.Total;
        PaymentType = data.PaymentType;
    }
}

public enum PaymentType
{
    Bill,
    CreditCard
}