namespace YachayTech_p_cov.Models
{
    public class Opcion
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public int Puntos { get; set; }
        public int SiguientePreguntaId { get; set; } // La lógica de salto se mantiene

        // Llave foránea hacia la Pregunta
        public int PreguntaId { get; set; }
        public Pregunta Pregunta { get; set; }
    }
}
