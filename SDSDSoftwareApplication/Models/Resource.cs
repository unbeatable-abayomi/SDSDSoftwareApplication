using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDSDSoftwareApplication.Models
{
    public class Resource : IdentityUser
    {

       [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public string UserPhoto { get; set;}


    }
}
