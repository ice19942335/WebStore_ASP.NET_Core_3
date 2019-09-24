using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Order;

namespace WebStore.Domain.DTO.Order
{
    public class OrderDTO : NamedEntity
    {
        public virtual User User { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<OrderItemDTO> OrderItems { get; set; }
    }
}
