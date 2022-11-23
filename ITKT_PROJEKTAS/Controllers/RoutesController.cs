using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITKT_PROJEKTAS.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using ITKT_PROJEKTAS.Models;

namespace ITKT_PROJEKTAS.Controllers
{
    public class RoutesController : Controller
    {
        private readonly DataContext _context;

        public RoutesController(DataContext context)
        {
            _context = context;
        }

        // GET: Routes
        [Authorize]
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.DiffSortParm = sortOrder == "Difficulity" ? "difficulityDesc" : "Difficulity";
            ViewBag.LengthSortParm = sortOrder == "Length" ? "lengthDesc" : "Length";
            var resevations = _context.Reservation.Include(r => r.Route).Select(r => r.RouteId).ToList();
            //var customers = context.Customers.WhereBulkNotContains(deserializedCustomers);
            //var customerIds = deserializedCustomers.Select(x => x.CustomerID).ToList();
            //var customers = context.Customers.Where(x => !customerIds.Contains(x.CustomerID)).ToList();
            //var routes = _context.Route.Where(x => !_context.Reservation.Include(r => r.Route).Any(x2 => x2.Route.Id == x.Id));
            var routes = _context.Route.Where(x => !resevations.Contains(x.Id)).Select(x => x);
            switch (sortOrder)
            {
                case "Difficulity":
                    routes = routes.OrderBy(s => s.Difficulity);
                    break;
                case "DifficulityDesc":
                    routes = routes.OrderByDescending(s => s.Difficulity);
                    break;
                case "Length":
                    routes = routes.OrderBy(s => s.Length);
                    break;
                case "LengthDesc":
                    routes = routes.OrderByDescending(s => s.Length);
                    break;
                default:
                    break;
            }
            return View(routes);
        }

        // GET: Routes/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null || _context.Route == null)
            {
                return NotFound();
            }

            var route = await _context.Route
                .FirstOrDefaultAsync(m => m.Id == id);
            if (route == null)
            {
                return NotFound();
            }
            //:D
            RouteOrderDTO routeOrderDTO = new RouteOrderDTO();
            routeOrderDTO.Name = route.Name;
            routeOrderDTO.Description = route.Description;
            routeOrderDTO.PricePerPerson = route.PricePerPerson;
            routeOrderDTO.MaxPeople = route.MaxPeople;
            routeOrderDTO.Difficulity = route.Difficulity;
            routeOrderDTO.Date = route.Date;
            routeOrderDTO.Length = route.Length;
            routeOrderDTO.PeopleCount = 0;
            routeOrderDTO.Passingid = route.Id;
            return View(routeOrderDTO);
        }

        // GET: Routes/Create
        [Authorize()]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Routes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Date,Length,Difficulity,Description,PricePerPerson,MaxPeople")] Entities.Route route)
        {
            if (ModelState.IsValid)
            {
                _context.Add(route);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            return View(route);
        }

        // GET: Routes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Route == null)
            {
                return NotFound();
            }

            var route = await _context.Route.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }
            return View(route);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Date,Length,Difficulity,Description,PricePerPerson,MaxPeople")] Entities.Route route)
        {
            if (id != route.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(route);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteExists(route.Id))
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
            return View(route);
        }

        // GET: Routes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Route == null)
            {
                return NotFound();
            }

            var route = await _context.Route
                .FirstOrDefaultAsync(m => m.Id == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Route == null)
            {
                return Problem("Entity set 'DataContext.Route'  is null.");
            }
            var route = await _context.Route.FindAsync(id);
            if (route != null)
            {
                _context.Route.Remove(route);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost, ActionName("PassOrder")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PassOrder(RouteOrderDTO order)
        {
            if (!ModelState.IsValid)
            {
                return Problem("Klaida");
            }
            return RedirectToAction("Create", "Reservations", order);
        }

        private bool RouteExists(int id)
        {
            return _context.Route.Any(e => e.Id == id);
        }
    }
}
