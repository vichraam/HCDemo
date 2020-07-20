using HealthCatalystService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCatalystService.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsAsync();
        //IEnumerable<Employee> GetEmployees();
        Task<Employee> GetEmployeeAsAsync(int employeeId);
        Task<int> CreateEmployeeAsAsync(Employee employee);
        Task UpdateEmployeeAsAsync(Employee employee);
        Task DeleteEmployeeAsAsync(int employeeId);
    }
}
