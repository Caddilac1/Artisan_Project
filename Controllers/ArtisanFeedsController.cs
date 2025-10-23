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
    public class ArtisanFeedsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtisanFeedsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ArtisanFeeds
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ArtisanFeeds.Include(a => a.Artisan);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ArtisanFeeds/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artisanFeed = await _context.ArtisanFeeds
                .Include(a => a.Artisan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artisanFeed == null)
            {
                return NotFound();
            }

            return View(artisanFeed);
        }

        // GET: ArtisanFeeds/Create
        public IActionResult Create()
        {
            ViewData["ArtisanId"] = new SelectList(_context.ArtisanProfiles, "Id", "AvailabilityStatus");
            return View();
        }

        // POST: ArtisanFeeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArtisanId,Title,Slug,Description,PostType,ServiceCategory,FeaturedImage,VideoUrl,Price,DiscountPercentage,ValidFrom,ValidUntil,ViewsCount,CommentsCount,LikesCount,DislikesCount,ReportsCount,SharesCount,CreatedAt,UpdatedAt,IsActive,IsFeatured,IsPromoted,IsFlagged")] ArtisanFeed artisanFeed)
        {
            if (ModelState.IsValid)
            {
                artisanFeed.Id = Guid.NewGuid();
                _context.Add(artisanFeed);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtisanId"] = new SelectList(_context.ArtisanProfiles, "Id", "AvailabilityStatus", artisanFeed.ArtisanId);
            return View(artisanFeed);
        }

        // GET: ArtisanFeeds/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artisanFeed = await _context.ArtisanFeeds.FindAsync(id);
            if (artisanFeed == null)
            {
                return NotFound();
            }
            ViewData["ArtisanId"] = new SelectList(_context.ArtisanProfiles, "Id", "AvailabilityStatus", artisanFeed.ArtisanId);
            return View(artisanFeed);
        }

        // POST: ArtisanFeeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ArtisanId,Title,Slug,Description,PostType,ServiceCategory,FeaturedImage,VideoUrl,Price,DiscountPercentage,ValidFrom,ValidUntil,ViewsCount,CommentsCount,LikesCount,DislikesCount,ReportsCount,SharesCount,CreatedAt,UpdatedAt,IsActive,IsFeatured,IsPromoted,IsFlagged")] ArtisanFeed artisanFeed)
        {
            if (id != artisanFeed.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artisanFeed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtisanFeedExists(artisanFeed.Id))
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
            ViewData["ArtisanId"] = new SelectList(_context.ArtisanProfiles, "Id", "AvailabilityStatus", artisanFeed.ArtisanId);
            return View(artisanFeed);
        }

        // GET: ArtisanFeeds/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artisanFeed = await _context.ArtisanFeeds
                .Include(a => a.Artisan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artisanFeed == null)
            {
                return NotFound();
            }

            return View(artisanFeed);
        }

        // POST: ArtisanFeeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var artisanFeed = await _context.ArtisanFeeds.FindAsync(id);
            if (artisanFeed != null)
            {
                _context.ArtisanFeeds.Remove(artisanFeed);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtisanFeedExists(Guid id)
        {
            return _context.ArtisanFeeds.Any(e => e.Id == id);
        }
    }
}
