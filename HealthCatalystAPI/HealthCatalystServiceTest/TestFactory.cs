using HealthCatalystService.Controllers;
using HealthCatalystService.Model;
using HealthCatalystService.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Security.Policy;

namespace HealthCatalystServiceTest
{
    public class TestFactory
    {
        private const string connectionString = @"Data Source=Database/HealthCatalystTest.db;";
        private ServiceProvider serviceProvider;

        public TestFactory()
        {
            var services = new ServiceCollection();
            
            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlite(connectionString));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            serviceProvider = services.BuildServiceProvider();
        }

        public IEmployeeRepository CreateEmployeeRepository()
        {
            return serviceProvider.GetService<IEmployeeRepository>();
        }
        public EmployeeController CreateEmployeeController()
        {
            return new EmployeeController(CreateEmployeeRepository());
        }
        public ApplicationDbContext CreateApplicationDbContext()
        {
            return serviceProvider.GetService<ApplicationDbContext>();
        }
        
    }
}
