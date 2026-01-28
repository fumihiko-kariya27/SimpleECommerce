using SimpleECommerce.Domain.Purchase.Payment;
using SimpleECommerce.Domain.User;

namespace SimpleECommerce.Service.Purchase
{
    public interface IPurchasePointRepository
    {
        Task InsertHistoryAsync(PurchasePointHistory history);

        Task<IReadOnlyList<PurchasePointHistory>> SelectHistoryByUserIdAsync(CustomerId customerId);
    }
}
