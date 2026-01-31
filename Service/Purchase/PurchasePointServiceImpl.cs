using SimpleECommerce.Domain.Purchase.Choise;
using SimpleECommerce.Domain.Purchase.Payment;
using SimpleECommerce.Domain.User;
using SimpleECommerce.Service.User;

namespace SimpleECommerce.Service.Purchase
{
    internal class PurchasePointServiceImpl : IPurchasePointService
    {
        private readonly IPurchasePointRepository _repository;
        private readonly IUserService _userService;

        public PurchasePointServiceImpl(IPurchasePointRepository repository, IUserService userService)
        { 
            _repository = repository;
            _userService = userService;
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
            IDomainUser? user = _userService.FindAsync(customerId);
            if (user == null)
            {
                return;
            }

            // 本来は日次単位で最初にログインした時に付与であるが、いったん実装保留

            PurchasePoint bonus = PurchasePoint.LoginBonusPerDay;
            PurchasePointHistory history = PurchasePointHistory.Earn(customerId, bonus);
            await _repository.InsertHistoryAsync(history);
        }
    }
}
