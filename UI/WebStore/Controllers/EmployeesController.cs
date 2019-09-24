using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    [Authorize]
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

        [Authorize(Roles = Domain.Entities.Identity.User.RoleAdmin)]
        public IActionResult Edit(int? id)
        {
            EmployeeModel employee;
            if (id != null)
            {
                employee = _employeesData.GetById((int)id);
                if (employee is null)
                    return NotFound();
            }
            else
            {
                employee = new EmployeeModel();
            }

            return View(employee);
        }

        [HttpPost]
        [Authorize(Roles = Domain.Entities.Identity.User.RoleAdmin)]
        public IActionResult Edit(EmployeeModel employee)
        {
            if (!ModelState.IsValid) return View(employee);

            if (employee.Id > 0)
            {
                var employeeToEdit = _employeesData.GetById(employee.Id);

                if (employeeToEdit is null) 
                    return NotFound();

                _employeesData.Update(employee.Id, employee);
            }
            else
            {
                _employeesData.AddNew(employee);
            }

            _employeesData.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = Domain.Entities.Identity.User.RoleAdmin)]
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