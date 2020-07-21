using HealthCatalystService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCatalystService.Repository
{
    public interface IPeopleRepository
    {
        Task<IEnumerable<People>> GetPeoplesAsAsync();
        Task<People> GetPeopleAsAsync(int id);
        Task<int> CreatePeopleAsAsync(People person);
        Task UpdatePeopleAsAsync(People person);
        Task DeletePeopleAsAsync(int id);
    }
}
