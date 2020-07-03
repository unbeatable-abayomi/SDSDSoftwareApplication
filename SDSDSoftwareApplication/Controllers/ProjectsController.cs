using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using SDSDSoftwareApplication.Data;
using SDSDSoftwareApplication.DepartmentRepo;
using SDSDSoftwareApplication.Models;
using SDSDSoftwareApplication.Services;
using SDSDSoftwareApplication.ViewModel;

namespace SDSDSoftwareApplication.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProject _project;
        private readonly  ApplicationDbContext _db;
        private readonly IDepartment _department; 

        public ProjectsController(IProject context, ApplicationDbContext data, IDepartment department)
        {
            _project = context;
            _db = data;
            _department = department;
        }
       
        [HttpGet]
        public IActionResult Edit(Guid Id)
        {
            ViewBag.Departments = new SelectList(_department.Departments, "Id", "Name");
            var project = _project.GetProject(Id);
            return View(project);
        }

        [HttpPost]
        public IActionResult Edit(Project project)
        {
              _project.EditProject(project);
            return RedirectToAction(nameof(Create));
        }





        [HttpGet]   
        public IActionResult Create()
        {
           
            ViewBag.Departments = new SelectList(_department.Departments, "Id", "Name");
            var project = new ProjectViewModel()
            {
                AllProjects = _db.Projects.ToList()
            };
            return View(project);
        }

        [HttpPost]
        public IActionResult Create(ProjectViewModel project)
        {
            
         
                _db.Projects.Add(project.Projects);
                _db.SaveChanges();
           
          
            return RedirectToAction(nameof(Create));
        }
    }
}
