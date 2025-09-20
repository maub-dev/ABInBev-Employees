using System.ComponentModel.DataAnnotations;

namespace ABInBev.Employees.Business.Models
{
    public class Employee : Entity
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string DocumentNumber { get; set; }

        public string UserIdentityId { get; set; }

        //Must validate that the person is not a minor.
        public DateOnly BirthDate { get; set; }

        [Required]
        public string Phone1 { get; set; }

        [Required]
        public string Phone2 { get; set; }
        //You cannot create a user with higher permissions than the current one.In other words, an employee cannot create a leader, and a leader cannot create a director.
        //public Employee? Manager { get; set; }
    }
}
