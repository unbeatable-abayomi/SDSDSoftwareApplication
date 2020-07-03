using Microsoft.EntityFrameworkCore;
using SDSDSoftwareApplication.Data;
using SDSDSoftwareApplication.Models;
using SDSDSoftwareApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDSDSoftwareApplication.Services
{
    public class TaskRepository : ITask
    {
        private readonly ApplicationDbContext context;

        public TaskRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Tasks> Tasks => context.Tasks;

        public Tasks AddTask(Tasks tasks)
        {
            context.Tasks.Add(tasks);
            context.SaveChanges();
            return tasks;
        }

        public async Task<Tasks> Delete(Guid ID)
        {
            Tasks task = context.Tasks.Find(ID);
            if (task != null)
            {
                context.Tasks.Remove(task);
                //After remove the employee then save changes to database
                await context.SaveChangesAsync();
            }
            return task;
        }

        public async Task<Tasks> GetTask(Guid ID)
        {
            return await context.Tasks.FindAsync(ID);
        }

        public async Task<Tasks> SaveTask(Tasks tasks)
        {
            context.Tasks.Update(tasks);
            await context.SaveChangesAsync();
            return tasks;
        }

        public IQueryable<Tasks> Search(string input)
        {
            var task = context.Tasks.Where(C => C.Description.Contains(input));

            return task;
        }

        //public TaskViewModel AddTasks(TaskViewModel task)
        //{
        //    if (task.TaskBoard.Id == null)
        //    {
        //        context.Tasks.Add(task.TaskBoard);
        //        context.SaveChanges();
        //    }
        //    else
        //    {

        //        var dataInDb = context.Tasks.FirstOrDefault(a => a.Id == task.TaskBoard.Id);
        //        dataInDb.TaskName = task.TaskBoard.TaskName;
        //        dataInDb.Description = task.TaskBoard.Description;
        //        dataInDb.Priority = task.TaskBoard.Priority;
        //        //dataInDb.StartDate = task.TaskBoard.StartDate;
        //        //dataInDb.EndDate = task.TaskBoard.EndDate;
        //        dataInDb.Duration = (task.EndDate - task.StartDate).Hours;
        //        dataInDb.CompletionTime = task.TaskBoard.CompletionTime;
                
        //        dataInDb.Projects = task.TaskBoard.Projects;
        //        dataInDb.Resources = task.TaskBoard.Resources;
        //        context.SaveChanges();
        //    }
        //    return task;
        //}

    }
}
