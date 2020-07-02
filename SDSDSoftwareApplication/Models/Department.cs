using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SDSDSoftwareApplication.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name ="Department Name")]
        public string Name { get; set; }
        [Required]
        
        public string Description { get; set; }
    }
}
