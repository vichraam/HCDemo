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
    public class PeopleControllerUnitTest : IClassFixture<TestFixture>
    {
        PeopleController controller;
        IPeopleRepository repo;
        public PeopleControllerUnitTest()
        {
            var factory = new TestFactory();
            repo = factory.CreatePeopleRepository();
            controller = factory.CreatePeopleController();
        }

        [Fact]
        public void GetPeoples_TestOkResult()
        {
            var result = controller.Get();
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetPeople_TestOkResult()
        {
            var id = 2;
            var result = controller.Get(id);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetPeople_TestBadRequestResult()
        {
            var result = controller.Get(-1);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void DeletePeople_TestOkResult()
        {
            int id = 3;
            var result = controller.Delete(id);

            Assert.IsType<OkResult>(result.Result);
        }

        [Fact]
        public void DeletePeople_TestBadRequestResult()
        {
            var id = -1;
            var result = controller.Delete(id);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }


        [Fact]
        public void CreatePeople_TestOkResult()
        {
            var person = TestHelper.GetPeople("FN6", "LN6", 60, "Test Address Line 1 - 6", "Test Address Line 2 - 6", "Test City6", "TS6", "67890", "Interest 1 - 6, Interest 2 - 6");
            var result = controller.Post(person);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void CreatePeople_TestBadResult()
        {
            var person = TestHelper.GetPeople("FN6", "LN6", 60, "Test Address Line 1 - 6", "Test Address Line 2 - 6", "Test City6", "TS6", "67890", "Interest 1 - 6, Interest 2 - 6");
            person.FirstName = null;

            var result =  controller.Post(person);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }


        [Fact]
        public async Task UpdatePeople_TestOkResult()
        {
            var people = await repo.GetPeoplesAsAsync();
            var person = people.ToList().FirstOrDefault();

            person.FirstName = "Updated FirstName";
            var result = controller.Put(person);
         
            Assert.IsType<OkResult>(result.Result);
        }

        [Fact]
        public async Task UpdatePeople_TestBadResult()
        {
            var people = await repo.GetPeoplesAsAsync();
            var person = people.ToList().FirstOrDefault();

            person.Id = -1; 
            var result = controller.Put(person);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
