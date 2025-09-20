using ABInBev.Employees.Business.Models;
using System.ComponentModel.DataAnnotations;

namespace ABInBev.Employees.API.DTOs
{
    public class EmployeeDTO
    {
        public EmployeeDTO() { }

        public EmployeeDTO(Employee employee)
        {
            Id = employee.Id;
            BirthDate = employee.BirthDate;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            DocumentNumber = employee.DocumentNumber;
            Email = employee.Email;
            Phone1 = employee.Phone1;
            Phone2 = employee.Phone2;
        }

        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string DocumentNumber { get; set; } = string.Empty;

        [Required]
        public string Phone1 { get; set; } = string.Empty;

        [Required]
        public string Phone2 { get; set; } = string.Empty;

        //You cannot create a user with higher permissions than the current one.In other words, an employee cannot create a leader, and a leader cannot create a director.
        //public Employee Manager { get; set; }

        public string Password { get; set; } = string.Empty;

        //Must validate that the person is not a minor.
        public DateOnly BirthDate { get; set; }

        public Employee ToEmployee()
        {
            return new Employee
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                DocumentNumber = DocumentNumber,
                BirthDate = BirthDate,
                Phone1 = Phone1,
                Phone2 = Phone2
            };
        }
    }
}
