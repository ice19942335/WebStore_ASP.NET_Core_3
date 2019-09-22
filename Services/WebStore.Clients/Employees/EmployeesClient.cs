using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Employees
{
    class EmployeesClient : BaseClient, IEmployeesData
    {
        public EmployeesClient(IConfiguration configuration, string serviceAddress) : base(configuration, serviceAddress) { }

        public IEnumerable<Employee> GetAll()
        {
            return Get<List<Employee>>(_serviceAddress);
        }

        public Employee GetById(int id)
        {
            return Get<Employee>($"{_serviceAddress}/{id}");
        }

        public void AddNew(Employee employee)
        {
            Post(_serviceAddress, employee);
        }

        public Employee Update(int id, Employee employee)
        {
            var response = Put($"{_serviceAddress}/{id}", employee);
            return response.Content.ReadAsAsync<Employee>().Result;
        }

        public void Delete(int id)
        {
            Delete($"{_serviceAddress}/{id}");
        }

        public void SaveChanges()
        {
            
        }
    }
}
