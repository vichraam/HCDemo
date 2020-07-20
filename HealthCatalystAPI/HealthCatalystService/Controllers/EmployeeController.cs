using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using HealthCatalystService.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
namespace HealthCatalystService.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        ApplicationDbContext dbContext;
        public EmployeeController(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return dbContext.Employee.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var employee = dbContext.Employee.Find(id);
                if (employee == null)
                    return NotFound($"The employee id {id} is unavailable.");

                return Ok(employee);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Employee employee)
        {
            try
            {
                employee.PicturePath = employee.PicturePath;
                var employeeToDb = new Employee();
                employeeToDb = employee;

                dbContext.Employee.Add(employeeToDb);
                await dbContext.SaveChangesAsync();

                var message = new HttpResponseMessage(System.Net.HttpStatusCode.Created);
                message.Headers.Location = new Uri($"{Request.GetDisplayUrl()}/{employee.Id.ToString()}");

                return Ok(message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Employee employee)
        {
            try
            {
                if(employee == null)
                    return NotFound($"Invalid employee");

                var employeeInDb = dbContext.Employee.Find(employee.Id);
                
                if (employeeInDb == null)
                    return NotFound($"The employee id {employee.Id} is unavailable.");

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

                await dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var employee = dbContext.Employee.Find(id);
                if (employee == null)
                    return NotFound($"The employee id {id} is unavailable.");

                dbContext.Employee.Remove(employee);
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}