using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YachayTech_p_cov.Data;

namespace YachayTech_p_cov.Controllers
{
    public class AdminController : Controller
    {
        private readonly EvaluacionContext _context;

        public AdminController(EvaluacionContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // Traemos todos los resultados, incluyendo quién los hizo, ordenados por fecha (los más nuevos primero)
            var trackingData = _context.Resultados
                                       .Include(r => r.Usuario)
                                       .OrderByDescending(r => r.FechaPrueba)
                                       .ToList();

            return View(trackingData);
        }
    }
}
