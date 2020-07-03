using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SDSDSoftwareApplication.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter a valid email address")]
        public string Email{ get; set; }
        [Required(ErrorMessage = "Please enter your password"), DataType(DataType.Password)]  
        public string Password { get; set; }
    }
}
