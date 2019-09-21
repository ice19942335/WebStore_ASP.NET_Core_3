using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels.Order;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IOrderService _orderService;
        public ProfileController(IOrderService orderService) => _orderService = orderService;

        public IActionResult Index() => View();

        public IActionResult Orders()
        {
            var orders = _orderService.GetUserOrders(User.Identity.Name);

            return View(orders.Select(order => new UserOrderViewModel
            {
                Id = order.Id,
                Name = order.Name,
                Phone = order.Phone,
                Address = order.Address,
                TotalSum = order.OrderItems.Sum(orderItem => orderItem.Price * orderItem.Quantity)
            }));
        }
    }
}