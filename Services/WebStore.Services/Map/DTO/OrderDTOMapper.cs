using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Domain.DTO.Order;
using WebStore.Domain.Entities.Order;

namespace WebStore.Services.Map.DTO
{
    public static class OrderDTOMapper
    {
        private static void CopyToOrderDTO(this Order order, OrderDTO dto)
        {
            dto.Id = order.Id;
            dto.Name = order.Name;
            dto.User = order.User;
            dto.Address = order.Address;
            dto.Date = order.Date;
            dto.Phone = order.Phone;
            dto.OrderItems = order.OrderItems?.Select(o => new OrderItemDTO
            {
                Id = o.Id,
                Price = o.Price,
                Quantity = o.Quantity
            }).ToList();
        }

        public static OrderDTO CreateOrderDTO(this Order order)
        {
            var dto = new OrderDTO();
            order.CopyToOrderDTO(dto);
            return dto;
        }

        private static void CopyToOrder(this OrderDTO dto, Order order)
        {
            order.Id = dto.Id;
            order.Name = dto.Name;
            order.User = dto.User;
            order.Address = dto.Address;
            order.Date = dto.Date;
            order.Phone = dto.Phone;
            order.OrderItems = dto.OrderItems?.Select(o => new OrderItem
            {
                Id = o.Id,
                Price = o.Price,
                Quantity = o.Quantity
            }).ToList();
        }

        public static Order CreateOrder(this OrderDTO dto)
        {
            var order = new Order();
            dto.CopyToOrder(order);
            return order;
        }
    }
}


//public virtual User User { get; set; }

//public string Phone { get; set; }

//public string Address { get; set; }

//public DateTime Date { get; set; }

//public virtual ICollection<OrderItem> OrderItems { get; set; }