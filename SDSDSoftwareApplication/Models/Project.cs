using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace SDSDSoftwareApplication.Models
{
    public class Project
    {
    
        public Guid Id { get; set; }
        
        [Required]
        [Display(Name ="Project Name")]
        public string ProjectName { get; set; }
        [Required]
        [Display(Name = "Client's Name")]

        public string ClientName { get; set; }

        [Required]
        [Display(Name = "Project Description")]
        //[DataType(DataType.MultilineText)]
        public string Description { get; set; }

        

        [Required]
        [Display(Name = "Project Timeline")]

        public string Duration { get; set; }


        [Display(Name = "Project Comments")]
        //[DataType(DataType.MultilineText)]
        public string Comments { get; set; }

        
       

       
       
       
        
        public Department Departments { get; set; }
        [Display(Name = "Department")]
        public Guid DepartmentsId { get; set; }
       
        public ICollection<Resource> Resources { get; set; }
        
        public string ResourcesId { get; set; }

    }
}
