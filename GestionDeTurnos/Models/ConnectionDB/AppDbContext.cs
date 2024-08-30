using Microsoft.EntityFrameworkCore;
using Models.Clases;

namespace Models.ConnectionDB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }

        private static AppDbContext? instance;
        public static AppDbContext Instancia
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppDbContext();
                }
                return instance;
            }
        }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cancha> Canchas { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Elemento> Elementos { get; set; }
        public DbSet<ElementosCancha> ElementosCancha { get; set; }
        public DbSet<Deporte> Deportes { get; set; }

        private string Entorno { get; set; } = "Produccion";

        public void setEntorno(string entorno)
        {
            Entorno = entorno.ToUpper();
        }

        private string definirEntorno()
        {
            if (Entorno == "TEST" || Entorno == "TESTING")
            {
                return "LaCocaWebTesting";
            }
            return "LaCocaWEB";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var entorno = definirEntorno(); 
            optionsBuilder.UseSqlServer($"server = LAPTOP-64KVEN22;database=LaCocaWEB;trusted_connection=true;Encrypt=False");
        }
    }
}
