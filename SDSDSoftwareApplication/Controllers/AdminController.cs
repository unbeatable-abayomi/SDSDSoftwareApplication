using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SDSDSoftwareApplication.Models;
using SDSDSoftwareApplication.ViewModel;

namespace SDSDSoftwareApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly SignInManager<Resource> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Resource> userManager;
        private readonly IWebHostEnvironment hostEnvironment;
        public AdminController(RoleManager<IdentityRole> role, UserManager<Resource> UserManager, SignInManager<Resource> SignInManager, IWebHostEnvironment hostEnvironment)
        {
            _roleManager = role;
            this.userManager = UserManager;
            _signInManager = SignInManager;
            this.hostEnvironment = hostEnvironment;
        }
        public IActionResult RegisterModel(RegisterModel registerModel)
        {
            return View(registerModel);
        }

        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }



        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        private string ProcessUploadedFile(ApplicationUserViewModel model)
        {
            string uniqueFileName = null;
            if (model.PhotoPath != null)
            {
                string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PhotoPath.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.PhotoPath.CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser( ApplicationUserViewModel applicationUser)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(applicationUser);
                var user = new Resource()
                {
                    Name = applicationUser.Name,
                    Email = applicationUser.Email,
                     UserPhoto = uniqueFileName,
                    UserName =applicationUser.Email,
                    
                };
                string password = GenerateRandomPassword();
                IdentityResult checkUser = await userManager.CreateAsync(user, password);
                if (checkUser.Succeeded)
                    return RedirectToAction("Index");
            }

            return View(applicationUser);
        }


        [HttpGet]

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]

        public async Task<IActionResult> Login (LoginViewModel login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

            return RedirectToAction("Dashboard", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Admin");
        }


        [HttpGet]
        public IActionResult ListUsers()
        {
            var Users = userManager.Users;
            return View(Users);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateRole()
        {

            return View();
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(ApplicationRoles model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View();
        }
        //[Authorize(Roles = "Administrator")]
        public IActionResult ListRole()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }



        [HttpGet]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user == null)
            {
                ViewBag.ErroMessage = $"User cannot be found";
                return View("NotFound");
            }
            var model = new Resource
            {
                Id = user.Id,
                Email = user.Email

            };


            return View(model);
        }
        [HttpPost, ActionName("DeleteUser")]
        public async Task<IActionResult> ConfirmDelete(Resource model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                user.Email = model.Email;

                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("listUsers");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListUsers", model);

            }

            else
            {

                ViewBag.ErrorMessage = $" Operation Failed";
                return View("NotFound");

            }


        }




        [HttpGet]
        public async Task<IActionResult> EditRole(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                ViewBag.ErroMessage = $"Role with Id = {Id} cannot be found";
                return View("NotFound");
            }
            var model = new EditApplicationRole
            {
                Id = role.Id,
                RoleName = role.Name

            };
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                };
            }

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> EditRole(EditApplicationRole model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErroMessage = $"Role with Id = {model.RoleName} cannot be found";
                return View("NotFound");
            }

            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(" ", error.Description);
                }

                return View("ListRole", model);
            }


        }

        [HttpGet]
        public async Task<IActionResult> EditUserInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id ={roleId} cannot be found";
                return View("NotFound");
            }
            var model = new List<EditUsersInApplicationRole>();
            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new EditUsersInApplicationRole()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserInRole(List<EditUsersInApplicationRole> model,
            string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role  cannot be found";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }

                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRole(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                ViewBag.ErroMessage = $"Role  cannot be found";
                return View("NotFound");
            }
            var model = new EditApplicationRole
            {
                Id = role.Id,
                RoleName = role.Name

            };



            return View(model);
        }


        [HttpPost, ActionName("DeleteRole")]
        public async Task<IActionResult> ConfirmDelete(EditApplicationRole model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role != null)
            {
                role.Name = model.RoleName;

                var result = await _roleManager.DeleteAsync(role);


                if (result.Succeeded)
                {
                    return RedirectToAction("listRole");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListRoles", model);

            }

            else
            {

                ViewBag.ErrorMessage = $"Role  cannot be found";
                return View("NotFound");

            }



        }

        [HttpGet]
        public IActionResult DeletedPage()
        {
            return View();
        }
    }
}
