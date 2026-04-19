namespace SimpleECommerce.Domain.Purchase.Shopping
{
    internal class ShoppingCart
    {
        // 一度の購入における商品数上限
        private static readonly int CAPACITY = 100;

        private List<CartLine> cart;

        internal ShoppingCart()
        {
            cart = new List<CartLine>(CAPACITY);
        }

        internal void Add(CartLine line)
        {
            if (CAPACITY <= cart.Count)
            {
                throw new CartOverFlowException($"カート容量が不足しているため、商品[{line.Name.Value}]を追加できません");
            }

            var existing = cart.FirstOrDefault(c => c.ProductId == line.ProductId);
            if (existing != null)
            {
                // 同じ商品が既にカートに入っている場合は1行にまとめる
                existing.ChangeQuantity(existing.Quantity.Value + line.Quantity.Value);
            }
            else
            {
                cart.Add(line);
            }
        }

        internal void Remove(CartLine line)
        {
            bool ret = cart.Remove(line);
            if (!ret)
            {
                throw new CartLineNotCanceledException($"商品[{line.Name.Value}]をキャンセルできませんでした");
            }
        }

        internal void Clear()
        {
            cart.Clear();
        }

        internal CartPrice Amount
        {
            get
            {
                int total = 0;
                foreach (var line in cart)
                {
                    total += line.TotalPrice.Value;
                }
                return new CartPrice(total);
            }
        }
    }
}
