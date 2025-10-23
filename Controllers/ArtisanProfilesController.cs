using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArtisanMarketplace.Data;
using ArtisanMarketplace.Models;

namespace Artisan_Project.Controllers
{
    public class ArtisanProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtisanProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ArtisanProfiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.ArtisanProfiles.ToListAsync());
        }

        // GET: ArtisanProfiles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artisanProfile = await _context.ArtisanProfiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artisanProfile == null)
            {
                return NotFound();
            }

            return View(artisanProfile);
        }

        // GET: ArtisanProfiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ArtisanProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,BusinessName,Slug,Specialization,YearsOfExperience,ExperienceLevel,LicenseNumber,Certification,BusinessRegistration,TaxId,InsuranceDetails,AverageRating,TotalReviews,CompletedProjects,AvailabilityStatus,HourlyRate,ServiceRadius,About,ServicesOffered,IsVerified,VerifiedDate,VerificationDocuments,CreatedAt,UpdatedAt")] ArtisanProfile artisanProfile)
        {
            if (ModelState.IsValid)
            {
                artisanProfile.Id = Guid.NewGuid();
                _context.Add(artisanProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artisanProfile);
        }

        // GET: ArtisanProfiles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artisanProfile = await _context.ArtisanProfiles.FindAsync(id);
            if (artisanProfile == null)
            {
                return NotFound();
            }
            return View(artisanProfile);
        }

        // POST: ArtisanProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,BusinessName,Slug,Specialization,YearsOfExperience,ExperienceLevel,LicenseNumber,Certification,BusinessRegistration,TaxId,InsuranceDetails,AverageRating,TotalReviews,CompletedProjects,AvailabilityStatus,HourlyRate,ServiceRadius,About,ServicesOffered,IsVerified,VerifiedDate,VerificationDocuments,CreatedAt,UpdatedAt")] ArtisanProfile artisanProfile)
        {
            if (id != artisanProfile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artisanProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtisanProfileExists(artisanProfile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(artisanProfile);
        }

        // GET: ArtisanProfiles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artisanProfile = await _context.ArtisanProfiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artisanProfile == null)
            {
                return NotFound();
            }

            return View(artisanProfile);
        }

        // POST: ArtisanProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var artisanProfile = await _context.ArtisanProfiles.FindAsync(id);
            if (artisanProfile != null)
            {
                _context.ArtisanProfiles.Remove(artisanProfile);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtisanProfileExists(Guid id)
        {
            return _context.ArtisanProfiles.Any(e => e.Id == id);
        }
    }
}
