using Models.DTOs.Deporte;


namespace Models.DTOs.Cancha
{
    public class CanchaDTO
    {
        public DeporteDTO Deporte { get; set; }
        public string Name { get; set; }
        public decimal Precio { get; set; }

    }
}
