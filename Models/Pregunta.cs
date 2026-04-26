namespace YachayTech_p_cov.Models
{
    public class Pregunta
    {
        public int Id { get; set; }
        public string TituloEtapa { get; set; }
        public string Enunciado { get; set; }

        // Relación: Una pregunta tiene muchas opciones
        public ICollection<Opcion> Opciones { get; set; } = new List<Opcion>();
    }
}
