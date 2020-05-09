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
    [Authorize(Roles = "admin")]
    public class BookNumbersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookNumbersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookNumbers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BookNumbers.Include(b => b.Book);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BookNumbers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookNumbers = await _context.BookNumbers
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookNumbers == null)
            {
                return NotFound();
            }

            return View(bookNumbers);
        }

        // GET: BookNumbers/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id");
            return View();
        }

        // POST: BookNumbers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Number,BookId")] BookNumbers bookNumbers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookNumbers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", bookNumbers.BookId);
            return View(bookNumbers);
        }

        // GET: BookNumbers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookNumbers = await _context.BookNumbers.FindAsync(id);
            if (bookNumbers == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", bookNumbers.BookId);
            return View(bookNumbers);
        }

        // POST: BookNumbers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Number,BookId")] BookNumbers bookNumbers)
        {
            if (id != bookNumbers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookNumbers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookNumbersExists(bookNumbers.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", bookNumbers.BookId);
            return View(bookNumbers);
        }

        // GET: BookNumbers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookNumbers = await _context.BookNumbers
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookNumbers == null)
            {
                return NotFound();
            }

            return View(bookNumbers);
        }

        // POST: BookNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookNumbers = await _context.BookNumbers.FindAsync(id);
            _context.BookNumbers.Remove(bookNumbers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookNumbersExists(int id)
        {
            return _context.BookNumbers.Any(e => e.Id == id);
        }
    }
}
