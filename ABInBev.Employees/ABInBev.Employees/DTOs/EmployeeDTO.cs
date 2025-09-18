using ABInBev.Employees.Business.Models;
using ABInBev.Employees.Business.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ABInBev.Employees.API.DTOs
{
    public class EmployeeDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string DocumentNumber { get; set; }

        //More than one
        public List<PhonebookDTO> Phonebook { get; set; }

        //You cannot create a user with higher permissions than the current one.In other words, an employee cannot create a leader, and a leader cannot create a director.
        //public Employee Manager { get; set; }

        public string Password { get; set; }

        //Must validate that the person is not a minor.
        public DateOnly BirthDate { get; set; }

        public Employee ToEmployee()
        {
            return new Employee
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                DocumentNumber = DocumentNumber,
                BirthDate = BirthDate,
                Phones = Phonebook.Select(x => new Phonebook { Type = (PhoneType)x.Type, PhoneNumber = x.Phone }).ToList()
            };
        }
    }
}
