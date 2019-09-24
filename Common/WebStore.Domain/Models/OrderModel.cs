using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.DTO.Order;
using WebStore.Domain.ViewModels.Order;

namespace WebStore.Domain.Models
{
    public class OrderModel
    {
        public OrderViewModel OrderViewModel { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
