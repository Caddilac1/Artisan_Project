using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Artisan_Project.Models;
using Artisan_Project.ViewModels;
using ArtisanMarketplace.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Artisan_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // ==========================
        // REGISTER (USER + ARTISAN)
        // ==========================
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new AppUser
            {
                FullName = model.FullName,
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber,
                City = model.City,
                State = model.State,
                Country = model.Country,
                PostalCode = model.PostalCode,
                Bio = model.Bio,
                Address = model.Address
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (model.IsArtisan)
                {
                    user.ArtisanProfile = new ArtisanProfile
                    {
                        ArtisanSpeciality = model.ArtisanSpeciality,
                        ProfessionalBio = model.Bio,
                        BusinessAddress = model.BusinessAddress
                    };
                }

                await _userManager.UpdateAsync(user);
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        // ==========================
        // LOGIN
        // ==========================
        [HttpGet]
        public IActionResult Login()
        {
            // Just return the login page
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AppUser? user = null;

            if (model.EmailOrPhone.Contains("@"))
                user = await _userManager.FindByEmailAsync(model.EmailOrPhone);
            else
                user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.EmailOrPhone);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    user.UserName, 
                    model.Password, 
                    model.RememberMe, 
                    false
                );

                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        // ==========================
        // LOGOUT
        // ==========================
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
