using SDSDSoftwareApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDSDSoftwareApplication.Services
{
    public interface IProject
    {
        IEnumerable<Project> GetAllProjects { get; }
        public Task<Project> AddProjects(Project project);
        public Project GetProject(Guid Id);
        //public Task<Project> DeleteProject(Guid Id);

        public Project EditProject(Project projects);
        IQueryable<Project> Search(string name);
    }
}
