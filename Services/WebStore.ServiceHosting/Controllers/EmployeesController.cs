using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesController(IEmployeesData employeesData) => _employeesData = employeesData;

        [HttpGet, ActionName("Get")]
        public IEnumerable<Employee> GetAll()
        {
            return _employeesData.GetAll();
        }

        [HttpGet("{id}"), ActionName("Get")]
        public Employee GetById(int id)
        {
            return _employeesData.GetById(id);
        }

        [HttpPost, ActionName("Post")]
        public void AddNew([FromBody] Employee employee)
        {
            _employeesData.AddNew(employee);
        }

        [HttpPut("{id}"), ActionName("Put")]
        public Employee Update(int id, [FromBody] Employee employee)
        {
           return _employeesData.Update(id, employee);
        }

        [HttpDelete("{id}")]
        public void Delete(int id/*, [FromServices] ILogger<EmployeesController> logger*/)
        {
            //using (logger.BeginScope($"Deleting employee with ID: {id}"))
            //{
            //    _employeesData.Delete(id);
            //    logger.LogInformation("Employee ID: {0} has been deleted", id);
            //}

            _employeesData.Delete(id);
        }

        [NonAction]
        public void SaveChanges()
        {
            _employeesData.SaveChanges();
        }
    }
}