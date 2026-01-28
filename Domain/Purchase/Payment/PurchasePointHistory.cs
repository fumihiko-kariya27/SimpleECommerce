using SimpleECommerce.Domain.User;

namespace SimpleECommerce.Domain.Purchase.Payment
{
    public class PurchasePointHistory
    {
        internal CustomerId CustomerId { get; init; }

        internal PurchasePoint Point { get; init; }

        internal PurchasePointHistoryType HistoryType { get; init; }

        internal DateTime OccurredAt { get; init; }

        private PurchasePointHistory(CustomerId customerId, PurchasePoint point, PurchasePointHistoryType type, DateTime occurredAt)
        { 
            CustomerId = customerId;
            Point = point;
            HistoryType = type;
            OccurredAt = occurredAt;
        }

        internal static PurchasePointHistory Earn(CustomerId customerId, PurchasePoint point)
        {
            return Earn(customerId, point, DateTime.Now);
        }

        internal static PurchasePointHistory Earn(CustomerId customerId, PurchasePoint point, DateTime occurredAt)
        {
            return new PurchasePointHistory
                (
                    customerId,
                    point,
                    PurchasePointHistoryType.Earned,
                    occurredAt
                );
        }

        internal static PurchasePointHistory Spend(CustomerId customerId, PurchasePoint point)
        {
            return Spend(customerId, point, DateTime.Now);
        }

        internal static PurchasePointHistory Spend(CustomerId customerId, PurchasePoint point, DateTime occurredAt)
        {
            return new PurchasePointHistory
                (
                    customerId,
                    point,
                    PurchasePointHistoryType.Spent,
                    occurredAt
                );
        }
    }
}
