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
    public class ReactionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reactions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reactions.Include(r => r.ArtisanFeed).Include(r => r.Comment).Include(r => r.User).Include(r => r.UserFeed);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reactions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaction = await _context.Reactions
                .Include(r => r.ArtisanFeed)
                .Include(r => r.Comment)
                .Include(r => r.User)
                .Include(r => r.UserFeed)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaction == null)
            {
                return NotFound();
            }

            return View(reaction);
        }

        // GET: Reactions/Create
        public IActionResult Create()
        {
            ViewData["ArtisanFeedId"] = new SelectList(_context.ArtisanFeeds, "Id", "Description");
            ViewData["CommentId"] = new SelectList(_context.Comments, "Id", "CommentType");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            ViewData["UserFeedId"] = new SelectList(_context.UserFeeds, "Id", "Description");
            return View();
        }

        // POST: Reactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ReactionType,ContentType,UserFeedId,ArtisanFeedId,CommentId,CreatedAt")] Reaction reaction)
        {
            if (ModelState.IsValid)
            {
                reaction.Id = Guid.NewGuid();
                _context.Add(reaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtisanFeedId"] = new SelectList(_context.ArtisanFeeds, "Id", "Description", reaction.ArtisanFeedId);
            ViewData["CommentId"] = new SelectList(_context.Comments, "Id", "CommentType", reaction.CommentId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", reaction.UserId);
            ViewData["UserFeedId"] = new SelectList(_context.UserFeeds, "Id", "Description", reaction.UserFeedId);
            return View(reaction);
        }

        // GET: Reactions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaction = await _context.Reactions.FindAsync(id);
            if (reaction == null)
            {
                return NotFound();
            }
            ViewData["ArtisanFeedId"] = new SelectList(_context.ArtisanFeeds, "Id", "Description", reaction.ArtisanFeedId);
            ViewData["CommentId"] = new SelectList(_context.Comments, "Id", "CommentType", reaction.CommentId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", reaction.UserId);
            ViewData["UserFeedId"] = new SelectList(_context.UserFeeds, "Id", "Description", reaction.UserFeedId);
            return View(reaction);
        }

        // POST: Reactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,ReactionType,ContentType,UserFeedId,ArtisanFeedId,CommentId,CreatedAt")] Reaction reaction)
        {
            if (id != reaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReactionExists(reaction.Id))
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
            ViewData["ArtisanFeedId"] = new SelectList(_context.ArtisanFeeds, "Id", "Description", reaction.ArtisanFeedId);
            ViewData["CommentId"] = new SelectList(_context.Comments, "Id", "CommentType", reaction.CommentId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", reaction.UserId);
            ViewData["UserFeedId"] = new SelectList(_context.UserFeeds, "Id", "Description", reaction.UserFeedId);
            return View(reaction);
        }

        // GET: Reactions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaction = await _context.Reactions
                .Include(r => r.ArtisanFeed)
                .Include(r => r.Comment)
                .Include(r => r.User)
                .Include(r => r.UserFeed)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reaction == null)
            {
                return NotFound();
            }

            return View(reaction);
        }

        // POST: Reactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reaction = await _context.Reactions.FindAsync(id);
            if (reaction != null)
            {
                _context.Reactions.Remove(reaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReactionExists(Guid id)
        {
            return _context.Reactions.Any(e => e.Id == id);
        }
    }
}
