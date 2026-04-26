using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using YachayTech_p_cov.Data;
using YachayTech_p_cov.Models;
using YachayTech_p_cov.Seguridad;
using YachayTech_p_cov.ViewModel;

namespace YachayTech_p_cov.Controllers
{
    [AllowAnonymous]
    public class CuentaController : Controller
    {
        private readonly EvaluacionContext _context;

        public CuentaController(EvaluacionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true) return RedirectToAction("Index", "Home");
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == model.Correo.Trim().ToLower());
            if (usuario == null || !ContraseñasHasheadas.VerifyPassword(model.Password, usuario.PasswordHash, usuario.PasswordSalt))
            {
                ModelState.AddModelError(string.Empty, "Credenciales inválidas.");
                return View(model);
            }

            await SignInUserAsync(usuario);
            return usuario.Rol == "Admin" ? RedirectToAction("Index", "Admin") : RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("UsuarioActivoId");
            HttpContext.Session.Remove("Score");
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied() => View();

        private async Task SignInUserAsync(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8) });

            HttpContext.Session.SetInt32("UsuarioActivoId", usuario.Id);
        }
    }
}
