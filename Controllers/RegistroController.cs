using Microsoft.AspNetCore.Mvc;
using YachayTech_p_cov.Data;
using YachayTech_p_cov.Models;

namespace YachayTech_p_cov.Controllers
{
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
            return View();
        }

        // Procesa los datos cuando el usuario hace clic en "Enviar"
        [HttpPost]
        public IActionResult Index(Usuario usuario)
        {
            // 1. Filtro de Dominio Institucional (HU2 Criterio de Aceptación)
            if (string.IsNullOrEmpty(usuario.Correo) || !usuario.Correo.Trim().EndsWith("@cibertec.edu.pe"))
            {
                // Si falla, devolvemos a la vista con un mensaje de error
                ViewBag.Error = "Acceso denegado: Debe registrarse con un correo institucional válido (@cibertec.edu.pe).";
                return View(usuario); // Le pasamos el usuario para que no tenga que volver a escribir su nombre
            }

            // 2. Persistencia en Base de Datos
            usuario.FechaRegistro = DateTime.Now;
            _context.Usuarios.Add(usuario);
            _context.SaveChanges(); // Aquí se genera el Usuario.Id automáticamente

            // 3. El puente estratégico para el Tracking: Guardamos quién es en la sesión
            HttpContext.Session.SetInt32("UsuarioActivoId", usuario.Id);

            // 4. Puertas abiertas: Redirigimos al primer paso del Test
            return RedirectToAction("Index", "Home");
        }
    }
}
