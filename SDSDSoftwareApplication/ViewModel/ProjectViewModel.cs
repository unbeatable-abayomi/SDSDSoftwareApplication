//using Microsoft.CodeAnalysis;
using SDSDSoftwareApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDSDSoftwareApplication.ViewModel
{
    public class ProjectViewModel
    {
        public Project Projects { get; set; }
        public List<Project> AllProjects { get; set; }
    }
}
