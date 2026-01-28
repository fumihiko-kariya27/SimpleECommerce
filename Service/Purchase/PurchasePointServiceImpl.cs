using SimpleECommerce.Domain.Purchase.Choise;
using SimpleECommerce.Domain.Purchase.Payment;
using SimpleECommerce.Domain.User;

namespace SimpleECommerce.Service.Purchase
{
    internal class PurchasePointServiceImpl : IPurchasePointService
    {
        private readonly IPurchasePointRepository _repository;

        public PurchasePointServiceImpl(IPurchasePointRepository repository)
        { 
            _repository = repository;
        }

        public async Task AddHistoryAsync(PurchasePointHistory history)
        {
            await _repository.InsertHistoryAsync(history);
        }

        public async Task<PurchasePoint> GetBalanceByCustomerIdAsync(CustomerId customerId)
        {
            IReadOnlyList<PurchasePointHistory> history = await GetHistoryByCustomerIdAsync(customerId);
            int balance = history.Sum(ph => ph.HistoryType == PurchasePointHistoryType.Earned ? ph.Point.Value : -ph.Point.Value);
            return new PurchasePoint(balance);
        }

        public async Task<IReadOnlyList<PurchasePointHistory>> GetHistoryByCustomerIdAsync(CustomerId customerId)
        {
            return await _repository.SelectHistoryByUserIdAsync(customerId);
        }

        public async Task GrantDailyPointAsync(CustomerId customerId)
        {
            PurchasePoint bonus = PurchasePoint.LoginPointAtDay;
            PurchasePointHistory history = PurchasePointHistory.Earn(customerId, bonus);
            await _repository.InsertHistoryAsync(history);
        }
    }
}
