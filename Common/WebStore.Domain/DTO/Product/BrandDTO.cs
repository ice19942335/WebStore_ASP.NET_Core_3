using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Entities;

namespace WebStore.Domain.DTO.Product
{
    public class BrandDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
