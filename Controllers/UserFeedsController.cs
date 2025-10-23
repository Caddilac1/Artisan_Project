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
    public class UserFeedsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserFeedsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserFeeds
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserFeeds.Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserFeeds/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFeed = await _context.UserFeeds
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userFeed == null)
            {
                return NotFound();
            }

            return View(userFeed);
        }

        // GET: UserFeeds/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: UserFeeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Title,Slug,Description,JobCategory,BudgetRangeMin,BudgetRangeMax,InvoiceImage,InvoiceAmount,InvoiceDate,AdditionalDocuments,Location,PreferredStartDate,Deadline,Status,Priority,ViewsCount,CommentsCount,LikesCount,DislikesCount,ReportsCount,CreatedAt,UpdatedAt,IsActive,IsFeatured,IsFlagged")] UserFeed userFeed)
        {
            if (ModelState.IsValid)
            {
                userFeed.Id = Guid.NewGuid();
                _context.Add(userFeed);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", userFeed.UserId);
            return View(userFeed);
        }

        // GET: UserFeeds/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFeed = await _context.UserFeeds.FindAsync(id);
            if (userFeed == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", userFeed.UserId);
            return View(userFeed);
        }

        // POST: UserFeeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,Title,Slug,Description,JobCategory,BudgetRangeMin,BudgetRangeMax,InvoiceImage,InvoiceAmount,InvoiceDate,AdditionalDocuments,Location,PreferredStartDate,Deadline,Status,Priority,ViewsCount,CommentsCount,LikesCount,DislikesCount,ReportsCount,CreatedAt,UpdatedAt,IsActive,IsFeatured,IsFlagged")] UserFeed userFeed)
        {
            if (id != userFeed.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userFeed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserFeedExists(userFeed.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", userFeed.UserId);
            return View(userFeed);
        }

        // GET: UserFeeds/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFeed = await _context.UserFeeds
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userFeed == null)
            {
                return NotFound();
            }

            return View(userFeed);
        }

        // POST: UserFeeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userFeed = await _context.UserFeeds.FindAsync(id);
            if (userFeed != null)
            {
                _context.UserFeeds.Remove(userFeed);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserFeedExists(Guid id)
        {
            return _context.UserFeeds.Any(e => e.Id == id);
        }
    }
}
