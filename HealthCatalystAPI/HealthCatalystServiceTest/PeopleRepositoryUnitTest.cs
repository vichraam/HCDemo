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
    public class PeopleRepositoryUnitTest : IClassFixture<TestFixture>
    {
        IPeopleRepository repo;
        public PeopleRepositoryUnitTest()
        {
            var factory = new TestFactory();
            repo = factory.CreatePeopleRepository();
        }

        [Fact]
        public async Task GetPeoples_TestCount()
        {
            var result = await repo.GetPeoplesAsAsync();
            Assert.True(result.ToList().Count > 0);
        }

        [Fact]
        public async Task GetPeople_TestValidPerson()
        {
            int id = 2;

            var result = await repo.GetPeopleAsAsync(id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetPeople_TestInvalidPerson()
        {
            var id = -1;
            var ex = await Assert.ThrowsAsync<Exception>(() =>  repo.GetPeopleAsAsync(id));

            Assert.Equal($"The person id {id} is unavailable.", ex.Message);
        }

        [Fact]
        public async Task DeletePeople_TestAvailablePerson()
        {
            int id = 3;
            await repo.DeletePeopleAsAsync(id);
            
            Assert.True(true);
        }

        [Fact]
        public async Task DeletePeople_TestInvalidPerson()
        {
            var id = -1;
            var ex = await Assert.ThrowsAsync<Exception>(() => repo.DeletePeopleAsAsync(id));

            Assert.Equal($"The person id {id} is unavailable.", ex.Message);
        }

        [Fact]
        public async Task CreatePeople_TestValidPerson()
        {
            var person = TestHelper.GetPeople("FN6", "LN6", 60, "Test Address Line 1 - 6", "Test Address Line 2 - 6", "Test City6", "TS6", "67890", "Interest 1 - 6, Interest 2 - 6");
            var id = await repo.CreatePeopleAsAsync(person);
            person.Id = id;

            var result = await repo.GetPeopleAsAsync(id);
            Assert.True(id > 0 );
            Assert.Equal<People>(person, result);
        }

        [Fact]
        public async Task CreatePeople_TestInvalidPerson()
        {
            var person = TestHelper.GetPeople("FN6", "LN6", 60, "Test Address Line 1 - 6", "Test Address Line 2 - 6", "Test City6", "TS6", "67890", "Interest 1 - 6, Interest 2 - 6");
            person.FirstName = null;
            
            var ex = await Assert.ThrowsAsync<Microsoft.EntityFrameworkCore.DbUpdateException>(() => repo.CreatePeopleAsAsync(person));
            Assert.IsType<Microsoft.EntityFrameworkCore.DbUpdateException>(ex);
        }

        [Fact]
        public async Task UpdatePeople_TestValidPerson()
        {
            var people = await repo.GetPeoplesAsAsync();
            var person = people.ToList().FirstOrDefault();

            person.FirstName = "Updated FirstName";
            await repo.UpdatePeopleAsAsync(person);

            var result = await repo.GetPeopleAsAsync(person.Id);
            Assert.Equal(person.FirstName, result.FirstName);
        }

        [Fact]
        public async Task UpdatePeople_TestNullPerson()
        {
            People person = null;
            var ex = await Assert.ThrowsAsync<Exception>(() => repo.UpdatePeopleAsAsync(person));
            Assert.IsType<Exception>(ex);
            Assert.Equal("Invalid person", ex.Message);
        }

        [Fact]
        public async Task UpdatePeople_TestInvalidPerson()
        {
            var person = TestHelper.GetPeople("FN6", "LN6", 60, "Test Address Line 1 - 6", "Test Address Line 2 - 6", "Test City6", "TS6", "67890", "Interest 1 - 6, Interest 2 - 6");
            person.Id = -1;
            var ex = await Assert.ThrowsAsync<Exception>(() => repo.UpdatePeopleAsAsync(person));
            Assert.IsType<Exception>(ex);
            Assert.Equal("The person is unavailable.", ex.Message);
        }

        [Fact]
        public async Task UpdatePeople_TestException()
        {
            var people = await repo.GetPeoplesAsAsync();
            var person = people.ToList().FirstOrDefault();

            person.FirstName = null;

            var ex = await Assert.ThrowsAsync<Microsoft.EntityFrameworkCore.DbUpdateException>(() => repo.UpdatePeopleAsAsync(person));
            Assert.IsType<Microsoft.EntityFrameworkCore.DbUpdateException>(ex);
        }
    }
}
