using HealthCatalystService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCatalystService.Repository
{
    public class PeopleRepository : IPeopleRepository
    {
        ApplicationDbContext context;
        public PeopleRepository(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }
        public async Task<int> CreatePeopleAsAsync(People person)
        {
            try
            {
                var personToDb = new People();
                personToDb = person;
                personToDb.Id = 0;

                context.People.Add(personToDb);
                await context.SaveChangesAsync();

                return person.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeletePeopleAsAsync(int id)
        {
            try
            {
                var person = context.People.Find(id);
                if (person == null)
                    throw new Exception($"The person id {id} is unavailable.");

                context.People.Remove(person);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<People> GetPeopleAsAsync(int id)
        {
            try
            {
                var person = await context.People.FindAsync(id);
                if (person == null)
                    throw new Exception($"The person id {id} is unavailable.");

                return person;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<People>> GetPeoplesAsAsync()
        {
            try
            {
                return await context.People.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdatePeopleAsAsync(People person)
        {
            try
            {
                if (person == null)
                    throw new Exception("Invalid person");

                var personInDb = context.People.Find(person.Id);

                if (personInDb == null)
                    throw new Exception($"The person is unavailable.");

                personInDb.FirstName = person.FirstName;
                personInDb.LastName = person.LastName;
                personInDb.AddressLine1 = person.AddressLine1;
                personInDb.AddressLine2 = person.AddressLine2;
                personInDb.City = person.City;
                personInDb.State = person.State;
                personInDb.Zipcode = person.Zipcode;
                personInDb.Age = person.Age;
                personInDb.Interests = person.Interests;
                personInDb.PicturePath = person.PicturePath;

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
