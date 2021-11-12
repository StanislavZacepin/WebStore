using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.ViewModels.Identity
{
    public class RegisterUserViewModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        [Remote("IsNameFree", "Account")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Пороль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Потверждение пароля")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }
    }
}
