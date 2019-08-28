using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.ViewModels
{
    public class EmployeeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "First name")]
        [MinLength(2, ErrorMessage = "Min 2 symbols")]
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression("^[A-Z][a-z]{2,150}$", ErrorMessage = "The name must begin with a capital letter and contain only characters of the alphabet")]
        public string FirstName { get; set; }

        public string Patronymic { get; set; }

        [MinLength(2, ErrorMessage = "Min 2 symbols")]
        [Required(ErrorMessage = "Surname is required")]
        [RegularExpression("^[A-Z][a-z]{2,150}$", ErrorMessage = "The surname must begin with a capital letter and contain only alphabetical characters")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(16, 65, ErrorMessage = "Age range is (16 - 65)")]
        public int Age { get; set; }
    }
}
