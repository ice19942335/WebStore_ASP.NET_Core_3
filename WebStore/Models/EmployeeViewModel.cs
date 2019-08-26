﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Models
{
    public class EmployeeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "First name")]
        [MinLength(2, ErrorMessage = "Min 2 symbols")]
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression("^[A-Z][a-z]{2,150}$", ErrorMessage = "Name have to begin with capital letter")]
        public string FirstName { get; set; }

        public string Patronymic { get; set; }

        [MinLength(2, ErrorMessage = "Min 2 symbols")]
        [Required(ErrorMessage = "Surname is required")]
        [RegularExpression("^[A-Z][a-z]{2,150}$", ErrorMessage = "Surname have to begin with capital letter")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(16, 65, ErrorMessage = "Age range is (16 - 65)")]
        public int Age { get; set; }
    }
}
