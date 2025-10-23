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
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reports
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reports.Include(r => r.ArtisanFeed).Include(r => r.Comment).Include(r => r.ReportedUser).Include(r => r.Reporter).Include(r => r.ReviewedBy).Include(r => r.UserFeed);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.ArtisanFeed)
                .Include(r => r.Comment)
                .Include(r => r.ReportedUser)
                .Include(r => r.Reporter)
                .Include(r => r.ReviewedBy)
                .Include(r => r.UserFeed)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create
        public IActionResult Create()
        {
            ViewData["ArtisanFeedId"] = new SelectList(_context.ArtisanFeeds, "Id", "Description");
            ViewData["CommentId"] = new SelectList(_context.Comments, "Id", "CommentType");
            ViewData["ReportedUserId"] = new SelectList(_context.Users, "Id", "Email");
            ViewData["ReporterId"] = new SelectList(_context.Users, "Id", "Email");
            ViewData["ReviewedById"] = new SelectList(_context.Users, "Id", "Email");
            ViewData["UserFeedId"] = new SelectList(_context.UserFeeds, "Id", "Description");
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReporterId,ContentType,UserFeedId,ArtisanFeedId,CommentId,ReportedUserId,Reason,Description,Status,ReviewedById,ResolutionNotes,CreatedAt,ReviewedAt")] Report report)
        {
            if (ModelState.IsValid)
            {
                report.Id = Guid.NewGuid();
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtisanFeedId"] = new SelectList(_context.ArtisanFeeds, "Id", "Description", report.ArtisanFeedId);
            ViewData["CommentId"] = new SelectList(_context.Comments, "Id", "CommentType", report.CommentId);
            ViewData["ReportedUserId"] = new SelectList(_context.Users, "Id", "Email", report.ReportedUserId);
            ViewData["ReporterId"] = new SelectList(_context.Users, "Id", "Email", report.ReporterId);
            ViewData["ReviewedById"] = new SelectList(_context.Users, "Id", "Email", report.ReviewedById);
            ViewData["UserFeedId"] = new SelectList(_context.UserFeeds, "Id", "Description", report.UserFeedId);
            return View(report);
        }

        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            ViewData["ArtisanFeedId"] = new SelectList(_context.ArtisanFeeds, "Id", "Description", report.ArtisanFeedId);
            ViewData["CommentId"] = new SelectList(_context.Comments, "Id", "CommentType", report.CommentId);
            ViewData["ReportedUserId"] = new SelectList(_context.Users, "Id", "Email", report.ReportedUserId);
            ViewData["ReporterId"] = new SelectList(_context.Users, "Id", "Email", report.ReporterId);
            ViewData["ReviewedById"] = new SelectList(_context.Users, "Id", "Email", report.ReviewedById);
            ViewData["UserFeedId"] = new SelectList(_context.UserFeeds, "Id", "Description", report.UserFeedId);
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ReporterId,ContentType,UserFeedId,ArtisanFeedId,CommentId,ReportedUserId,Reason,Description,Status,ReviewedById,ResolutionNotes,CreatedAt,ReviewedAt")] Report report)
        {
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.Id))
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
            ViewData["ArtisanFeedId"] = new SelectList(_context.ArtisanFeeds, "Id", "Description", report.ArtisanFeedId);
            ViewData["CommentId"] = new SelectList(_context.Comments, "Id", "CommentType", report.CommentId);
            ViewData["ReportedUserId"] = new SelectList(_context.Users, "Id", "Email", report.ReportedUserId);
            ViewData["ReporterId"] = new SelectList(_context.Users, "Id", "Email", report.ReporterId);
            ViewData["ReviewedById"] = new SelectList(_context.Users, "Id", "Email", report.ReviewedById);
            ViewData["UserFeedId"] = new SelectList(_context.UserFeeds, "Id", "Description", report.UserFeedId);
            return View(report);
        }

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.ArtisanFeed)
                .Include(r => r.Comment)
                .Include(r => r.ReportedUser)
                .Include(r => r.Reporter)
                .Include(r => r.ReviewedBy)
                .Include(r => r.UserFeed)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report != null)
            {
                _context.Reports.Remove(report);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(Guid id)
        {
            return _context.Reports.Any(e => e.Id == id);
        }
    }
}
