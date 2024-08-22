using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Administrador
{
    public class UpdateDatosPersonalesDTO
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int Dni { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string? Calle { get; set; }
        public int Altura { get; set; }
    }
}
