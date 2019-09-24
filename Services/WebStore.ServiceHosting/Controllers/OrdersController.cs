using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO.Order;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase, IOrderService
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService) => _orderService = orderService;

        [HttpGet("user/{userName}")]
        public IEnumerable<OrderDTO> GetUserOrders(string userName)
        {
            return _orderService.GetUserOrders(userName);
        }

        [HttpGet("{id}")]
        public OrderDTO GetOrderById(int id)
        {
            return _orderService.GetOrderById(id);
        }

        [HttpPost("{userName}")]
        public OrderDTO CreateOrder(OrderModel orderModel, string userName)
        {
            return _orderService.CreateOrder(orderModel, userName);
        }
    }
}