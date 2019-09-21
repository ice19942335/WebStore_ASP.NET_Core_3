using System.Collections.Generic;
using WebStore.Domain.Models;

namespace WebStore.Interfaces.Services
{
    public interface IEmployeesData
    {
        IEnumerable<EmployeeViewModel> GetAll();

        EmployeeViewModel GetById(int id);

        void AddNew(EmployeeViewModel employee);

        void SaveChanges();

        void Delete(int id);
    }
}
