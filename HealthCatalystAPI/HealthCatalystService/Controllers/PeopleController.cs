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
using HealthCatalystService.Repository;
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
    public class PeopleController : ControllerBase
    {
        IPeopleRepository repo;
        public PeopleController(IPeopleRepository empRepo)
        {
            repo = empRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var people = await repo.GetPeoplesAsAsync();
                return Ok(people);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var person = await repo.GetPeopleAsAsync(id);

                return Ok(person);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(People person)
        {
            try
            {
                var id = await repo.CreatePeopleAsAsync(person); 
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.Created);

                return Ok(message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(People person)
        {
            try
            {
                await repo.UpdatePeopleAsAsync(person);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await repo.DeletePeopleAsAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}