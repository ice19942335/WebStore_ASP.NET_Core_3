using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.ViewModels.Account
{
    public class RegisterUserViewModel
    {
        [Required]
        [MaxLength(256, ErrorMessage = "Max 256 symbols")]
        [MinLength(8, ErrorMessage = "Username required minimum 8 symbols")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(256, ErrorMessage = "Max 256 symbols")]
        [MinLength(8, ErrorMessage = "Password required minimum 8 symbols")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MaxLength(256, ErrorMessage = "Max 256 symbols")]
        [MinLength(8, ErrorMessage = "Password required minimum 8 symbols")]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
