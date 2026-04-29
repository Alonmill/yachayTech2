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
            modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Correo)
            .IsUnique();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Rol)
                .HasDefaultValue("Usuario");


            modelBuilder.Entity<Pregunta>().HasData(
                new Pregunta { Id = 1, TituloEtapa = "Intereses generales", Enunciado = "¿Qué actividad disfrutas más en un proyecto tecnológico?" },
                new Pregunta { Id = 2, TituloEtapa = "Lógica y resolución", Enunciado = "Cuando enfrentas un problema difícil, ¿qué haces primero?" },
                new Pregunta { Id = 3, TituloEtapa = "Trabajo en equipo", Enunciado = "¿Qué rol te gusta asumir cuando trabajas con otras personas?" },
                new Pregunta { Id = 4, TituloEtapa = "Creatividad", Enunciado = "¿Qué tan importante es para ti diseñar experiencias visuales o de usuario?" },
                new Pregunta { Id = 5, TituloEtapa = "Datos", Enunciado = "¿Te interesa analizar datos para encontrar patrones?" },
                new Pregunta { Id = 6, TituloEtapa = "Seguridad", Enunciado = "¿Qué tanto te atrae detectar vulnerabilidades y proteger sistemas?" },
                new Pregunta { Id = 7, TituloEtapa = "Automatización", Enunciado = "¿Qué opinas sobre automatizar tareas repetitivas con scripts o IA?" },
                new Pregunta { Id = 8, TituloEtapa = "Infraestructura", Enunciado = "¿Te gustaría administrar servidores, redes o servicios en la nube?" },
                new Pregunta { Id = 9, TituloEtapa = "Innovación", Enunciado = "¿Qué tan cómodo te sientes probando tecnologías nuevas sin mucha guía?" },
                new Pregunta { Id = 10, TituloEtapa = "Orientación profesional", Enunciado = "¿Cuál de estas áreas te imaginas estudiando como carrera?" }
            );
           modelBuilder.Entity<Opcion>().HasData(
                new Opcion { Id = 1, PreguntaId = 1, Texto = "Programar soluciones y aplicaciones", Puntos = 5, SiguientePreguntaId = 2 },
                new Opcion { Id = 2, PreguntaId = 1, Texto = "Diseñar interfaces y experiencia de usuario", Puntos = 3, SiguientePreguntaId = 2 },
                new Opcion { Id = 3, PreguntaId = 1, Texto = "Investigar tecnologías y proponer mejoras", Puntos = 4, SiguientePreguntaId = 2 },

                new Opcion { Id = 4, PreguntaId = 2, Texto = "Dividir el problema en partes pequeñas", Puntos = 5, SiguientePreguntaId = 3 },
                new Opcion { Id = 5, PreguntaId = 2, Texto = "Buscar ejemplos y adaptarlos", Puntos = 3, SiguientePreguntaId = 3 },
                new Opcion { Id = 6, PreguntaId = 2, Texto = "Probar varias ideas hasta encontrar una", Puntos = 4, SiguientePreguntaId = 3 },

                new Opcion { Id = 7, PreguntaId = 3, Texto = "Liderar la planificación", Puntos = 4, SiguientePreguntaId = 4 },
                new Opcion { Id = 8, PreguntaId = 3, Texto = "Implementar la parte técnica", Puntos = 5, SiguientePreguntaId = 4 },
                new Opcion { Id = 9, PreguntaId = 3, Texto = "Documentar y coordinar tareas", Puntos = 3, SiguientePreguntaId = 4 },

                new Opcion { Id = 10, PreguntaId = 4, Texto = "Muy importante, me encanta crear interfaces", Puntos = 5, SiguientePreguntaId = 5 },
                new Opcion { Id = 11, PreguntaId = 4, Texto = "Importante, pero no es mi prioridad", Puntos = 3, SiguientePreguntaId = 5 },
                new Opcion { Id = 12, PreguntaId = 4, Texto = "Prefiero la lógica interna del sistema", Puntos = 2, SiguientePreguntaId = 5 },

                new Opcion { Id = 13, PreguntaId = 5, Texto = "Sí, me fascina trabajar con datos", Puntos = 5, SiguientePreguntaId = 6 },
                new Opcion { Id = 14, PreguntaId = 5, Texto = "Me interesa de forma moderada", Puntos = 3, SiguientePreguntaId = 6 },
                new Opcion { Id = 15, PreguntaId = 5, Texto = "No mucho, prefiero otras áreas", Puntos = 1, SiguientePreguntaId = 6 },

                new Opcion { Id = 16, PreguntaId = 6, Texto = "Me interesa bastante la ciberseguridad", Puntos = 5, SiguientePreguntaId = 7 },
                new Opcion { Id = 17, PreguntaId = 6, Texto = "Algo, como conocimiento complementario", Puntos = 3, SiguientePreguntaId = 7 },
                new Opcion { Id = 18, PreguntaId = 6, Texto = "No es un área que me llame", Puntos = 1, SiguientePreguntaId = 7 },

                new Opcion { Id = 19, PreguntaId = 7, Texto = "La automatización me motiva muchísimo", Puntos = 5, SiguientePreguntaId = 8 },
                new Opcion { Id = 20, PreguntaId = 7, Texto = "La uso cuando es necesaria", Puntos = 3, SiguientePreguntaId = 8 },
                new Opcion { Id = 21, PreguntaId = 7, Texto = "Prefiero procesos manuales", Puntos = 1, SiguientePreguntaId = 8 },

                new Opcion { Id = 22, PreguntaId = 8, Texto = "Sí, me gusta la nube y la infraestructura", Puntos = 5, SiguientePreguntaId = 9 },
                new Opcion { Id = 23, PreguntaId = 8, Texto = "Me interesa aprender lo básico", Puntos = 3, SiguientePreguntaId = 9 },
                new Opcion { Id = 24, PreguntaId = 8, Texto = "No, prefiero desarrollo de software", Puntos = 2, SiguientePreguntaId = 9 },

                new Opcion { Id = 25, PreguntaId = 9, Texto = "Muy cómodo, disfruto experimentar", Puntos = 5, SiguientePreguntaId = 10 },
                new Opcion { Id = 26, PreguntaId = 9, Texto = "Depende de la complejidad", Puntos = 3, SiguientePreguntaId = 10 },
                new Opcion { Id = 27, PreguntaId = 9, Texto = "Prefiero tecnologías ya consolidadas", Puntos = 2, SiguientePreguntaId = 10 },

                new Opcion { Id = 28, PreguntaId = 10, Texto = "Ingeniería en Inteligencia Artificial", Puntos = 5, SiguientePreguntaId = 0 },
                new Opcion { Id = 29, PreguntaId = 10, Texto = "Ingeniería de Software", Puntos = 4, SiguientePreguntaId = 0 },
                new Opcion { Id = 30, PreguntaId = 10, Texto = "Ciberseguridad / Infraestructura", Puntos = 3, SiguientePreguntaId = 0 }
            );
        }
    }
}

