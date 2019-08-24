using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        private static List<EmployeeViewModel> _employee = new List<EmployeeViewModel>
        {
            new EmployeeViewModel {Id = 1, FirstName = "Name 1", Patronymic = "Patronymic 1", SureName = "SureName 1", Age = 21},
            new EmployeeViewModel {Id = 2, FirstName = "Name 2", Patronymic = "Patronymic 2", SureName = "SureName 2", Age = 21},
            new EmployeeViewModel {Id = 3, FirstName = "Name 3", Patronymic = "Patronymic 3", SureName = "SureName 3", Age = 21},
            new EmployeeViewModel {Id = 4, FirstName = "Name 4", Patronymic = "Patronymic 4", SureName = "SureName 4", Age = 21}
        };

        public IActionResult Index()
        {
            return View(_employee);
        }

        public IActionResult Details(int id)
        {
            return View(_employee.FirstOrDefault(e => e.Id == id));
        }
    }
}