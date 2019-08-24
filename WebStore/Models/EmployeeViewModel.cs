using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First name")] 
        public string FirstName { get; set; }

        public string Patronymic { get; set; }

        [Display(Name = "Sure name")]
        public string SureName { get; set; }

        [Display(Name = "Age")]
        public int Age { get; set; }

    }
}
