using Microsoft.EntityFrameworkCore;
using YachayTech_p_cov.Models;

namespace YachayTech_p_cov.Data
{
    public class EvaluacionContext : DbContext
    {
        public EvaluacionContext(DbContextOptions<EvaluacionContext> options) : base(options)
        {
        }

        // Estas serán tus tablas reales en SQL Server
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pregunta> Preguntas { get; set; }
        public DbSet<Opcion> Opciones { get; set; }
        public DbSet<ResultadoEvaluacion> Resultados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Sembrar Preguntas
            modelBuilder.Entity<Pregunta>().HasData(
                new Pregunta { Id = 1, TituloEtapa = "Alineación de Perfil Tecnológico", Enunciado = "¿Qué te apasiona más de la tecnología?" },
                new Pregunta { Id = 2, TituloEtapa = "Estrategia de Desarrollo", Enunciado = "¿Qué lenguaje prefieres para Backend?" },
                new Pregunta { Id = 3, TituloEtapa = "Estrategia de Innovación", Enunciado = "¿Cuál es tu herramienta de IA favorita?" },
                new Pregunta { Id = 4, TituloEtapa = "Final", Enunciado = "¡Has completado el Nivel 2!" },
                new Pregunta { Id = 5, TituloEtapa = "Exploración e I+D", Enunciado = "¿Con qué ecosistema de tecnologías emergentes prefieres experimentar para resolver problemas complejos?" }
            );

            // 2. Sembrar Opciones (Asignamos Id único y el PreguntaId correspondiente)
            modelBuilder.Entity<Opcion>().HasData(
                // Opciones Pregunta 1
                new Opcion { Id = 1, PreguntaId = 1, Texto = "Arquitectura y creación de aplicaciones escalables", Puntos = 3, SiguientePreguntaId = 2 },
                new Opcion { Id = 2, PreguntaId = 1, Texto = "Entrenamiento de modelos predictivos y automatización (IA)", Puntos = 3, SiguientePreguntaId = 3 },
                new Opcion { Id = 3, PreguntaId = 1, Texto = "Investigación e integración de tecnologías emergentes (I+D)", Puntos = 3, SiguientePreguntaId = 5 },

                // Opciones Pregunta 2
                new Opcion { Id = 4, PreguntaId = 2, Texto = "C# / .NET", Puntos = 5, SiguientePreguntaId = 4 },
                new Opcion { Id = 5, PreguntaId = 2, Texto = "Java", Puntos = 4, SiguientePreguntaId = 4 },

                // Opciones Pregunta 3
                new Opcion { Id = 6, PreguntaId = 3, Texto = "Python / PyTorch", Puntos = 5, SiguientePreguntaId = 4 },
                new Opcion { Id = 7, PreguntaId = 3, Texto = "Modelos Pre-entrenados", Puntos = 3, SiguientePreguntaId = 4 },

                // Opciones Pregunta 5
                new Opcion { Id = 8, PreguntaId = 5, Texto = "Internet de las Cosas (IoT), Robótica y Hardware", Puntos = 5, SiguientePreguntaId = 4 },
                new Opcion { Id = 9, PreguntaId = 5, Texto = "Blockchain, Web3 y Sistemas Descentralizados", Puntos = 4, SiguientePreguntaId = 4 },
                new Opcion { Id = 10, PreguntaId = 5, Texto = "Computación Cuántica o simulación de nuevos materiales", Puntos = 5, SiguientePreguntaId = 4 }
            );
        }
    }
}

