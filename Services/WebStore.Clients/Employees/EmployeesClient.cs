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
            return Get<List<EmployeeModel>>(_ServiceAddress);
        }

        public EmployeeModel GetById(int id)
        {
            return Get<EmployeeModel>($"{_ServiceAddress}/{id}");
        }

        public void AddNew(EmployeeModel employee)
        {
            Post(_ServiceAddress, employee);
        }

        public EmployeeModel Update(int id, EmployeeModel employee)
        {
            var response = Put($"{_ServiceAddress}/{id}", employee);
            return response.Content.ReadAsAsync<EmployeeModel>().Result;
        }

        public void Delete(int id)
        {
            Delete($"{_ServiceAddress}/{id}");
        }

        public void SaveChanges()
        {
            
        }
    }
}
