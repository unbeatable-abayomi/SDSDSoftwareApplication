using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SDSDSoftwareApplication.Data;
using SDSDSoftwareApplication.Services;
using SDSDSoftwareApplication.ViewModel;

namespace SDSDSoftwareApplication.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProject _context;
        private readonly  ApplicationDbContext _db;

        public ProjectsController(IProject context, ApplicationDbContext data)
        {
            _context = context;
            _db = data;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]   
        public IActionResult Create()
        {
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
