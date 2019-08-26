using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore._Infrastructure.Interfaces
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
