using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Domain.User;
using SimpleECommerce.Models.Context;
using SimpleECommerce.Models.User;
using System.Security.Claims;

namespace SimpleECommerce.Controllers.Auth
{
    public class AuthController : Controller
    {
        private ECommerceDbContext context;

        public AuthController(ECommerceDbContext context)
        {
            this.context = context;
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

            UserModel? user = await context.Users
                .Include(u => u.Roles)
                .ThenInclude(ur => ur.Role)
                .ThenInclude(ur => ur.Permissions)
                .ThenInclude(rp => rp.Permission)
                .SingleOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "指定されたアドレスは登録されていません");
                return View();
            }

            PasswordHasher<UserModel> hasher = new PasswordHasher<UserModel>();
            PasswordVerificationResult result = hasher.VerifyHashedPassword(user, user.Password, request.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "ユーザー名、またはパスワードが違います");
                return View();
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (string role in user.Roles.Select(r => r.Role.Name))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            IEnumerable<string> permissions = user.Roles.SelectMany(ur => ur.Role.Permissions).Select(rp => rp.Permission.Code).Distinct();
            foreach (string permission in permissions)
            {
                claims.Add(new Claim("Permission", permission));
            }

            ClaimsIdentity identity = new ClaimsIdentity(claims, "AuthCookie");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            CreateDomainUser(principal);

            await HttpContext.SignInAsync("AuthCookie", principal);

            return RedirectToAction("Index", "Product");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("AuthCookie");
            return RedirectToAction(nameof(Login));
        }

        private void CreateDomainUser(ClaimsPrincipal principal)
        {
            string? role = principal.FindFirst(ClaimTypes.Role)?.Value;
            switch (role) 
            {
                case "Admin":
                    break;

                case "Operator":
                    break;

                case "General":
                    string name = principal.FindFirst(ClaimTypes.Name)?.Value!;
                    string email = principal.FindFirst(ClaimTypes.Email)?.Value!;
                    Customer customer = DomainUserFactory.CreateCustomer(name, email);
                    HttpContext.Items["customer"] = customer;
                    break;

                default:
                    throw new Exception($"Unknow role [role = {role}]");
            }
        }
    }
}
