
namespace Models.Clases
{
    public class Elemento
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int Stock { get; set; }

        public override string ToString()
        {
            return $"{Nombre}";
        }
    }
}
