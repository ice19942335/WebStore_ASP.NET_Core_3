using System.Collections.Generic;
using WebStore.Domain.Entities;
using WebStore.Domain.Models;

namespace WebStore.Interfaces.Services
{
    public interface IEmployeesData
    {
        IEnumerable<EmployeeModel> GetAll();

        EmployeeModel GetById(int id);

        void AddNew(EmployeeModel employee);

        EmployeeModel Update(int id, EmployeeModel employee);

        void Delete(int id);

        void SaveChanges();
    }
}
