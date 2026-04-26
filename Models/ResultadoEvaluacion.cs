namespace YachayTech_p_cov.Models
{
    public class ResultadoEvaluacion
    {
        public int Id { get; set; }
        public int PuntajeTotal { get; set; }
        public string CategoriaAsignada { get; set; } // Ej: "IA", "Desarrollo", "Innovación"
        public DateTime FechaPrueba { get; set; } = DateTime.Now;

        // Llave foránea hacia el Usuario
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
