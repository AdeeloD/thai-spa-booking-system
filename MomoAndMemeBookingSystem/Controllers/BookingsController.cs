using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MomoAndMemeBookingSystem.Data;
using MomoAndMemeBookingSystem.Models;
using MomoAndMemeBookingSystem.Services;


namespace MomoAndMemeBookingSystem.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly EmailService _emailService;

        public BookingsController(ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings
                .Include(b => b.MassageType)
                .Include(b => b.Masseur)
                .ToListAsync();
            return View(bookings);
        }


        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.MassageType)
                .Include(b => b.Masseur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["MassageTypeId"] = new SelectList(_context.MassageTypes, "Id", "Name");
            ViewData["MasseurId"] = new SelectList(_context.Masseurs, "Id", "FullName");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,MassageTypeId,MasseurId,CustomerName,CustomerEmail")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                // Konvertáljuk az időpontot UTC-re
                booking.Date = DateTime.SpecifyKind(booking.Date, DateTimeKind.Utc);

                var isBooked = await _context.Bookings
                    .AnyAsync(b => b.Date == booking.Date && b.MasseurId == booking.MasseurId);

                if (isBooked)
                {
                    ModelState.AddModelError("", "The selected masseur is not available at the chosen time.");
                }
                else
                {
                    _context.Add(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                _emailService.SendEmail(
                booking.CustomerEmail,
                "Booking Confirmation",
                $"Dear {booking.CustomerName}, your booking on {booking.Date} is confirmed."
                 );

            }

            ViewData["MassageTypeId"] = new SelectList(_context.MassageTypes, "Id", "Name", booking.MassageTypeId);
            ViewData["MasseurId"] = new SelectList(_context.Masseurs, "Id", "FullName", booking.MasseurId);
            return View(booking);

        }



        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["MassageTypeId"] = new SelectList(_context.MassageTypes, "Id", "Name", booking.MassageTypeId);
            ViewData["MasseurId"] = new SelectList(_context.Masseurs, "Id", "FullName", booking.MasseurId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,MassageTypeId,MasseurId,CustomerName,CustomerEmail")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
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
            ViewData["MassageTypeId"] = new SelectList(_context.MassageTypes, "Id", "Name", booking.MassageTypeId);
            ViewData["MasseurId"] = new SelectList(_context.Masseurs, "Id", "FullName", booking.MasseurId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.MassageType)
                .Include(b => b.Masseur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
        public async Task<IActionResult> ExportCsv()
        {
            var bookings = await _context.Bookings
                .Include(b => b.MassageType)
                .Include(b => b.Masseur)
                .ToListAsync();

            var csv = "Customer Name,Customer Email,Massage Type,Masseur,Date\n";
            foreach (var booking in bookings)
            {
                csv += $"{booking.CustomerName},{booking.CustomerEmail},{booking.MassageType.Name},{booking.Masseur.FullName},{booking.Date:yyyy-MM-dd HH:mm}\n";
            }

            return File(System.Text.Encoding.UTF8.GetBytes(csv), "text/csv", "bookings.csv");
        }

    }
}
