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
using ITKT_PROJEKTAS.Entities;
using System.Security.Claims;
using System.Xml.Linq;
using AutoMapper;

namespace ITKT_PROJEKTAS.Controllers
{
    public class RoutesController : Controller
    {
        private readonly DataContext _context;
        private IMapper _mapper;

        public RoutesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Routes
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.DiffSortParm = sortOrder == "Difficulity" ? "difficulityDesc" : "Difficulity";
            ViewBag.LengthSortParm = sortOrder == "Length" ? "lengthDesc" : "Length";
            var resevations = _context.Reservation.Include(r => r.Route).Select(r => r.RouteId).ToList();
            //var customers = context.Customers.WhereBulkNotContains(deserializedCustomers);
            //var customerIds = deserializedCustomers.Select(x => x.CustomerID).ToList();
            //var customers = context.Customers.Where(x => !customerIds.Contains(x.CustomerID)).ToList();
            //var routes = _context.Route.Where(x => !_context.Reservation.Include(r => r.Route).Any(x2 => x2.Route.Id == x.Id));
            //sitas buvo var routes = _context.Route.Where(x => !resevations.Contains(x.Id)).Select(x => x);
            var routes = _context.Route.Include(z => z.Reservation).Select(x => x);
            switch (sortOrder)
            {
                case "Difficulity":
                    routes = routes.OrderBy(s => (int)s.Difficulity);
                    break;
                case "difficulityDesc":
                    routes = routes.OrderByDescending(s => (int)s.Difficulity);
                    break;
                case "Length":
                    routes = routes.OrderBy(s => s.Length);
                    break;
                case "lengthDesc":
                    routes = routes.OrderByDescending(s => s.Length);
                    break;
                default:
                    break;
            }
            return View(routes);
        }
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> IndexAdmin(string sortOrder, bool Success)
        {
            if (Success)
            {
                ViewBag.Erorras = "Atlikta operacija sekmingai.";
            }
            ViewBag.DiffSortParm = sortOrder == "Difficulity" ? "difficulityDesc" : "Difficulity";
            ViewBag.LengthSortParm = sortOrder == "Length" ? "lengthDesc" : "Length";
            var resevations = _context.Reservation.Include(r => r.Route).Select(r => r.RouteId).ToList();
            //var customers = context.Customers.WhereBulkNotContains(deserializedCustomers);
            //var customerIds = deserializedCustomers.Select(x => x.CustomerID).ToList();
            //var customers = context.Customers.Where(x => !customerIds.Contains(x.CustomerID)).ToList();
            //var routes = _context.Route.Where(x => !_context.Reservation.Include(r => r.Route).Any(x2 => x2.Route.Id == x.Id));
            var routes = _context.Route.Select(x => x);
            switch (sortOrder)
            {
                case "Difficulity":
                    routes = routes.OrderBy(s => (int)s.Difficulity);
                    break;
                case "difficulityDesc":
                    routes = routes.OrderByDescending(s => (int)s.Difficulity);
                    break;
                case "Length":
                    routes = routes.OrderBy(s => s.Length);
                    break;
                case "lengthDesc":
                    routes = routes.OrderByDescending(s => s.Length);
                    break;
                default:
                    break;
            }
            return View(routes);
        }

        // GET: Routes/Details/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int? id, int? errr, int? add, RouteOrderDTO? dataEx)
        {
            if (errr == 1)
            {
                ViewBag.Erorras = "Pasirinktas skačius žmonių netelpa baidarėse. Baidarės yra dvi-vietės.";
            }
            else if (errr == 2)
            {
                ViewBag.Erorras = "Pasirinktas skaičius žmonių netelpa kanojose. Kanojos yra keturvietės.";
            }
            else if (errr == 3)
            {
                ViewBag.Erorras = "Pasirinktas skačius žmonių netelpa valtyse. Valtis yra sešiavietė.";
            }
            else if (errr == 4)
            {
                ViewBag.Erorras = "Negalima pasirinkti tu paciu paslaugu.";
            }

            List<SelectListItem> paslaugos = new List<SelectListItem>();
            foreach (var pasl in _context.Paslauga)
            {
                string data = String.Format("{0} {1}eur", pasl.Name, pasl.Price);
                paslaugos.Add(new SelectListItem(data, pasl.Id.ToString()));
            }
            ViewData["Paslaugos"] = paslaugos;

            if (id == null || _context.Route == null)
            {
                return NotFound();
            }

            var route = await _context.Route
                .FirstOrDefaultAsync(m => m.Id == id);

            var routeReservationExists =  _context.Reservation.Include(r => r.Route).Any(x => x.RouteId == id);
            if(routeReservationExists)
            {
                ViewBag.Taken = true;
            }else
            {
                ViewBag.Taken = false;
            }
            //if (route == null || routeReservationExists)
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

            var routePictures = _context.Route.Include(r => r.Pictures).Where(z => z.Id == route.Id).Select(p => p.Pictures).FirstOrDefault();
            ViewBag.Pictures = routePictures.ToList();

            if (add != null)
            {
                if (dataEx.Paslauga.Count > 0)
                    dataEx.Paslauga.Add(_context.Paslauga.Where(x => x.Id == dataEx.Paslauga.Last().Id).FirstOrDefault());
                else
                    dataEx.Paslauga.Add(_context.Paslauga.FirstOrDefault());
                //ViewBag.Erorras negalima tu paciu paslaugu ar pan
                //ModelState.Clear();
                //keep values
                int pc = dataEx.PeopleCount;
                Difficulity dif = dataEx.Difficulity;
                dataEx.Difficulity = dif;
                dataEx.PeopleCount = pc;
                return View(dataEx);
            }



            return View(routeOrderDTO);
        }
        // GET: Routes/DetailsUser/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DetailsUser(int? id, int err)
        {


            if (err == 1)
            {
                ViewBag.Erorras = "Nepasirinktas paveikslelis/blogas formatas";
            }
            if (id == null || _context.Route == null)
            {
                return NotFound();
            }
            var userIdstring = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            int userId = int.Parse(userIdstring);
            var route = await _context.Route.FirstOrDefaultAsync(m => m.Id == id);
            var showUpload = DateTime.Compare(route.Date, DateTime.Today) < 0;
            if (showUpload == true)
            {
                ViewBag.ShowUpload = true;
            }
            else
            {
                ViewBag.ShowUpload = false;
            }
            if (route != null && User.IsInRole("Manager"))
            {
                return View(route);
            }
            var reservation = await _context.Reservation.Include(u => u.User).Include(r => r.Route).Where(z => z.RouteId == id && userId == z.UserId).FirstOrDefaultAsync();
            if (reservation == null || route == null)
            {
                return NotFound();
            }


            return View(_mapper.Map<RouteImageDTO>(route));
        }

        // GET: Routes/Create
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Routes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Date,Length,Difficulity,Description,PricePerPerson,MaxPeople,Lattitude,Longtitude")] Entities.Route route)
        {
            if (ModelState.IsValid)
            {
                _context.Add(route);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexAdmin), new RouteValueDictionary(new
                {
                    Success = true
                }));
            }
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            return View(route);
        }

        // GET: Routes/Edit/5
        [Authorize(Roles = "Manager")]
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
                    _context.Update(route); // Paupdatinu ruote,
                    // Recalculatinu discounta
                    Reservation reservation = _context.Reservation.Include(r => r.Route).Include(u => u.User).Where(x => x.RouteId == id).FirstOrDefault();
                    if(!(reservation == null))
                    {
                        User useris = _context.Reservation.Include(r => r.Route).Include(u => u.User).Where(x => x.RouteId == id).Select(z => z.User).FirstOrDefault();
                        var userReservations = _context.Reservation.Include(r => r.User).Where(r => r.UserId == useris.Id && r.RouteId != route.Id);//excludinu sita itema nes recalculatima
                        var userReservationSum = userReservations.Sum(x => x.Price);
                        //Skull emoji , this is awful why am I not doing this properly 
                        double totalSum = reservation.PersonCount * route.PricePerPerson;
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
                        ViewBag.Update = "Perskaiciuota nuolaida suteiktai rezervacijai pagal atnaujintą maršrutą.";
                        reservation.Price = totalSum - reservation.Discount;
                        _context.Update(reservation);
                    }
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
                return RedirectToAction(nameof(IndexAdmin), new RouteValueDictionary(new { Success = true }));
            }
            return View(route);
        }

        // GET: Routes/Delete/5
        [Authorize(Roles = "Manager")]
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
        [Authorize(Roles = "Manager")]
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
            return RedirectToAction(nameof(IndexAdmin), new RouteValueDictionary(new
            {
                Success = true
            }));
        }
        [HttpPost, ActionName("PassOrder")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PassOrder(RouteOrderDTO order)
        {
            var paslaugoscheck = order.Paslauga;
            var valid = paslaugoscheck.DistinctBy(x => x.Id).Count();
            bool validTooMany = order.PeopleCount <= order.MaxPeople;
            if(valid != paslaugoscheck.Count)
            {
                return RedirectToAction("Details", new RouteValueDictionary(new { id = order.Passingid, errr = 4 }));
            }    
            if (order.Boat == BoatType.Baidare && order.PeopleCount % 2 != 0 || !validTooMany)
            {
                return RedirectToAction("Details", new RouteValueDictionary(new { id = order.Passingid, errr = 1 }));
            }
            else if(order.Boat == BoatType.Kanoja && order.PeopleCount % 4 != 0 || !validTooMany)
            {
                return RedirectToAction("Details", new RouteValueDictionary(new { id = order.Passingid, errr = 2 }));
            }
            else if(order.Boat == BoatType.Valtis && order.PeopleCount % 6 != 0 || !validTooMany)
            {
                return RedirectToAction("Details", new RouteValueDictionary(new { id = order.Passingid, errr = 3 }));
            }

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
            List<Paslauga> nl = new List<Paslauga>();

            foreach (var paslauga in order.Paslauga)
            {
                Paslauga paslaugaObj = _context.Paslauga.Where(r => r.Id == paslauga.Id).FirstOrDefault();
                nl.Add(paslaugaObj);
                reservation.Price += paslaugaObj.Price;
            }
            reservation.Paslauga = nl;
            //Nuolaidų sistema pagal vartotojo užsakymo sumą
            //(nuo 200 lt – 3 %, 250 lt – 5 %, 400 lt – 10 %).
            //Jei bendroje sumoje(per kelis kartus) vartotojas yra užsakęs paslaugų daugiau
            //kaip už 1000 lt papildomai+3 %, 2000 lt + 5 %, už 3000 lt + 10 %)

            reservation.Discount = Math.Round(reservation.Discount, 2);
            reservation.Price = Math.Round(reservation.Price, 2);
            reservation.ReservationCost = Math.Round(reservation.ReservationCost, 2);
            _context.Reservation.Add(reservation);
            _context.SaveChanges();
            return View(reservation);
            //return View("~/Reservations/Create",reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DetailsUser(RouteImageDTO userViewModel)
        {
            if (userViewModel is null)
            {
                throw new ArgumentNullException(nameof(userViewModel));
            }


            var routess = _context.Route.Where(r => r.Id == userViewModel.Id).FirstOrDefault();
            userViewModel.Description = routess.Description;
            userViewModel.Name = routess.Name;
            if (userViewModel.Picture is null)
            {
                return RedirectToAction(nameof(DetailsUser), new RouteValueDictionary(new
                {
                    Id = userViewModel.Id,
                    err = 1
                }));
            }
            var Name = userViewModel.Picture.Name;
            var PictureFormat = userViewModel.Picture.ContentType;


            var memoryStream = new MemoryStream();
            userViewModel.Picture.CopyTo(memoryStream);
            var userPicture = memoryStream.ToArray();

            //_context
            var userIdstring = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            int userId = int.Parse(userIdstring);
            var routes = _context.Route.Include(z => z.Pictures).FirstOrDefault(r => r.Id == userViewModel.Id);
            if(routes != null)
            {
                    Picture picturez = new Picture();
                    picturez.PictureBytes = userPicture;
                    picturez.PictureFormat = userViewModel.Picture.ContentType;
                    routes.Pictures.Add(picturez);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        private bool RouteExists(int id)
        {
            return _context.Route.Any(e => e.Id == id);
        }
    }
}
