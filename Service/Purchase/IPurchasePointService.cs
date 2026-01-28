using SimpleECommerce.Domain.Purchase.Choise;
using SimpleECommerce.Domain.Purchase.Payment;
using SimpleECommerce.Domain.User;

namespace SimpleECommerce.Service.Purchase
{
    public interface IPurchasePointService
    {
        Task GrantDailyPointAsync(CustomerId customerId);

        Task AddHistoryAsync(PurchasePointHistory history);

        Task<IReadOnlyList<PurchasePointHistory>> GetHistoryByCustomerIdAsync(CustomerId customerId);

        Task<PurchasePoint> GetBalanceByCustomerIdAsync(CustomerId customerId);
    }
}
