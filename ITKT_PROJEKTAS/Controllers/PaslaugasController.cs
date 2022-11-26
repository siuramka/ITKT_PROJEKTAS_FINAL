using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITKT_PROJEKTAS.Entities;
using ITKT_PROJEKTAS.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ITKT_PROJEKTAS.Controllers
{
    [Authorize(Roles = "Manager")]
    public class PaslaugasController : Controller
    {
        private readonly DataContext _context;

        public PaslaugasController(DataContext context)
        {
            _context = context;
        }

        // GET: Paslaugas
        public async Task<IActionResult> Index(bool Success)
        {
            if(Success)
            {
                ViewBag.Erorras = "Operacija atlikta sekingai";
            }
              return View(await _context.Paslauga.ToListAsync());
        }

        // GET: Paslaugas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Paslauga == null)
            {
                return NotFound();
            }

            var paslauga = await _context.Paslauga
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paslauga == null)
            {
                return NotFound();
            }

            return View(paslauga);
        }

        // GET: Paslaugas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Paslaugas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price")] Paslauga paslauga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paslauga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new RouteValueDictionary(new
                {
                    Success = true
                }));
            }
            return View(paslauga);
        }

        // GET: Paslaugas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Paslauga == null)
            {
                return NotFound();
            }

            var paslauga = await _context.Paslauga.FindAsync(id);
            if (paslauga == null)
            {
                return NotFound();
            }
            return View(paslauga);
        }

        // POST: Paslaugas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price")] Paslauga paslauga)
        {
            if (id != paslauga.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paslauga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaslaugaExists(paslauga.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new RouteValueDictionary(new
                {
                    Success = true
                }));
            }
            return View(paslauga);
        }

        // GET: Paslaugas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Paslauga == null)
            {
                return NotFound();
            }

            var paslauga = await _context.Paslauga
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paslauga == null)
            {
                return NotFound();
            }

            return View(paslauga);
        }

        // POST: Paslaugas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Paslauga == null)
            {
                return Problem("Entity set 'DataContext.Paslauga'  is null.");
            }
            var paslauga = await _context.Paslauga.FindAsync(id);
            if (paslauga != null)
            {
                var reservations = _context.Reservation.Include(r => r.Paslauga).Where(r => r.PaslaugaId == id);
                foreach(var resv in reservations)
                {
                    resv.Paslauga = null;
                    resv.PaslaugaId = null;
                }
                await _context.SaveChangesAsync();
                _context.Paslauga.Remove(paslauga);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new RouteValueDictionary(new
            {
                Success = true
            }));
        }

        private bool PaslaugaExists(int id)
        {
          return _context.Paslauga.Any(e => e.Id == id);
        }
    }
}
