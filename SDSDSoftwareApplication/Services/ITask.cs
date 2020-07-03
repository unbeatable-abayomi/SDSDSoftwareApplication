using SDSDSoftwareApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDSDSoftwareApplication.Services
{
    public interface ITask
    {
        IEnumerable<Tasks> Tasks { get; }

        public Tasks AddTask(Tasks tasks);

        public Task<Tasks> Delete(Guid ID);

        public Task<Tasks> GetTask(Guid ID);

        public Task<Tasks> SaveTask(Tasks tasks);

        IQueryable<Tasks> Search(string input);
    }
}
