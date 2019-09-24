using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.DTO.Order;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Order;
using WebStore.Domain.Models;
using WebStore.Domain.ViewModels.Cart;
using WebStore.Domain.ViewModels.Order;
using WebStore.Interfaces.Services;
using WebStore.Services.Map.DTO;

namespace WebStore.Services.SQL
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreContext _context;
        private readonly UserManager<User> _userManager;

        public SqlOrderService(WebStoreContext ctx, UserManager<User> userManager)
        {
            _context = ctx;
            _userManager = userManager;
        }

        public IEnumerable<OrderDTO> GetUserOrders(string userName)
        {
            return _context.Orders
                .Include(order => order.User)
                .Include(order => order.OrderItems)
                .Where(order => order.User.UserName == userName)
                .Select(order => order.CreateOrderDTO())
                .ToArray();
        }

        public OrderDTO GetOrderById(int id)
        {
            return _context.Orders
                .Include(order => order.OrderItems)
                .FirstOrDefault(order => order.Id == id)
                .CreateOrderDTO();
        }

        public OrderDTO CreateOrder(OrderModel orderModel, string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            using var transaction = _context.Database.BeginTransaction();
            //Creating Order entry ------------------------------------------------------------------------------------------------------
            var order = new Order
            {
                Name = orderModel.OrderViewModel.Name,
                Address = orderModel.OrderViewModel.Address,
                Phone = orderModel.OrderViewModel.Phone,
                User = user,
                Date = DateTime.Now
            };

            _context.Orders.Add(order);

            //Creating OrderItems entry -------------------------------------------------------------------------------------------------
            foreach (var orderItemDTO in orderModel.OrderItems)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == orderItemDTO.Id);
                if(product is null)
                    throw  new InvalidOperationException($"Product in database with ID: {orderItemDTO.Id} not found");

                var orderItem = new OrderItem
                {
                    Order = order,
                    Price = product.Price,
                    Quantity = orderItemDTO.Quantity,
                    Product = product
                };

                _context.OrderItems.Add(orderItem);
            }

            //Saving changes ------------------------------------------------------------------------------------------------------------
            _context.SaveChanges();
            transaction.Commit();

            return order.CreateOrderDTO();
        }
    }
}
