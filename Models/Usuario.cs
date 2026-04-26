using System.ComponentModel.DataAnnotations;

namespace YachayTech_p_cov.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(120)]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(180)]
        public string Correo { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Rol { get; set; } = "Usuario";

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string PasswordSalt { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Relación: Un usuario puede tener un resultado (o varios si lo intenta de nuevo)
        public ICollection<ResultadoEvaluacion> Resultados { get; set; } = new List<ResultadoEvaluacion>();
    }
}
