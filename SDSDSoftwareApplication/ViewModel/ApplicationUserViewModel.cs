using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SDSDSoftwareApplication.ViewModel
{
    public class ApplicationUserViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //[Required, DataType(DataType.Password)]
        //public string Password { get; set; }
        [Required, DataType(DataType.Upload), Display(Name ="Image")]
        public IFormFile PhotoPath { get; set; }


    }
}
