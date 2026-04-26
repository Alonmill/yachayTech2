using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using YachayTech_p_cov.Models;
using YachayTech_p_cov.Data;

namespace YachayTech_p_cov.Controllers
{
    [Authorize(Roles = "Usuario,Admin")]
    public class HomeController : Controller
    {
        private readonly EvaluacionContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(EvaluacionContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index(int id = 1)
        {
            var pregunta = _context.Preguntas
                                   .Include(p => p.Opciones)
                                   .FirstOrDefault(p => p.Id == id);

            if (pregunta == null || !pregunta.Opciones.Any()) return RedirectToAction("Resultado");

            return View(pregunta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Siguiente(string respuestaCombinada)
        {
            if (string.IsNullOrEmpty(respuestaCombinada)) return RedirectToAction("Index");

            var partes = respuestaCombinada.Split('|');
            int puntosSeleccionados = int.Parse(partes[0]);
            int siguienteId = int.Parse(partes[1]);

            // Lógica de sesión
            int puntajeActual = HttpContext.Session.GetInt32("Score") ?? 0;
            HttpContext.Session.SetInt32("Score", puntajeActual + puntosSeleccionados);

            // Si el siguienteId es un marcador de final (ej: 0 o 99), ir a resultados
            if (siguienteId == 4)
            {
                return RedirectToAction("Resultado");
            }

            return RedirectToAction("Index", new { id = siguienteId });
        }

        public IActionResult Resultado()
        {
            // 1. Capturamos el puntaje exacto y el ID del usuario que está haciendo el test
            int puntajeFinal = HttpContext.Session.GetInt32("Score") ?? 0;
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioActivoId");

            // 2. Si hay un usuario logueado, guardamos su resultado en la Base de Datos
            if (usuarioId.HasValue)
            {
                // Lógica temporal de clasificación (se refinará en el Sprint 3 cuando separes puntajes por rama)
                string categoria = puntajeFinal >= 12 ? "Especialista (Desarrollo/IA)" :
                                   puntajeFinal >= 8 ? "Innovador (I+D)" :
                                   "Explorador Tecnológico";

                var nuevoResultado = new ResultadoEvaluacion
                {
                    UsuarioId = usuarioId.Value,
                    PuntajeTotal = puntajeFinal,
                    CategoriaAsignada = categoria,
                    FechaPrueba = DateTime.UtcNow
                };

                _context.Resultados.Add(nuevoResultado);
                _context.SaveChanges();
            }

            // 3. Enviamos los datos a la vista
            ViewBag.PuntajeTotal = puntajeFinal;

            // 4. Limpiamos SOLO el puntaje para que pueda hacer el test de nuevo si quiere, 
            // pero NO borramos el UsuarioActivoId, para no obligarlo a registrarse otra vez.
            HttpContext.Session.Remove("Score");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
