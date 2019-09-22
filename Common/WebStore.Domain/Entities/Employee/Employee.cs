using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities.Employee
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
