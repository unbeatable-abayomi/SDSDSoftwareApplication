using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SDSDSoftwareApplication.Data;
using SDSDSoftwareApplication.Models;
using SDSDSoftwareApplication.Services;
using SDSDSoftwareApplication.ViewModel;

namespace SDSDSoftwareApplication.Controllers
{
    public class TasksController : Controller
    {
        private ApplicationDbContext db;
        private readonly ITask _tasks;

        private readonly IComment _comments;

        public TasksController(ITask task, IComment comment, ApplicationDbContext data)
        {
            _tasks = task;
            _comments = comment;
            db = data;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View(_tasks.Tasks);
        }




        [HttpGet]
        public IActionResult Search()
        {

            return View();
        }


        [HttpPost]
        public IActionResult Search(string input)
        {
            var task = _tasks.Search(input);
            return View("SearchOutput", task);
        }



        [HttpGet]
        public IActionResult AddTask()
        {
            var task = new TaskViewModel()
            {
                TaskBoards = db.Tasks.ToList()
            };
            return View(task);

        }

        //[HttpPost]
        //public IActionResult AddTask(TaskViewModel task)
        //{

        //    db.Tasks.Add(task.TaskBoard);
        //    db.SaveChanges();
        //    return RedirectToAction(nameof(AddTask));

        //}


        [HttpPost]
        public async Task<IActionResult> AddTask(TaskViewModel task)
        {
            if (ModelState.IsValid)
            {

                var dataInDb = db.Tasks.FirstOrDefault(a => a.Id == task.TaskBoard.Id);
                dataInDb.TaskName = task.TaskBoard.TaskName;
                dataInDb.Description = task.TaskBoard.Description;
                dataInDb.Priority = task.TaskBoard.Priority;
                dataInDb.StartDate = task.TaskBoard.StartDate;
                dataInDb.EndDate = task.TaskBoard.EndDate;
                task.EndDate = dataInDb.EndDate;
                task.StartDate = dataInDb.StartDate;
                dataInDb.Duration = (task.EndDate - task.StartDate).Hours;
                dataInDb.CompletionTime = task.TaskBoard.CompletionTime;
                dataInDb.Status = task.TaskBoard.Status;
                dataInDb.Projects = task.TaskBoard.Projects;
                dataInDb.Resources = task.TaskBoard.Resources;
               await db.SaveChangesAsync();

                Comment comments = new Comment
                {
                    Name = task.comment.Name,
                    TasksId = task.TaskBoard.Id
                };
                _comments.AddComment(comments);

            }


            return RedirectToAction("AddTask");

        }
    }
}
