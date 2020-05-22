using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Milestone1.Data;
using Milestone1.Models;

namespace Milestone1.Controllers
{
    [Authorize(Roles = "admin,moderator")]
    public class AuthorBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthorBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AuthorBooks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AuthorBooks.Include(a => a.Author).Include(a => a.Book);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AuthorBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorBooks = await _context.AuthorBooks
                .Include(a => a.Author)
                .Include(a => a.Book)
                .FirstOrDefaultAsync(m => m.AuthorId == id);
            if (authorBooks == null)
            {
                return NotFound();
            }

            return View(authorBooks);
        }

        // GET: AuthorBooks/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id");
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id");
            return View();
        }

        // POST: AuthorBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,BookId")] AuthorBooks authorBooks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authorBooks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", authorBooks.AuthorId);
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", authorBooks.BookId);
            return View(authorBooks);
        }

        // GET: AuthorBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorBooks = await _context.AuthorBooks.FindAsync(id);
            if (authorBooks == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", authorBooks.AuthorId);
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", authorBooks.BookId);
            return View(authorBooks);
        }

        // POST: AuthorBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorId,BookId")] AuthorBooks authorBooks)
        {
            if (id != authorBooks.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorBooks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorBooksExists(authorBooks.AuthorId))
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
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", authorBooks.AuthorId);
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", authorBooks.BookId);
            return View(authorBooks);
        }

        // GET: AuthorBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorBooks = await _context.AuthorBooks
                .Include(a => a.Author)
                .Include(a => a.Book)
                .FirstOrDefaultAsync(m => m.AuthorId == id);
            if (authorBooks == null)
            {
                return NotFound();
            }

            return View(authorBooks);
        }

        // POST: AuthorBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorBooks = await _context.AuthorBooks.FindAsync(id);
            _context.AuthorBooks.Remove(authorBooks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorBooksExists(int id)
        {
            return _context.AuthorBooks.Any(e => e.AuthorId == id);
        }
    }
}
