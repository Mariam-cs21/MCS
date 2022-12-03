using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MCS.Data;
using MCS.Models;

namespace MCS.Controllers
{
    public class CosmeticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CosmeticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cosmetics
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cosmetic.ToListAsync());
        }

        public IActionResult SearchForm()
        {
            return View();
        }

        public async Task<IActionResult> SearchResult(string Title)
        {
            return View("Index",await _context.Cosmetic.Where(a=>a.Title.Contains(Title)).ToListAsync());
        }

        // GET: Cosmetics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cosmetic = await _context.Cosmetic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cosmetic == null)
            {
                return NotFound();
            }

            return View(cosmetic);
        }

        // GET: Cosmetics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cosmetics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,URL")] Cosmetic cosmetic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cosmetic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cosmetic);
        }

        // GET: Cosmetics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cosmetic = await _context.Cosmetic.FindAsync(id);
            if (cosmetic == null)
            {
                return NotFound();
            }
            return View(cosmetic);
        }

        // POST: Cosmetics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,URL")] Cosmetic cosmetic)
        {
            if (id != cosmetic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cosmetic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CosmeticExists(cosmetic.Id))
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
            return View(cosmetic);
        }

        // GET: Cosmetics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cosmetic = await _context.Cosmetic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cosmetic == null)
            {
                return NotFound();
            }

            return View(cosmetic);
        }

        // POST: Cosmetics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cosmetic = await _context.Cosmetic.FindAsync(id);
            _context.Cosmetic.Remove(cosmetic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CosmeticExists(int id)
        {
            return _context.Cosmetic.Any(e => e.Id == id);
        }
    }
}
