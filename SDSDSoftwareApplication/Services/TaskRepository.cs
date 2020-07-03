using Microsoft.EntityFrameworkCore;
using SDSDSoftwareApplication.Data;
using SDSDSoftwareApplication.Models;
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
            context.Entry(tasks).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return tasks;
        }

        public IQueryable<Tasks> Search(string input)
        {
            var task = context.Tasks.Where(C => C.Description.Contains(input));

            return task;
        }
    }
}
