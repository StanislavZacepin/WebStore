using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Domain.ViewModels
{
    public class EmployeeViewModel : IValidatableObject
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }


        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя не указано")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Длина от 2 до 200 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Неверный формат")]
        public string Name { get; set; }


        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия не указано")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Длина от 2 до 200 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Неверный формат")]
        public string LastName { get; set; }


        [Display(Name = "Отчество")]
        [Required(ErrorMessage = "Отчество не указано")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Длина от 2 до 200 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Неверный формат")]

        public string Patronymic { get; set; }
        [Display(Name = "Возраст")]
        [Required(ErrorMessage = "Возраст не указано")]
        [Range(18, 65, ErrorMessage = "Возраст должен быть от 18 до 65 лет")]

        public int Age { get; set; }
        [Display(Name = "О сотруднике")]
        public string AboutTheEmployee { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext Context)
        {
            switch (Context.MemberName)
            {
                // default: return Enumerable.Empty<ValidationResult>();
                default: return new[] { ValidationResult.Success };

                case nameof(Age):
                    if (Age < 15 || Age > 90) return new[]
                    { new ValidationResult("Странный возраст", new[] { nameof(Age) }) };
                    return new[] { ValidationResult.Success };
            }
        }
    }
}
