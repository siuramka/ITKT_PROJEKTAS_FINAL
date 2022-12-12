using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITKT_PROJEKTAS.Entities;
using ITKT_PROJEKTAS.Helpers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ITKT_PROJEKTAS.Models;
using Microsoft.AspNetCore.Routing;
using AutoMapper;
using System.Runtime.Intrinsics.X86;

namespace ITKT_PROJEKTAS.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly DataContext _context;
        private IMapper _mapper;

        public ReservationsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public IActionResult NotFound()
        {
            return View("NotFound");
        }

        // GET: Reservations
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index()
        {
            var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            var dataContext = _context.Reservation.Include(r => r.Route).Include(r => r.User).Include(p => p.Paslauga).Where(r => r.UserId == int.Parse(userId));
            return View(await dataContext.ToListAsync());
        }
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> IndexAdmin(bool Success)
        {
            if (Success)
            {
                ViewBag.Erorras = "Operacija atlikta sekmingai";
            }
            return View(_context.Reservation.Include(r => r.Route).Include(p => p.Paslauga).Include(r => r.User));
        }

        // GET: Reservations/Details/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Route)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }
        [Authorize(Roles = "User")]
        public IActionResult Create(RouteOrderDTO order)
        {
            var reservation = new Reservation();
            var route = _context.Route.Where(s => s.Id == order.Passingid).FirstOrDefault();
            var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _context.Users.Where(u => u.Id == int.Parse(userId)).FirstOrDefault();

            var userReservations = _context.Reservation.Include(r => r.User).Where(r => r.UserId == int.Parse(userId));
            int userReservationCount = userReservations.Count();

            var userReservationSum = userReservations.Sum(x => x.Price);




            reservation.Boat = order.Boat;
            //Skull emoji , this is awful why am I not doing this properly 
            double totalSum = order.PeopleCount * order.PricePerPerson;
            if (totalSum >= 200 && totalSum < 250)
            {
                reservation.Discount = totalSum * 0.03;
            }
            else if (totalSum >= 250 && totalSum < 400)
            {
                reservation.Discount = totalSum * 0.05;
            }
            else if (totalSum >= 400)
            {
                reservation.Discount = totalSum * 0.10;
            }
            else
                reservation.Discount = 0;

            //Papildoma
            if (userReservationSum >= 1000 && userReservationSum < 2000)
            {
                reservation.Discount += totalSum * 0.03;
            }
            else if (userReservationSum >= 2000 && userReservationSum < 3000)
            {
                reservation.Discount += totalSum * 0.05;
            }
            else if (userReservationSum >= 3000)
            {
                reservation.Discount += totalSum * 0.10;
            }
            reservation.PersonCount = order.PeopleCount;
            reservation.Price = totalSum - reservation.Discount;
            reservation.Route = route;
            reservation.User = user;

  

            if (userReservationCount < 10)
            {
                reservation.ReservationCost = totalSum * 0.05;
                reservation.Price += reservation.ReservationCost;// Papildoma funkcija
            }

            foreach(var paslauga in order.Paslauga)
            {
                Paslauga paslaugaObj = _context.Paslauga.Where(r => r.Id == paslauga.Id).FirstOrDefault();
                reservation.Paslauga.Add(paslaugaObj);
                reservation.Price += paslaugaObj.Price;
            }

            //Nuolaidų sistema pagal vartotojo užsakymo sumą
            //(nuo 200 lt – 3 %, 250 lt – 5 %, 400 lt – 10 %).
            //Jei bendroje sumoje(per kelis kartus) vartotojas yra užsakęs paslaugų daugiau
            //kaip už 1000 lt papildomai+3 %, 2000 lt + 5 %, už 3000 lt + 10 %)

            reservation.Discount = Math.Round(reservation.Discount,2);
            reservation.Price = Math.Round(reservation.Price,2);
            reservation.ReservationCost = Math.Round(reservation.ReservationCost,2);
            _context.Reservation.Add(reservation);
            _context.SaveChanges();
            return View(reservation);
        }

        //// POST: Reservations/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(RouteOrderDTO order)
        //{
        //    return View();
        //    //if (ModelState.IsValid)
        //    //{
        //    //    _context.Add(reservation);
        //    //    await _context.SaveChangesAsync();
        //    //    return RedirectToAction(nameof(Index));
        //    //}
        //    //ViewData["RouteId"] = new SelectList(_context.Route, "Id", "Id", reservation.RouteId);
        //    //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", reservation.UserId);
        //    //return View(reservation);
        //}

        // GET: Reservations/Edit/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            List<SelectListItem> users = new List<SelectListItem>();
            foreach (var user in _context.Users)
            {
                string data = String.Format("{0} {1} {2} {3}", user.Id, user.Username, user.FirstName, user.LastName);
                if (user.Id == reservation.UserId)
                {
                    users.Add(new SelectListItem(data, user.Id.ToString(), true));
                }
                else
                {
                    users.Add(new SelectListItem(data, user.Id.ToString()));
                }
            }
            List<SelectListItem> paslaugos = new List<SelectListItem>();
            foreach (var pasl in _context.Paslauga)
            {
                //string data = String.Format("{0} {1}eur", pasl.Name, pasl.Price);
                //if (reservation.PaslaugaId != null)
                //{
                //    if (pasl.Id == reservation.PaslaugaId)
                //    {

                //        paslaugos.Add(new SelectListItem(data, pasl.Id.ToString(), true));
                //    }
                //}
                //else
                //{
                //    paslaugos.Add(new SelectListItem(data, pasl.Id.ToString()));
                //}
            }
            ViewData["Paslaugos"] = paslaugos;
            ViewData["Users"] = users;
            List<SelectListItem> routes = new List<SelectListItem>();// select only routes that are not reserved
            foreach (var route in _context.Route.Include(r => r.Reservation))
            {
                string data = String.Format("{0} {1} {2} {3}km", route.Id, route.Name, route.Date, route.Length);
                if (route.Id == reservation.RouteId)
                {
                    routes.Add(new SelectListItem(data, route.Id.ToString(), true));
                }
                else
                {
                    if (route.Reservation == null)
                    {
                        routes.Add(new SelectListItem(data, route.Id.ToString()));
                    }
                }
            }
            ViewData["Routes"] = routes;

            return View(_mapper.Map<ReservationEditDTO>(reservation));
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RouteId,Boat,Price,Discount,PersonCount,UserId, PaslaugaId")] ReservationEditDTO reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var reservOld = await _context.Reservation.FindAsync(id);
                    Reservation resrv = _mapper.Map(reservation, reservOld);
                    _context.Update(resrv);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexAdmin), new RouteValueDictionary(new
                {
                    Success = true
                }));
            }
            List<SelectListItem> users = new List<SelectListItem>();
            foreach (var user in _context.Users)
            {
                string data = String.Format("{0} {1} {2} {3}", user.Id, user.Username, user.FirstName, user.LastName);
                if (user.Id == reservation.UserId)
                {
                    users.Add(new SelectListItem(data, user.Id.ToString(), true));
                }
                else
                {
                    users.Add(new SelectListItem(data, user.Id.ToString()));
                }
            }
            ViewData["Users"] = users;
            List<SelectListItem> paslaugos = new List<SelectListItem>();
            foreach (var pasl in _context.Paslauga)
            {
                string data = String.Format("{0} {1}eur", pasl.Name, pasl.Price);
                if (reservation.PaslaugaId != null)
                {
                    if (pasl.Id == reservation.PaslaugaId)
                    {

                        paslaugos.Add(new SelectListItem(data, pasl.Id.ToString(), true));
                    }
                }
                else
                {
                    paslaugos.Add(new SelectListItem(data, pasl.Id.ToString()));
                }
            }
            ViewData["Paslaugos"] = paslaugos;
            List<SelectListItem> routes = new List<SelectListItem>();
            foreach (var route in _context.Route)
            {
                string data = String.Format("{0} {1} {2} {3}km", route.Id, route.Name, route.Date, route.Length);
                if (route.Id == reservation.RouteId)
                {
                    routes.Add(new SelectListItem(data, route.Id.ToString(), true));
                }
                else
                {
                    routes.Add(new SelectListItem(data, route.Id.ToString()));
                }
            }
            ViewData["Routes"] = routes;
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Route)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [Authorize(Roles = "Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservation == null)
            {
                return Problem("Entity set 'DataContext.Reservation'  is null.");
            }
            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservation.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexAdmin), new RouteValueDictionary(new
            {
                Success = true
            }));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }
    }
}
