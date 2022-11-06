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

            if (user == null) return View(model);

            await _userManager.SignIn(this.HttpContext, user, false);

            if(User.IsInRole(Role.Manager.ToString()))
            {
                return LocalRedirect("~/Manager");
            }
            if (User.IsInRole(Role.Admin.ToString()))
            {
                return LocalRedirect("~/Admin");
            }
            return LocalRedirect("~/Account/Profile");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _userRepository.Register(model);

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
