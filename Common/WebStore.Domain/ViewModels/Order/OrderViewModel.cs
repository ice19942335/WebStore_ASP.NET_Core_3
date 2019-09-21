﻿using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.ViewModels.Order
{
    public class OrderViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public string Address { get; set; }
    }
}
