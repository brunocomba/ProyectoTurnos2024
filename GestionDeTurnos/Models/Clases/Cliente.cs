
using System.Numerics;

namespace Models.Clases
{
    public class Cliente 
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int Dni { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string? Calle { get; set; }
        public int? Altura { get; set; }
        public uint Telefono {  get; set; }

        //public Cliente(string nombre, string apellido, int dni, DateTime FechaNacimiento, string? calle, int altura, uint tel)
        //{
        //    Nombre = nombre; Apellido = apellido; Dni = dni; fechaNacimiento = FechaNacimiento; Calle = calle; Altura = altura; Telefono = tel;
        //}

    }
}
