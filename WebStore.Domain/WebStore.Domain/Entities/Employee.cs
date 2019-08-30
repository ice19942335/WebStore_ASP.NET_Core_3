using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities
{
    public class Employee : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }

        public string Patronymic { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public int Age { get; set; }
    }
}
