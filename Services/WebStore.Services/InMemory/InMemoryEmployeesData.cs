using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.Services.InMemory
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private List<EmployeeModel> _employee = new List<EmployeeModel>
        {
            new EmployeeModel {Id = 1, FirstName = "Name 1", Patronymic = "Patronymic 1", Surname = "Surname 1", Age = 21},
            new EmployeeModel {Id = 2, FirstName = "Name 2", Patronymic = "Patronymic 2", Surname = "Surname 2", Age = 21},
            new EmployeeModel {Id = 3, FirstName = "Name 3", Patronymic = "Patronymic 3", Surname = "Surname 3", Age = 21},
            new EmployeeModel {Id = 4, FirstName = "Name 4", Patronymic = "Patronymic 4", Surname = "Surname 4", Age = 21}
        };

        public IEnumerable<EmployeeModel> GetAll() => _employee;

        public EmployeeModel GetById(int id) => _employee.FirstOrDefault(e => e.Id == id);

        public void AddNew(EmployeeModel employee)
        {
            if (employee is null) 
                throw new ArgumentNullException(nameof(employee));

            if (_employee.Contains(employee) || _employee.Any(e => e.Id == employee.Id)) 
                return;

            employee.Id = _employee.Count == 0 ? 1 : _employee.Max(e => e.Id) + 1;
            _employee.Add(employee);
        }

        public EmployeeModel Update(int id, EmployeeModel employee)
        {
            if (employee is null) 
                throw new ArgumentNullException(nameof(employee));

            var dbEmployee = GetById(id);

            if(dbEmployee is null)
                throw new InvalidOperationException($"Employee with ID: {id} not found");

            dbEmployee.FirstName = employee.FirstName;
            dbEmployee.Patronymic = employee.Patronymic;
            dbEmployee.Surname = employee.Surname;
            dbEmployee.Age = employee.Age;

            return dbEmployee;
        }

        public void Delete(int id)
        {
            var employee = GetById(id);
            if(employee is null) return;
            _employee.Remove(employee);
        }

        public void SaveChanges() { }
    }
}
