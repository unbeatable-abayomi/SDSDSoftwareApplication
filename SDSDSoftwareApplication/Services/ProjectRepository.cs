
using SDSDSoftwareApplication.Data;
using SDSDSoftwareApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDSDSoftwareApplication.Services
{
    public class ProjectRepository : IProject
    {
        private readonly ApplicationDbContext _db;

        public ProjectRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public IEnumerable<Project> GetAllProjects => _db.Projects.ToList();


        public async Task<Project> AddProjects(Project project)
        {
            _db.Projects.Add(project);
            await _db.SaveChangesAsync();
            return project;
        }
        //public async Task<Project> GetProject(Guid Id)
        //{
        //    Project project = _db.Projects.Find(Id);

        //    await _db.SaveChangesAsync();
        //    return project;
        //}
        public Project GetProject(Guid Id)
        {
            Project project = _db.Projects.Find(Id);

             _db.SaveChanges();
            return project;
        }
        //public async Task<Project> DeleteProject(Guid Id)
        //{
        //    Project project = await GetProject(Id);

        //    if (project != null)
        //    {
        //        _db.Projects.Remove(project);
        //        await _db.SaveChangesAsync();
        //    }


        //    return project;

        //}
        public Project EditProject(Project projects)
        {
            _db.Entry(projects).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
             _db.SaveChanges();
            return projects;
        }

        public IQueryable<Project> Search(string name)
        {
            var projects = _db.Projects.Where(c => c.ProjectName.Contains(name) || c.Description.Contains(name) || c.ClientName.Contains(name));

            return projects;
        }
    }
}
