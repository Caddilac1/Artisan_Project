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
    public class ArtisanWorksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtisanWorksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ArtisanWorks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ArtisanWorks.Include(a => a.Artisan);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ArtisanWorks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artisanWork = await _context.ArtisanWorks
                .Include(a => a.Artisan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artisanWork == null)
            {
                return NotFound();
            }

            return View(artisanWork);
        }

        // GET: ArtisanWorks/Create
        public IActionResult Create()
        {
            ViewData["ArtisanId"] = new SelectList(_context.ArtisanProfiles, "Id", "AvailabilityStatus");
            return View();
        }

        // POST: ArtisanWorks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArtisanId,Title,Slug,Description,ProjectType,ProjectStatus,DurationDays,ProjectCost,Location,FeaturedImage,ClientName,ClientTestimonial,ClientRating,ViewsCount,LikesCount,CompletionDate,CreatedAt,UpdatedAt,IsFeatured,IsPublic")] ArtisanWork artisanWork)
        {
            if (ModelState.IsValid)
            {
                artisanWork.Id = Guid.NewGuid();
                _context.Add(artisanWork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtisanId"] = new SelectList(_context.ArtisanProfiles, "Id", "AvailabilityStatus", artisanWork.ArtisanId);
            return View(artisanWork);
        }

        // GET: ArtisanWorks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artisanWork = await _context.ArtisanWorks.FindAsync(id);
            if (artisanWork == null)
            {
                return NotFound();
            }
            ViewData["ArtisanId"] = new SelectList(_context.ArtisanProfiles, "Id", "AvailabilityStatus", artisanWork.ArtisanId);
            return View(artisanWork);
        }

        // POST: ArtisanWorks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ArtisanId,Title,Slug,Description,ProjectType,ProjectStatus,DurationDays,ProjectCost,Location,FeaturedImage,ClientName,ClientTestimonial,ClientRating,ViewsCount,LikesCount,CompletionDate,CreatedAt,UpdatedAt,IsFeatured,IsPublic")] ArtisanWork artisanWork)
        {
            if (id != artisanWork.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artisanWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtisanWorkExists(artisanWork.Id))
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
            ViewData["ArtisanId"] = new SelectList(_context.ArtisanProfiles, "Id", "AvailabilityStatus", artisanWork.ArtisanId);
            return View(artisanWork);
        }

        // GET: ArtisanWorks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artisanWork = await _context.ArtisanWorks
                .Include(a => a.Artisan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artisanWork == null)
            {
                return NotFound();
            }

            return View(artisanWork);
        }

        // POST: ArtisanWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var artisanWork = await _context.ArtisanWorks.FindAsync(id);
            if (artisanWork != null)
            {
                _context.ArtisanWorks.Remove(artisanWork);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtisanWorkExists(Guid id)
        {
            return _context.ArtisanWorks.Any(e => e.Id == id);
        }
    }
}
