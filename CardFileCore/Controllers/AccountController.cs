using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardFileCore.Models;
using CardFileCore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CardFileCore.Controllers
{
    public class AccountController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            _roleManager = roleManager;
            //AddRole("admin");
            //AddRole("user");
        }

        //private async Task<IdentityResult> AddRole(string name)
        //{
        //    return await _roleManager.CreateAsync(new IdentityRole(name));
        //}

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email };
                
                var result = await _userManager.CreateAsync(user, model.Password);

                //var allRoles = _roleManager.Roles.ToList();
                await _userManager.AddToRoleAsync(user, "user");

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            var roles = new List<IdentityRole>();

            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl=null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if(result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(model.ReturnUrl)&&Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        var user = await _userManager.FindByEmailAsync(model.Email);
                        var userRoles = await _userManager.GetRolesAsync(user);
                        if (userRoles[0] == "admin")
                            return RedirectToAction("Index", "Admin");
                        else if (userRoles[0] == "user")
                            return RedirectToAction("Index", "User");
                        else
                            return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}