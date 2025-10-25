using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArtisanMarketplace.Data;
using ArtisanMarketplace.Models;

namespace Artisan_Project.Controllers
{
    [Authorize]
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ProfilesController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ==========================
        // GET: Profiles (List all artisans or users if admin)
        // ==========================
        public async Task<IActionResult> Index()
        {
            var profiles = await _context.ArtisanProfiles
                .Include(p => p.User)
                .ToListAsync();

            return View(profiles);
        }

        // ==========================
        // GET: Profiles/Details
        // Show the current user's profile (works for all users)
        // ==========================
        public async Task<IActionResult> Details()
        {
            var userIdStr = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userIdStr))
                return RedirectToAction("Login", "Account");

            if (!Guid.TryParse(userIdStr, out var userId))
                return BadRequest("Invalid user ID format.");

            var user = await _context.Users
                .Include(u => u.ArtisanProfile)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // ==========================
        // GET: Profiles/Edit
        // Edit current user's profile
        // ==========================
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userId = _userManager.GetUserId(User);
            if (!Guid.TryParse(userId, out var userGuid))
                return BadRequest("Invalid user ID format.");

            var user = await _context.Users
                .Include(u => u.ArtisanProfile)
                .FirstOrDefaultAsync(u => u.Id == userGuid);


            if (user == null)
                return NotFound();

            return View(user);
        }

        // ==========================
        // POST: Profiles/Edit
        // ==========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppUser model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.Users
                .Include(u => u.ArtisanProfile)
                .FirstOrDefaultAsync(u => u.Id == model.Id);

            if (user == null)
                return NotFound();

            // Update user info
            user.FullName = model.FullName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.City = model.City;
            user.State = model.State;
            user.Country = model.Country;
            user.Address = model.Address;
            user.PostalCode = model.PostalCode;
            user.Bio = model.Bio;

            // If artisan, update artisan profile too
            if (user.ArtisanProfile != null && model.ArtisanProfile != null)
            {
                user.ArtisanProfile.BusinessName = model.ArtisanProfile.BusinessName;
                user.ArtisanProfile.Specialization = model.ArtisanProfile.Specialization;
                user.ArtisanProfile.YearsOfExperience = model.ArtisanProfile.YearsOfExperience;
                user.ArtisanProfile.About = model.ArtisanProfile.About;
            }

            _context.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details));
        }

        // ==========================
        // DELETE PROFILE (optional)
        // ==========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var profile = await _context.ArtisanProfiles.FindAsync(id);
            if (profile != null)
            {
                _context.ArtisanProfiles.Remove(profile);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
