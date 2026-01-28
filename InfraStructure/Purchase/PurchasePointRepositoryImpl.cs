using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Domain.Purchase.Payment;
using SimpleECommerce.Domain.User;
using SimpleECommerce.Models.Context;
using SimpleECommerce.Models.Purchase;
using SimpleECommerce.Service.Purchase;

namespace SimpleECommerce.InfraStructure.Purchase
{
    internal class PurchasePointRepositoryImpl : IPurchasePointRepository
    {
        private readonly ECommerceDbContext _context;

        public PurchasePointRepositoryImpl(ECommerceDbContext context) 
        {
            _context = context;
        }

        public async Task InsertHistoryAsync(PurchasePointHistory history)
        {
            int nextId = _context.PurchasePointHistories.Max(p => p.Id) + 1;
            PurchasePointHistoryModel model = new();
            model.Id = nextId;
            model.UserId = history.CustomerId.Value;
            model.Point = history.Point.Value;
            model.HistoryType = history.HistoryType;
            model.OccurredAt = history.OccurredAt;

            _context.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<PurchasePointHistory>> SelectHistoryByUserIdAsync(CustomerId customerId)
        {
            List<PurchasePointHistoryModel> histories = await _context.PurchasePointHistories
                .Where(p => p.UserId.Equals(customerId.Value)).ToListAsync();

            List<PurchasePointHistory> ret = [];
            foreach (PurchasePointHistoryModel history in histories)
            {
                CustomerId cId = new CustomerId(history.UserId);
                PurchasePoint point = new PurchasePoint(history.Point);
                PurchasePointHistoryType type = 0;
                if (Enum.IsDefined(typeof(PurchasePointHistoryType), history.HistoryType))
                {
                    type = history.HistoryType;
                }
                else 
                {
                    throw new InvalidDataException($"{history.HistoryType}はポイント区分の値として使用できません");
                }

                PurchasePointHistory h = null!;
                if (type == PurchasePointHistoryType.Earned)
                {
                    h = PurchasePointHistory.Earn(cId, point, history.OccurredAt);
                }
                else
                { 
                    h = PurchasePointHistory.Spend(cId, point, history.OccurredAt);
                }
                ret.Add(h);
            }

            return ret;
        }
    }
}
