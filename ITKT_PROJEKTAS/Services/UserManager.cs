using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using ITKT_PROJEKTAS.Entities;

namespace ITKT_PROJEKTAS.Services
{
    public interface IUserManager
    {
        Task SignIn(HttpContext httpContext, User user, bool isPersistent = false);
        Task SignOut(HttpContext httpContext);
    }

    public class UserManager : IUserManager
    {
        public async Task SignIn(HttpContext httpContext, User user, bool isPersistent = false)
        {
            string authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            // Generate Claims from DbEntity
            var claims = GetUserClaims(user);

            // Add Additional Claims from the Context
            // which might be useful
            // claims.Add(httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name));

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, authenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authProperties = new AuthenticationProperties
            {
            };

            await httpContext.SignInAsync(authenticationScheme, claimsPrincipal, authProperties);
        }

        public async Task SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private List<Claim> GetUserClaims(User user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
            claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));
            return claims;
        }
    }
}
