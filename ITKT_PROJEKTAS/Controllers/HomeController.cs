using ITKT_PROJEKTAS.Entities;
using ITKT_PROJEKTAS.Models;
using ITKT_PROJEKTAS.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ITKT_PROJEKTAS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(AuthenticateRequest model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            AuthenticateResponse resp = _userService.Authenticate(model);
        //            return RedirectToAction("Index");
        //        }
        //        catch
        //        {
        //            ViewBag.error = "Login failed";
        //            return RedirectToAction("Login");
        //        }


        //    }
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Register(RegisterDto _user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _userService.Register(_user);
        //            return RedirectToAction("Index");
        //        }
        //        catch(Exception ex)
        //        {
        //            ViewBag.error = ex.Message;
        //            return View();
        //        }
        //    }
        //    var errors = ModelState.Values.SelectMany(v => v.Errors);
        //    return View();


        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}