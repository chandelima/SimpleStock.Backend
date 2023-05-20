using System.Text.Json.Serialization;

namespace SimpleStock.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EOrderStatus
{
    Pending,
    Billed        
}
