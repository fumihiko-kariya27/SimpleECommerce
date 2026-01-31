using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Domain.User;
using SimpleECommerce.Models.Context;
using SimpleECommerce.Models.User;
using SimpleECommerce.Service.Purchase;
using System.Security.Claims;

namespace SimpleECommerce.Controllers.Auth
{
    public class AuthController : Controller
    {
        private readonly ECommerceDbContext _context;
        private readonly IPurchasePointService _purchaseService;

        public AuthController(ECommerceDbContext context, IPurchasePointService purchaseService)
        {
            _context = context;
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

            UserModel? user = await _context.Users
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

            foreach (int role in user.Roles.Select(r => r.Role.Id))
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            IEnumerable<string> permissions = user.Roles.SelectMany(ur => ur.Role.Permissions).Select(rp => rp.Permission.Code).Distinct();
            foreach (string permission in permissions)
            {
                claims.Add(new Claim("Permission", permission));
            }

            ClaimsIdentity identity = new ClaimsIdentity(claims, "AuthCookie");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            CreateDomainUser(principal);

            CustomerId customerId = new CustomerId(user.Email);
            await _purchaseService.GrantDailyPointAsync(customerId);

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
            DomainUserRole userRole = DomainUserRole.Unknown;
            if (role != null && Enum.IsDefined(typeof(DomainUserRole), Int32.Parse(role))) 
            {
                userRole = (DomainUserRole)Int32.Parse(role);
            }

            string name = principal.FindFirst(ClaimTypes.Name)?.Value!;
            string email = principal.FindFirst(ClaimTypes.Email)?.Value!;
            IDomainUser user = DomainUserFactory.CreateUserByRole(userRole, name, email);
            HttpContext.Items["user"] = user;
        }
    }
}
