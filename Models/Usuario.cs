namespace YachayTech_p_cov.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; } // Aquí validaremos el dominio institucional luego
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Relación: Un usuario puede tener un resultado (o varios si lo intenta de nuevo)
        public ICollection<ResultadoEvaluacion> Resultados { get; set; }
    }
}
