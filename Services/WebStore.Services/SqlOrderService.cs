using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels.Cart;
using WebStore.Domain.ViewModels.Order;
using WebStore.Interfaces.Services;

namespace WebStore.Services
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

        public IEnumerable<Order> GetUserOrders(string userName)
        {
            return _context.Orders
                .Include(order => order.User)
                .Include(order => order.OrderItems)
                .Where(order => order.User.UserName == userName)
                .ToArray();
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders
                .Include(order => order.OrderItems)
                .FirstOrDefault(order => order.Id == id);
        }

        public async Task<Order> CreateOrder(OrderViewModel orderModel, CartViewModel cartModel, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            await using var transaction = _context.Database.BeginTransaction();
            //Creating Order entry ------------------------------------------------------------------------------------------------------
            var order = new Order
            {
                Name = orderModel.Name,
                Address = orderModel.Address,
                Phone = orderModel.Phone,
                User = user,
                Date = DateTime.Now
            };

            await _context.Orders.AddAsync(order);

            //Creating OrderItems entry -------------------------------------------------------------------------------------------------
            foreach (var (productViewModel, quantity) in cartModel.Items)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productViewModel.Id);
                if(product is null)
                    throw  new InvalidOperationException($"Product in database with ID: {productViewModel.Id} not found");

                var orderItem = new OrderItem
                {
                    Order = order,
                    Price = product.Price,
                    Quantity = quantity,
                    Product = product
                };

                await _context.OrderItems.AddAsync(orderItem);
            }

            //Saving changes ------------------------------------------------------------------------------------------------------------
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return order;
        }
    }
}
