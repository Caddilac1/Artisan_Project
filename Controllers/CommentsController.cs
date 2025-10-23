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
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Comments.Include(c => c.ArtisanFeed).Include(c => c.ParentComment).Include(c => c.User).Include(c => c.UserFeed);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.ArtisanFeed)
                .Include(c => c.ParentComment)
                .Include(c => c.User)
                .Include(c => c.UserFeed)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            ViewData["ArtisanFeedId"] = new SelectList(_context.ArtisanFeeds, "Id", "Description");
            ViewData["ParentCommentId"] = new SelectList(_context.Comments, "Id", "CommentType");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            ViewData["UserFeedId"] = new SelectList(_context.UserFeeds, "Id", "Description");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,UserFeedId,ArtisanFeedId,ParentCommentId,CommentType,CreatedAt")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Id = Guid.NewGuid();
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtisanFeedId"] = new SelectList(_context.ArtisanFeeds, "Id", "Description", comment.ArtisanFeedId);
            ViewData["ParentCommentId"] = new SelectList(_context.Comments, "Id", "CommentType", comment.ParentCommentId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", comment.UserId);
            ViewData["UserFeedId"] = new SelectList(_context.UserFeeds, "Id", "Description", comment.UserFeedId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["ArtisanFeedId"] = new SelectList(_context.ArtisanFeeds, "Id", "Description", comment.ArtisanFeedId);
            ViewData["ParentCommentId"] = new SelectList(_context.Comments, "Id", "CommentType", comment.ParentCommentId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", comment.UserId);
            ViewData["UserFeedId"] = new SelectList(_context.UserFeeds, "Id", "Description", comment.UserFeedId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,UserFeedId,ArtisanFeedId,ParentCommentId,CommentType,CreatedAt")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
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
            ViewData["ArtisanFeedId"] = new SelectList(_context.ArtisanFeeds, "Id", "Description", comment.ArtisanFeedId);
            ViewData["ParentCommentId"] = new SelectList(_context.Comments, "Id", "CommentType", comment.ParentCommentId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", comment.UserId);
            ViewData["UserFeedId"] = new SelectList(_context.UserFeeds, "Id", "Description", comment.UserFeedId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.ArtisanFeed)
                .Include(c => c.ParentComment)
                .Include(c => c.User)
                .Include(c => c.UserFeed)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(Guid id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
