

namespace DedicatedService.DTOs
{
    public record OrderSummary
    (
        int Id,
        int ItemId,
        string ItemName,
        decimal ItemPrice,
        int TotalQuantity,
        decimal TotalAmmount,
        DateTime OrderDate
    );
}
