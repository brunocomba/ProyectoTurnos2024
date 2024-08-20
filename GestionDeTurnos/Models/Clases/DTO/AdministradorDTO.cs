using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Clases.DTO
{
    public class AdministradorDTO
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int Dni { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string? Calle { get; set; }
        public int Altura { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? confirPass { get; set;}
    }
}
