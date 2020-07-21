using HealthCatalystService.Controllers;
using HealthCatalystService.Model;
using HealthCatalystService.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthCatalystServiceTest
{
    public class TestFixture : IDisposable
    {
        private ApplicationDbContext dbContext;

        public TestFixture()
        {
            var factory = new TestFactory();
            dbContext = factory.CreateApplicationDbContext();

            TruncateDb();

            dbContext.People.Add(TestHelper.GetPeople("FN1", "LN1", 30, "Test Address Line 1 - 1", "Test Address Line 2 - 1", "Test City1", "TS1", "12345", "Interest 1 - 1, Interest 2 - 1"));
            dbContext.People.Add(TestHelper.GetPeople("FN2", "LN2", 35, "Test Address Line 1 - 2", "Test Address Line 2 - 2", "Test City2", "TS2", "23456", "Interest 1 - 2, Interest 2 - 2"));
            dbContext.People.Add(TestHelper.GetPeople("FN3", "LN3", 40, "Test Address Line 1 - 3", "Test Address Line 2 - 3", "Test City3", "TS3", "34567", "Interest 1 - 3, Interest 2 - 3"));
            dbContext.People.Add(TestHelper.GetPeople("FN4", "LN4", 45, "Test Address Line 1 - 4", "Test Address Line 2 - 4", "Test City4", "TS4", "45678", "Interest 1 - 4, Interest 2 - 4"));
            dbContext.People.Add(TestHelper.GetPeople("FN5", "LN5", 50, "Test Address Line 1 - 5", "Test Address Line 2 - 5", "Test City5", "TS5", "56789", "Interest 1 - 5, Interest 2 - 5"));

            dbContext.SaveChanges();
        }

        private async Task DeleteAllPeople()
        {
            var people = await dbContext.People.ToListAsync();
            dbContext.People.RemoveRange(people);
            dbContext.SaveChanges();
        }

        private void TruncateDb()
        {
            dbContext.Database.ExecuteSqlCommand("DELETE FROM [People]"); //This can be replaced with DeleteAllPeople method. Check Dispose method
            dbContext.Database.ExecuteSqlCommand("UPDATE SQLITE_SEQUENCE SET SEQ = 0 WHERE NAME = 'People'");
        }
        public void Dispose()
        {
            TruncateDb();
        }
    }
}
