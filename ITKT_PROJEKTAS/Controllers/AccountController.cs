using ITKT_PROJEKTAS.Entities;
using ITKT_PROJEKTAS.Models;
using ITKT_PROJEKTAS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITKT_PROJEKTAS.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IUserRepository _userRepository;

        public AccountController(IUserManager userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View(this.User.Claims.ToDictionary(x => x.Type, x => x.Value));
        }


        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _userRepository.Authenticate(model);

            if (user == null)
            {
                ViewBag.Erorras = "Vartotojas nerastas/blogas slaptazodis";
                return View(model);
            }

            await _userManager.SignIn(this.HttpContext, user, false);

            return LocalRedirect("~/");
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _userRepository.Register(model);
            if(user == null)
            {
                ViewBag.Erorras = "Vartotojas jau egzistuoja/neteisingi duomenys";
                return View(model);
            }
            await _userManager.SignIn(this.HttpContext, user, false);

            return LocalRedirect("~/Account/Profile");
        }

        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await _userManager.SignOut(this.HttpContext);
            return RedirectPermanent("~/Home/Index");
        }
    }
}
