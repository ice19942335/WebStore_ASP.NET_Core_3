﻿using System;
using System.Collections.Generic;
using System.Linq;
using WebStore._Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore._Infrastructure.Implementation.InMemory
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        public List<EmployeeViewModel> _employee = new List<EmployeeViewModel>
        {
            new EmployeeViewModel {Id = 1, FirstName = "Name 1", Patronymic = "Patronymic 1", Surname = "Surname 1", Age = 21},
            new EmployeeViewModel {Id = 2, FirstName = "Name 2", Patronymic = "Patronymic 2", Surname = "Surname 2", Age = 21},
            new EmployeeViewModel {Id = 3, FirstName = "Name 3", Patronymic = "Patronymic 3", Surname = "Surname 3", Age = 21},
            new EmployeeViewModel {Id = 4, FirstName = "Name 4", Patronymic = "Patronymic 4", Surname = "Surname 4", Age = 21}
        };

        public IEnumerable<EmployeeViewModel> GetAll() => _employee;

        public EmployeeViewModel GetById(int id) => _employee.FirstOrDefault(e => e.Id == id);

        public void AddNew(EmployeeViewModel employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (_employee.Contains(employee) || _employee.Any(e => e.Id == employee.Id)) return;

            employee.Id = _employee.Count == 0 ? 1 : _employee.Max(e => e.Id) + 1;
            _employee.Add(employee);
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