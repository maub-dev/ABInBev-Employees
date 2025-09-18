using ABInBev.Employees.Business.Interfaces;
using ABInBev.Employees.Business.Models;
using ABInBev.Employees.Business.Models.Validators;
using Microsoft.AspNetCore.Identity;

namespace ABInBev.Employees.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IPhonebookRepository _phonebookRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public EmployeeService(IEmployeeRepository repository, IPhonebookRepository phonebookRepository, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _phonebookRepository = phonebookRepository;
            _userManager = userManager;
        }

        public async Task AddAsync(Employee employee, string password)
        {
            foreach (var phone in employee.Phones)
            {
                phone.Employee = employee;
            }
            await ValidatorHelper.ValidateAsync(new EmployeeValidator(_repository), employee);

            var user = new IdentityUser
            {
                UserName = employee.Email,
                Email = employee.Email,
                EmailConfirmed = true
            };

            var identityResult = await _userManager.CreateAsync(user, password);
            if (!identityResult.Succeeded)
            {
                var errorMessage = string.Empty;
                identityResult.Errors.ToList().ForEach(x => errorMessage += $"{x.Code}: {x.Description}");
                throw new InvalidOperationException(errorMessage);
            }

            employee.UserIdentityId = user.Id;
            await _repository.AddAsync(employee);
        }

        public async Task UpdateAsync(Employee employee)
        {
            await ValidatorHelper.ValidateAsync(new EmployeeValidator(_repository), employee);

            var employeeDb = await _repository.GetByIdAsync(employee.Id);
            if (employeeDb is null)
                throw new Exception($"The Employee {employee.Id} was not found.");

            employeeDb.BirthDate = employee.BirthDate;
            employeeDb.DocumentNumber = employee.DocumentNumber;
            employeeDb.Email = employee.Email;
            employeeDb.FirstName = employee.FirstName;
            employeeDb.LastName = employee.LastName;

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
            var employee = await GetByIdAsync(id);
            await _repository.DeleteAsync(id);

            var user = await _userManager.FindByIdAsync(employee.UserIdentityId);
            if (user is not null)
                await _userManager.DeleteAsync(user);
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
