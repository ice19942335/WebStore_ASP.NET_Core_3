using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.DTO.Order;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration configuration) : base(configuration, "api/orders") { }


        public IEnumerable<OrderDTO> GetUserOrders(string userName)
        {
            return Get<List<OrderDTO>>($"{_serviceAddress}/user/{userName}");
        }

        public OrderDTO GetOrderById(int id)
        {
            return Get<OrderDTO>($"{_serviceAddress}/{id}");
        }

        public OrderDTO CreateOrder(OrderModel orderModel, string userName)
        {
            var response = Post($"{_serviceAddress}/{userName}", orderModel);
            return response.Content.ReadAsAsync<OrderDTO>().Result;
        }
    }
}
