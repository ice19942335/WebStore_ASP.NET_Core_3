using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.ViewModels;

namespace WebStore._Infrastructure.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetUserOrders(string userName);

        Order GetOrderById(int id);

        Task<Order> CreateOrder(OrderViewModel orderModel, CartViewModel cartModel, string userName);
    }
}
