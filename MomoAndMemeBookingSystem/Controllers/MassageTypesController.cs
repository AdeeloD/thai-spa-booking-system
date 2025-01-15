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
    public class MassageTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MassageTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MassageTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MassageTypes.ToListAsync());
        }

        // GET: MassageTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var massageType = await _context.MassageTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (massageType == null)
            {
                return NotFound();
            }

            return View(massageType);
        }

        // GET: MassageTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MassageTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price")] MassageType massageType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(massageType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(massageType);
        }

        // GET: MassageTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var massageType = await _context.MassageTypes.FindAsync(id);
            if (massageType == null)
            {
                return NotFound();
            }
            return View(massageType);
        }

        // POST: MassageTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price")] MassageType massageType)
        {
            if (id != massageType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(massageType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MassageTypeExists(massageType.Id))
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
            return View(massageType);
        }

        // GET: MassageTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var massageType = await _context.MassageTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (massageType == null)
            {
                return NotFound();
            }

            return View(massageType);
        }

        // POST: MassageTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var massageType = await _context.MassageTypes.FindAsync(id);
            if (massageType != null)
            {
                _context.MassageTypes.Remove(massageType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MassageTypeExists(int id)
        {
            return _context.MassageTypes.Any(e => e.Id == id);
        }
    }
}
