using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SDSDSoftwareApplication.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        [Display(Name = "Comments")]

        public string Name { get; set; }

        public Task Tasks { get; set; }
        public Guid TasksId { get; set; }
    }
}
