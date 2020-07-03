using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SDSDSoftwareApplication.DepartmentRepo;

namespace SDSDSoftwareApplication.Models
{
    public class DepartmentsController : Controller
    {
		private readonly IDepartment _department;
		private readonly IWebHostEnvironment WebHost;
		public DepartmentsController(IDepartment department, IWebHostEnvironment _webhost)
		{
			_department = department;
			WebHost = _webhost;
		}
		public IActionResult Index()
        {
            return View();
        }


		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Department department)
		{

			if (ModelState.IsValid)
			{
				_department.Add(department);
				return View("Thank", department);
			}
			else
			{
				return View();
			}
		}

		public IActionResult Details(Guid DepartmentId)
		{
			var person = _department.GetDepartment(DepartmentId);
			return View(person);
		}

		[HttpGet]
		public IActionResult Edit(Guid DepartmentId)
		{
			var person = _department.GetDepartment(DepartmentId);

			return View(person);
		}

		[HttpPost]
		public IActionResult Edit(Department department)
		{
			_department.EditDepartment(department);
			return View();
		}

		public IActionResult DeleteConfirm(Guid DepartmentID)
		{
			Department person = _department.GetDepartment(DepartmentID);
			if (person == null)
			{
				return RedirectToAction("List");
			}
			return View(person);
		}

		[HttpPost]
		public IActionResult Delete(Guid Id)
		{
			var person = _department.Delete(Id);
			return View("Deleted", person);
		}

		public IActionResult List()
		{
			return View(_department.Departments);
		}
	}
}
