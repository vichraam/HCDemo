using HealthCatalystService.Controllers;
using HealthCatalystService.Model;
using HealthCatalystService.Repository;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HealthCatalystServiceTest
{
    public class EmployeeRepositoryUnitTest : IClassFixture<TestFixture>
    {
        IEmployeeRepository repo;
        public EmployeeRepositoryUnitTest()
        {
            var factory = new TestFactory();
            repo = factory.CreateEmployeeRepository();
        }

        [Fact]
        public async Task GetEmployees_TestEmployeeCount()
        {
            var result = await repo.GetEmployeesAsAsync();
            Assert.True(result.ToList().Count > 0);
        }

        [Fact]
        public async Task GetEmployee_TestAvailableEmployee()
        {
            int employeeId = 2;

            var result = await repo.GetEmployeeAsAsync(employeeId);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetEmployee_TestInvalidEmployee()
        {
            var employeeId = -1;
            var ex = await Assert.ThrowsAsync<Exception>(() =>  repo.GetEmployeeAsAsync(employeeId));

            Assert.Equal($"The employee id {employeeId} is unavailable.", ex.Message);
        }

        [Fact]
        public async Task DeleteEmployee_TestAvailableEmployee()
        {
            int employeeId = 3;
            await repo.DeleteEmployeeAsAsync(employeeId);
            
            Assert.True(true);
        }

        [Fact]
        public async Task DeleteEmployee_TestInvalidEmployee()
        {
            var employeeId = -1;
            var ex = await Assert.ThrowsAsync<Exception>(() => repo.DeleteEmployeeAsAsync(employeeId));

            Assert.Equal($"The employee id {employeeId} is unavailable.", ex.Message);
        }

        [Fact]
        public async Task CreateEmployee_TestValidEmployee()
        {
            var employee = TestHelper.GetEmployee("FN6", "LN6", 60, "Test Address Line 1 - 6", "Test Address Line 2 - 6", "Test City6", "TS6", "67890", "Interest 1 - 6, Interest 2 - 6");
            var employeeId = await repo.CreateEmployeeAsAsync(employee);
            employee.Id = employeeId;

            var result = await repo.GetEmployeeAsAsync(employeeId);
            Assert.True(employeeId > 0 );
            Assert.Equal<Employee>(employee, result);
        }

        [Fact]
        public async Task CreateEmployee_TestInvalidEmployee()
        {
            var employee = TestHelper.GetEmployee("FN6", "LN6", 60, "Test Address Line 1 - 6", "Test Address Line 2 - 6", "Test City6", "TS6", "67890", "Interest 1 - 6, Interest 2 - 6");
            employee.FirstName = null;
            
            var ex = await Assert.ThrowsAsync<Microsoft.EntityFrameworkCore.DbUpdateException>(() => repo.CreateEmployeeAsAsync(employee));
            Assert.IsType<Microsoft.EntityFrameworkCore.DbUpdateException>(ex);
        }


        [Fact]
        public async Task UpdateEmployee_TestValidEmployee()
        {
            var employees = await repo.GetEmployeesAsAsync();
            var employee = employees.ToList().FirstOrDefault();

            employee.FirstName = "Updated FirstName";
            await repo.UpdateEmployeeAsAsync(employee);

            var result = await repo.GetEmployeeAsAsync(employee.Id);
            Assert.Equal(employee.FirstName, result.FirstName);
        }

        [Fact]
        public async Task UpdateEmployee_TestNullEmployee()
        {
            Employee employee = null;
            var ex = await Assert.ThrowsAsync<Exception>(() => repo.UpdateEmployeeAsAsync(employee));
            Assert.IsType<Exception>(ex);
            Assert.Equal("Invalid Employee", ex.Message);
        }

        [Fact]
        public async Task UpdateEmployee_TestInvalidEmployee()
        {
            var employee = TestHelper.GetEmployee("FN6", "LN6", 60, "Test Address Line 1 - 6", "Test Address Line 2 - 6", "Test City6", "TS6", "67890", "Interest 1 - 6, Interest 2 - 6");
            employee.Id = -1;
            var ex = await Assert.ThrowsAsync<Exception>(() => repo.UpdateEmployeeAsAsync(employee));
            Assert.IsType<Exception>(ex);
            Assert.Equal("The employee is unavailable.", ex.Message);
        }

        [Fact]
        public async Task UpdateEmployee_TestException()
        {
            var employees = await repo.GetEmployeesAsAsync();

            //if (employees.ToList().Count == 0)
            //{
            //    var employeeId = await repo.CreateEmployeeAsAsync(GetTestEmployee());
            //    employees = await repo.GetEmployeesAsAsync();
            //}
            
            var employee = employees.ToList().FirstOrDefault();

            employee.FirstName = null;

            var ex = await Assert.ThrowsAsync<Microsoft.EntityFrameworkCore.DbUpdateException>(() => repo.UpdateEmployeeAsAsync(employee));
            Assert.IsType<Microsoft.EntityFrameworkCore.DbUpdateException>(ex);
        }
    }
}
