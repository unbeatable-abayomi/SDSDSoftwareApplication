using SDSDSoftwareApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDSDSoftwareApplication.ViewModel
{
    public class TaskViewModel
    {
        public Tasks TaskBoard { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public List<Tasks> TaskBoards { get; set; }
        public Comment comment { get; set; }
    }
}
