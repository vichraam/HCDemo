using HealthCatalystService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCatalystService.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        ApplicationDbContext employeeDbContext;
        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            employeeDbContext = dbContext;
        }
        public async Task<int> CreateEmployeeAsAsync(Employee employee)
        {
            try
            {
                var employeeToDb = new Employee();
                employeeToDb = employee;
                employeeToDb.Id = 0;

                employeeDbContext.Employee.Add(employeeToDb);
                await employeeDbContext.SaveChangesAsync();

                return employee.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteEmployeeAsAsync(int employeeId)
        {
            try
            {
                var employee = employeeDbContext.Employee.Find(employeeId);
                if (employee == null)
                    throw new Exception($"The employee id {employeeId} is unavailable.");

                employeeDbContext.Employee.Remove(employee);
                await employeeDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> GetEmployeeAsAsync(int employeeId)
        {
            try
            {
                var employee = await employeeDbContext.Employee.FindAsync(employeeId);
                if (employee == null)
                    throw new Exception($"The employee id {employeeId} is unavailable.");

                return employee;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<Employee>> GetEmployeesAsAsync()
        {
            try
            {
                return await employeeDbContext.Employee.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateEmployeeAsAsync(Employee employee)
        {
            try
            {
                if (employee == null)
                    throw new Exception("Invalid Employee");

                var employeeInDb = employeeDbContext.Employee.Find(employee.Id);

                if (employeeInDb == null)
                    throw new Exception($"The employee is unavailable.");

                employeeInDb.FirstName = employee.FirstName;
                employeeInDb.LastName = employee.LastName;
                employeeInDb.AddressLine1 = employee.AddressLine1;
                employeeInDb.AddressLine2 = employee.AddressLine2;
                employeeInDb.City = employee.City;
                employeeInDb.State = employee.State;
                employeeInDb.Zipcode = employee.Zipcode;
                employeeInDb.Age = employee.Age;
                employeeInDb.Interests = employee.Interests;
                employeeInDb.PicturePath = employee.PicturePath;

                await employeeDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
