using ABInBev.Employees.Business.Models.Enums;

namespace ABInBev.Employees.Business.Models
{
    public class Phonebook : Entity
    {
        public PhoneType Type { get; set; }
        public string PhoneNumber { get; set; }
    }
}
