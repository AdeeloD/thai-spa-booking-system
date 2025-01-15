using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MomoAndMemeBookingSystem.Data;
using MomoAndMemeBookingSystem.Models;

namespace MomoAndMemeBookingSystem.Controllers
{
    public class MasseursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MasseursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Masseurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Masseurs.ToListAsync());
        }

        // GET: Masseurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masseur = await _context.Masseurs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (masseur == null)
            {
                return NotFound();
            }

            return View(masseur);
        }

        // GET: Masseurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Masseurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Bio")] Masseur masseur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(masseur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(masseur);
        }

        // GET: Masseurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masseur = await _context.Masseurs.FindAsync(id);
            if (masseur == null)
            {
                return NotFound();
            }
            return View(masseur);
        }

        // POST: Masseurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Bio")] Masseur masseur)
        {
            if (id != masseur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(masseur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MasseurExists(masseur.Id))
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
            return View(masseur);
        }

        // GET: Masseurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masseur = await _context.Masseurs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (masseur == null)
            {
                return NotFound();
            }

            return View(masseur);
        }

        // POST: Masseurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var masseur = await _context.Masseurs.FindAsync(id);
            if (masseur != null)
            {
                _context.Masseurs.Remove(masseur);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MasseurExists(int id)
        {
            return _context.Masseurs.Any(e => e.Id == id);
        }
    }
}
