using System.Collections.Generic;
using WebStore.Domain.Entities;
using WebStore.Domain.Models;
using Employee = WebStore.Domain.Models.Employee;

namespace WebStore.Interfaces.Services
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> GetAll();

        Employee GetById(int id);

        void AddNew(Employee employee);

        Employee Update(int id, Employee employee);

        void Delete(int id);

        void SaveChanges();
    }
}
