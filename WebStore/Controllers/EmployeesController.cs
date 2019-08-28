using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore._Infrastructure.Implementation;
using WebStore._Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesController(IEmployeesData employeesData)
        {
            _employeesData = employeesData;
        }

        public IActionResult Index()
        {
            return View(_employeesData.GetAll());
        }

        public IActionResult Details(int id)
        {
            var employee = _employeesData.GetById(id);

            if (employee is null) return NotFound();

            return View(employee);
        }

        public IActionResult Edit(int? id)
        {
            EmployeeViewModel employee;
            if (id != null)
            {
                employee = _employeesData.GetById((int)id);
                if (employee is null)
                    return NotFound();
            }
            else
            {
                employee = new EmployeeViewModel();
            }

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel employee)
        {
            if (!ModelState.IsValid) return View(employee);

            if (employee.Id > 0)
            {
                var employeeToEdit = _employeesData.GetById(employee.Id);

                if (employeeToEdit is null) 
                    return NotFound();

                employeeToEdit.FirstName = employee.FirstName;
                employeeToEdit.Patronymic = employee.Patronymic;
                employeeToEdit.Surname = employee.Surname;
                employeeToEdit.Age = employee.Age;
            }
            else
            {
                _employeesData.AddNew(employee);
            }

            _employeesData.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var employeeToDelete = _employeesData.GetById(id);

            if (employeeToDelete is null)
                return NotFound();

            _employeesData.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}