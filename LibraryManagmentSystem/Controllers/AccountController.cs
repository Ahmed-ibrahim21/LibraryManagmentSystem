using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagmentSystem.Controllers
{
    public class AccountController : Controller
    {
        UserManager<User> UserManager;
        SignInManager<User> SignInManager;
        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager) 
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> SaveRegister(RegisterUserViewModel userViewModel)
        {
            if (ModelState.IsValid) 
            {
                User user = new User()
                {
                    UserName = userViewModel.UserName,
                    PasswordHash = userViewModel.Password
                };

                IdentityResult result = await UserManager.CreateAsync(user,userViewModel.Password);
                if (result.Succeeded) 
                {
                    await UserManager.AddToRoleAsync(user, "Member");
                    await SignInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Book");
                }
                foreach (var item in result.Errors) 
                {
                    ModelState.AddModelError(string.Empty,item.Description);
                }
            }
            return View("Register",userViewModel);
        }

        [HttpGet]
        public IActionResult LibrarianRegister()
        {
            return View("LibrarianRegister");
        }

        [HttpPost]
        public async Task<IActionResult> LibrarianSaveRegister(RegisterUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    UserName = userViewModel.UserName,
                    PasswordHash = userViewModel.Password
                };

                IdentityResult result = await UserManager.CreateAsync(user, userViewModel.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, "Librarian");
                    await SignInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Book");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return View("Register", userViewModel);
        }


        public IActionResult Login()
        {
            return View("Login");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]//requets.form['_requetss]
        public async Task<IActionResult> SaveLogin(LoginUserViewModel userViewModel)
        {
            if (ModelState.IsValid == true)
            {
                //check found 
                User appUser =
                    await UserManager.FindByNameAsync(userViewModel.Name);
                if (appUser != null)
                {
                    bool found =
                         await UserManager.CheckPasswordAsync(appUser, userViewModel.Password);
                    if (found == true)
                    {
                        await SignInManager.SignInAsync(appUser, userViewModel.RememberMe);
                        return RedirectToAction("Index", "Book");
                    }

                }
                ModelState.AddModelError("", "Username OR PAssword wrong");
                //create cookie
            }
            return View("Login", userViewModel);
        }



        public async Task<IActionResult> SignOut()
        {
            await SignInManager.SignOutAsync();
            return View("Login");
        }
    }
}
