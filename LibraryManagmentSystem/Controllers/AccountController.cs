using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
    }
}
