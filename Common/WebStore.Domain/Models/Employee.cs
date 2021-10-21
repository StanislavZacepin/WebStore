using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Models
{
    /// <summary> Информация о сотруднике </summary>
    public class Employee //: NamedEntity//, IOrderedEntity
    {
        /// <summary> Идентификатор сотрудника </summary>
        public int Id { get; set; }
        /// <summary> Имя сотрудника </summary>
        public string FirstName { get; set; }
        /// <summary> Фамилия сотрудника </summary>
        public string LastName { get; set; }
        /// <summary> Отчество сотрудника </summary>
        public string Patronymic { get; set; }
        /// <summary> Возраст сотрудника </summary>
        public int Age { get; set; }
        /// <summary> О сотруднике </summary>
        public string AboutTheEmployee { get; set; }


    }
    // public record Employee2(int Id, string LastName, string FirstName, string Patronymic, int Age, string AboutTheEmployee)
}
