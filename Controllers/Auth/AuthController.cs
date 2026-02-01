using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Domain.User;
using SimpleECommerce.Models.Context;
using SimpleECommerce.Models.User;
using SimpleECommerce.Service.Purchase;
using SimpleECommerce.Service.User;
using System.Diagnostics;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimpleECommerce.Controllers.Auth
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPurchasePointService _purchaseService;

        public AuthController(IUserService userService, IPurchasePointService purchaseService)
        {
            _userService = userService;
            _purchaseService = purchaseService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            DomainUser? user = await _userService.FindByEmailAsync(request.Email);
            string password = await _userService.GetHashedPasswordAsync(request.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "指定されたアドレスは登録されていません");
                return View();
            }

            PasswordHasher<DomainUser> hasher = new PasswordHasher<DomainUser>();
            PasswordVerificationResult result = hasher.VerifyHashedPassword(user, password, request.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "ユーザー名、またはパスワードが違います");
                return View();
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Profile.Name),
                new Claim(ClaimTypes.Email, user.Profile.Email)
            };

            foreach (int role in user.Role.Roles.Select(r => r.Id))
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            IEnumerable<string> permissions = user.Role.Roles.SelectMany(ur => ur.Permissions).Select(rp => rp.Behavior).Distinct();
            foreach (string permission in permissions)
            {
                claims.Add(new Claim("Permission", permission));
            }

            ClaimsIdentity identity = new ClaimsIdentity(claims, "AuthCookie");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            HttpContext.Items["user"] = user;

            CustomerId customerId = new CustomerId(user.Id);
            await _purchaseService.GrantDailyPointAsync(customerId);

            await HttpContext.SignInAsync("AuthCookie", principal);

            return RedirectToAction("Index", "Product");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("AuthCookie");
            return RedirectToAction(nameof(Login));
        }
    }
}
