using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class RegistroController : Controller
    {
        private readonly EvaluacionContext _context;

        public RegistroController(EvaluacionContext context)
        {
            _context = context;
        }

        // Muestra el formulario vacío
        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true) return RedirectToAction("Index", "Home");
            return View(new RegisterViewModel());
        }

        // Procesa los datos cuando el usuario hace clic en "Enviar"
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RegisterViewModel model)
        {
            // 1. Filtro de Dominio Institucional (HU2 Criterio de Aceptación)
            if (!ModelState.IsValid) return View(model);

            var correo = model.Correo.Trim().ToLower();
            if (!correo.EndsWith("@cibertec.edu.pe"))
            {
                ModelState.AddModelError(nameof(model.Correo), "Debe registrarse con un correo institucional válido (@cibertec.edu.pe).");
                return View(model);
            }

            // 2. Persistencia en Base de Datos
            if (await _context.Usuarios.AnyAsync(u => u.Correo == correo))
            {
                ModelState.AddModelError(nameof(model.Correo), "El correo ya está registrado. Inicia sesión.");
                return View(model);
            }

            // 3. El puente estratégico para el Tracking: Guardamos quién es en la sesión
            var (hash, salt) = ContraseñasHasheadas.HashPassword(model.Password);
            var usuario = new Usuario
            {
                Nombre = model.Nombre.Trim(),
                Apellido = model.Apellido.Trim(),
                Correo = correo,
                Rol = "Usuario",
                PasswordHash = hash,
                PasswordSalt = salt,
                FechaRegistro = DateTime.UtcNow
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            await IinciarSesionUsuarioAsync(usuario);

            // 4. Puertas abiertas: Redirigimos al primer paso del Test
            return RedirectToAction("Index", "Home");
        }

        private async Task IinciarSesionUsuarioAsync(Usuario usuario)
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

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            HttpContext.Session.SetInt32("UsuarioActivoId", usuario.Id);
        }
    }
}
