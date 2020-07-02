using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SDSDSoftwareApplication.Models
{
    public class Tasks
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Task Name")]
        public string TaskName { get; set; }
        [Required]
        [Display(Name = "Task Description")]
        public string Description { get; set; }
        [Required]
        public string Priority { get; set; }
        [Required]
        public DateTimeOffset Duration { get; set; }
        public DateTimeOffset? CompletionTime { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public Guid CommentsId { get; set; }
        public Project Projects { get; set; }
        public Guid ProjectsId { get; set; }
        public ICollection<Resource> Resources { get; set; }
        public string ResourcesId { get; set; }
    }
}
