using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Models.ConnectionDB;

namespace Models
{
    public class DesignTimeDbcontext : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Especifica la cadena de conexión a tu base de datos
            optionsBuilder.UseSqlServer("Server=BR-PC;Database=LaCocaWEB;trusted_connection=true;Encrypt=False;MultipleActiveResultSets=true");

            // Devolver una instancia del contexto con las opciones configuradas
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
