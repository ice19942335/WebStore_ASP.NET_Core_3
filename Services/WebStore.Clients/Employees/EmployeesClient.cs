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
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        public EmployeesClient(IConfiguration configuration) : base(configuration, "api/employees") { }

        public IEnumerable<EmployeeModel> GetAll()
        {
            return Get<List<EmployeeModel>>(_serviceAddress);
        }

        public EmployeeModel GetById(int id)
        {
            return Get<EmployeeModel>($"{_serviceAddress}/{id}");
        }

        public void AddNew(EmployeeModel employee)
        {
            Post(_serviceAddress, employee);
        }

        public EmployeeModel Update(int id, EmployeeModel employee)
        {
            var response = Put($"{_serviceAddress}/{id}", employee);
            return response.Content.ReadAsAsync<EmployeeModel>().Result;
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
