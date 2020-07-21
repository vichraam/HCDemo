using FluentAssertions;
using HealthCatalystService.Controllers;
using HealthCatalystService.Model;
using HealthCatalystService.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HealthCatalystServiceTest
{
    public class EmployeeControllerUnitTest : IClassFixture<TestFixture>
    {
        EmployeeController controller;
        IEmployeeRepository repo;
        public EmployeeControllerUnitTest()
        {
            var factory = new TestFactory();
            repo = factory.CreateEmployeeRepository();
            controller = factory.CreateEmployeeController();
        }

        [Fact]
        public void GetEmployees_TestOkResult()
        {
            var result = controller.Get();
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetEmployee_TestOkResult()
        {
            var employeeId = 2;
            var result = controller.Get(employeeId);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetEmployee_TestBadRequestResult()
        {
            var result = controller.Get(-1);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void DeleteEmployee_TestOkResult()
        {
            int employeeId = 3;
            var result = controller.Delete(employeeId);

            Assert.IsType<OkResult>(result.Result);
        }

        [Fact]
        public void DeleteEmployee_TestBadRequestResult()
        {
            var employeeId = -1;
            var result = controller.Delete(employeeId);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }


        [Fact]
        public void CreateEmployee_TestOkResult()
        {
            var employee = TestHelper.GetEmployee("FN6", "LN6", 60, "Test Address Line 1 - 6", "Test Address Line 2 - 6", "Test City6", "TS6", "67890", "Interest 1 - 6, Interest 2 - 6");
            var result = controller.Post(employee);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void CreateEmployee_TestBadResult()
        {
            var employee = TestHelper.GetEmployee("FN6", "LN6", 60, "Test Address Line 1 - 6", "Test Address Line 2 - 6", "Test City6", "TS6", "67890", "Interest 1 - 6, Interest 2 - 6");
            employee.FirstName = null;

            var result =  controller.Post(employee);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }


        [Fact]
        public async Task UpdateEmployee_TestOkResult()
        {
            var employees = await repo.GetEmployeesAsAsync();
            var employee = employees.ToList().FirstOrDefault();

            employee.FirstName = "Updated FirstName";
            var result = controller.Put(employee);
         
            Assert.IsType<OkResult>(result.Result);
        }

        [Fact]
        public async Task UpdateEmployee_TestBadResult()
        {
            var employees = await repo.GetEmployeesAsAsync();
            var employee = employees.ToList().FirstOrDefault();

            employee.Id = -1; 
            var result = controller.Put(employee);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
