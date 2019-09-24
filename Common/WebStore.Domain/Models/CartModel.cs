using System.Collections.Generic;
using System.Linq;

namespace WebStore.Domain.Models
{
    public class CartModel
    {
        public List<CartItemModel> Items { get; set; } = new List<CartItemModel>();

        public int ItemsCount => Items?.Sum(item => item.Quantity) ?? 0;
    }
}