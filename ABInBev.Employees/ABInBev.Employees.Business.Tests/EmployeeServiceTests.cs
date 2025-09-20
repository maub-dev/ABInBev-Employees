using ABInBev.Employees.Business.Interfaces;
using ABInBev.Employees.Business.Models;
using ABInBev.Employees.Business.Services;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace ABInBev.Employees.Business.Tests
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepoMock;
        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
        private readonly EmployeeService _service;
        private readonly Employee _validEmployee;

        public EmployeeServiceTests()
        {
            _employeeRepoMock = new Mock<IEmployeeRepository>();

            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            _userManagerMock = new Mock<UserManager<IdentityUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null
            );

            _service = new EmployeeService(_employeeRepoMock.Object, _userManagerMock.Object);

            _validEmployee = new Employee
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@test.com",
                DocumentNumber = "12345",
                BirthDate = new DateOnly(1950, 01, 01),
                Phone1 = "12 999999999",
                Phone2 = "12 345678901"
            };
        }

        [Fact]
        public async Task AddAsync_IdentityCreationSuccess_InsertEmployee()
        {
            // Arrange
            _userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _employeeRepoMock
                .Setup(x => x.AddAsync(It.IsAny<Employee>()))
                .Returns(Task.CompletedTask);

            // Act
            await _service.AddAsync(_validEmployee, "Password123!");

            // Assert
            _employeeRepoMock.Verify(x => x.AddAsync(It.Is<Employee>(e => e.Email == _validEmployee.Email)), Times.Once);
            _userManagerMock.Verify(x => x.CreateAsync(It.IsAny<IdentityUser>(), "Password123!"), Times.Once);
        }

        [Fact]
        public async Task AddAsync_IdentityCreationFails_ThrowsException()
        {
            // Arrange
            _userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Code = "Error", Description = "Invalid Password" }));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddAsync(_validEmployee, "pass"));
        }

        [Fact]
        public async Task AddAsync_InvalidEmail_ThrowsException()
        {
            // Arrange
            _validEmployee.Email = "invalid email";
            _userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _employeeRepoMock
                .Setup(x => x.AddAsync(It.IsAny<Employee>()))
                .Returns(Task.CompletedTask);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddAsync(_validEmployee, "Pa$$word1!"));
        }

        [Fact]
        public async Task AddAsync_DuplicatedDocumentNumber_ThrowsException()
        {
            // Arrange
            _userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _employeeRepoMock
                .Setup(x => x.AddAsync(It.IsAny<Employee>()))
                .Returns(Task.CompletedTask);

            _employeeRepoMock
                .Setup(x => x.IsDocumentNumberInUseAsync(It.IsAny<string>(), It.IsAny<Guid?>()))
                .ReturnsAsync(() => true);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddAsync(_validEmployee, "Pa$$word1!"));
        }

        [Fact]
        public async Task AddAsync_Phone1Empty_ThrowsException()
        {
            // Arrange
            _validEmployee.Phone1 = string.Empty;
            _userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _employeeRepoMock
                .Setup(x => x.AddAsync(It.IsAny<Employee>()))
                .Returns(Task.CompletedTask);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddAsync(_validEmployee, "Pa$$word1!"));
        }

        [Fact]
        public async Task AddAsync_Phone2Empty_ThrowsException()
        {
            // Arrange
            _validEmployee.Phone2 = string.Empty;
            _userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _employeeRepoMock
                .Setup(x => x.AddAsync(It.IsAny<Employee>()))
                .Returns(Task.CompletedTask);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddAsync(_validEmployee, "Pa$$word1!"));
        }

        [Fact]
        public async Task AddAsync_LessThanEighteenYearsOld_ThrowsException()
        {
            // Arrange
            _validEmployee.BirthDate = new DateOnly(DateTime.Now.Year - 10, 01, 01);
            _userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _employeeRepoMock
                .Setup(x => x.AddAsync(It.IsAny<Employee>()))
                .Returns(Task.CompletedTask);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddAsync(_validEmployee, "Pa$$word1!"));
        }

        [Fact]
        public async Task UpdateAsync_ValidEmployee_UpdateEmployeeAndPhonebook()
        {
            // Arrange
            _employeeRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(_validEmployee);
            _employeeRepoMock.Setup(x => x.UpdateAsync(It.IsAny<Employee>())).Returns(Task.CompletedTask);

            // Act
            await _service.UpdateAsync(_validEmployee);

            // Assert
            _employeeRepoMock.Verify(x => x.UpdateAsync(It.Is<Employee>(e => e.FirstName == _validEmployee.FirstName)), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_UnexistingEmployee_ThrowsException()
        {
            // Arrange
            Employee? employee = null;

            _employeeRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(employee);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.UpdateAsync(_validEmployee));
        }

        [Fact]
        public async Task DeleteAsync_Valid_DeleteEmployeeAndUserIdentity()
        {
            // Arrange
            var employee = new Employee { Id = Guid.NewGuid(), UserIdentityId = "123" };
            var identityUser = new IdentityUser { Id = employee.UserIdentityId };

            _employeeRepoMock.Setup(x => x.GetByIdAsync(employee.Id)).ReturnsAsync(employee);
            _employeeRepoMock.Setup(x => x.DeleteAsync(employee.Id)).Returns(Task.CompletedTask);

            _userManagerMock.Setup(x => x.FindByIdAsync(employee.UserIdentityId)).ReturnsAsync(identityUser);
            _userManagerMock.Setup(x => x.DeleteAsync(identityUser)).ReturnsAsync(IdentityResult.Success);

            // Act
            await _service.DeleteAsync(employee.Id);

            // Assert
            _employeeRepoMock.Verify(x => x.DeleteAsync(employee.Id), Times.Once);
            _userManagerMock.Verify(x => x.DeleteAsync(identityUser), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_Valid_CallsRepository()
        {
            // Arrange
            var employees = new List<Employee> { _validEmployee };
            _employeeRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(employees);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Single(result);
            Assert.Same(employees, result);
            _employeeRepoMock.Verify(x => x.GetAllAsync(), Times.Once);
        }
    }
}
