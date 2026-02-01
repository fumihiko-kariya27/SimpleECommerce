using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Domain.Purchase.Payment;
using SimpleECommerce.Domain.User;
using SimpleECommerce.Models.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleECommerce.Models.Purchase
{
    public class PurchasePointHistoryModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        [Column("Customer")]
        public UserModel Customer { get; set; } = null!;

        public int Point { get; set; }

        public PurchasePointHistoryType HistoryType { get; set; }

        public DateTime OccurredAt { get; set; }
    }
}
