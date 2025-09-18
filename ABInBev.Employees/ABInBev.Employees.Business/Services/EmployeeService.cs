using ABInBev.Employees.Business.Interfaces;
using ABInBev.Employees.Business.Models;

namespace ABInBev.Employees.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IPhonebookRepository _phonebookRepository;

        public EmployeeService(IEmployeeRepository repository, IPhonebookRepository phonebookRepository)
        {
            _repository = repository;
            _phonebookRepository = phonebookRepository;
        }

        public async Task AddAsync(Employee employee)
        {
            foreach (var phone in employee.Phones)
            {
                phone.Employee = employee;
            }
            await _repository.AddAsync(employee);
        }

        public async Task UpdateAsync(Employee employee)
        {
            var employeeDb = await _repository.GetByIdAsync(employee.Id);
            if (employeeDb is null)
            {
                throw new Exception($"The Employee {employee.Id} was not found.");
            }
            employeeDb.BirthDate = employee.BirthDate;
            employeeDb.DocumentNumber = employee.DocumentNumber;
            employeeDb.Email = employee.Email;
            employeeDb.FirstName = employee.FirstName;
            employeeDb.LastName = employee.LastName;
            employeeDb.Password = employee.Password;

            await _repository.UpdateAsync(employeeDb);

            await _phonebookRepository.RemoveAllPhonesFromEmployeeAsync(employeeDb.Id);
            foreach (var phone in employee.Phones)
            {
                phone.EmployeeId = employeeDb.Id;
            }
            await _phonebookRepository.AddRangeAsync(employee.Phones);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
