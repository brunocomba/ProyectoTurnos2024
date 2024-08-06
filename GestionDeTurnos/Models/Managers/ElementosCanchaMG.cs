
using Models.Clases;
using Models.ConnectionDB;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Models.Managers
{
    public class ElementosCanchaMG
    {
        private ElementosCanchaMG() { }

        private static ElementosCanchaMG? instance;
        public static ElementosCanchaMG Instancia
        {
            get
            {
                if (instance == null)
                {
                    instance = new ElementosCanchaMG();
                }
                return instance;
            }
        }

        private readonly AppDbContext _context = AppDbContext.Instancia;

        private static readonly Validaciones _v = new Validaciones();



        public ElementosCancha Buscar(string nombreElemento, string nombreCancha)
        {
            if (nombreElemento == null || nombreCancha == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var elementoMAY = nombreCancha.ToUpper();
            var canchaMAY = nombreCancha.ToUpper();

            var elementCancha = _context.ElementosCancha.FirstOrDefault(ec => ec.Cancha.Name == canchaMAY && ec.Elemento.Nombre == elementoMAY);
            if (elementCancha == null)
            {
                throw new Exception($"No se encontro el elemento {elementoMAY} registrado en la cancha {canchaMAY}.");
            }
            return elementCancha;
        }

        private bool ExisteAsignacion(Elemento elemento, Cancha cancha)
        {
            var busqueda = _context.ElementosCancha.FirstOrDefault(ec => ec.Elemento.Nombre == elemento.Nombre && ec.Cancha.Name == cancha.Name);

            if (busqueda != null)
            {
                return true;
            }
            return false;
        }
         
        private bool StockSuficiente(int stock, int cantidad)
        {
            if (cantidad <= stock)
            {
                return true;
            }
            return false;
        }


        public List<ElementosCancha> Listado()
        {
            return _context.ElementosCancha.ToList();
        }

        public string Add(string nameElemento, string nameCancha, int cantidad)
        {
            if (nameElemento == null || nameCancha == null || cantidad == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var element = ElementoMG.Instancia.Buscar(nameElemento);
            var cancha = CanchaMG.Instancia.Buscar(nameCancha);

            if (ExisteAsignacion(element, cancha) == true)
            {
                throw new Exception($"Ya existe el elemento {element.Nombre} en la cancha {cancha.Name}.");
            }

            if (_v.SoloNumeros(cantidad) == false)
            {
                throw new Exception("No puede contener letras la cantidad. ");
            }

            if (StockSuficiente(element.Stock, cantidad) == false)
            {
                throw new Exception($"No hay stock suficiente del elemento {element.Nombre}.");
            }


            ElementosCancha ec = new ElementosCancha();
            ec.Elemento = element;
            ec.Cancha = cancha;
            ec.Cantidad = cantidad;

            _context.ElementosCancha.Add(ec);

            // bajar la cantidad en el stock
            element.Stock = element.Stock - cantidad;
            _context.Elementos.Update(element);

            _context.SaveChanges();


            return $"{element.Nombre} asignado correctamente a la cancha {cancha.Name}";

        }

        public string AddCantidad(string nameElemento, string nameCancha, int cantidad)
        {
            if (nameElemento == null || nameCancha == null || cantidad == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var element = ElementoMG.Instancia.Buscar(nameElemento);
            var cancha = CanchaMG.Instancia.Buscar(nameCancha);

            if (ExisteAsignacion(element, cancha) == false)
            {
                throw new Exception($"No existe el elemento {element.Nombre} en la cancha {cancha.Name}.");
            }

            if (_v.SoloNumeros(cantidad) == false)
            {
                throw new Exception("No puede contener letras la cantidad. ");
            }

            if (StockSuficiente(element.Stock, cantidad) == false)
            {
                throw new Exception($"No hay stock suficiente del elemento {element.Nombre}.");
            }

            var asig = Buscar(nameElemento, nameCancha);

            // sumar cantidad
            asig.Cantidad = asig.Cantidad + cantidad;
            _context.ElementosCancha.Update(asig);

            // bajar la cantidad en el stock
            element.Stock = element.Stock - cantidad;
            _context.Elementos.Update(element);

            _context.SaveChanges();

            return $"Cantidad actualizada.\n--{asig.Cantidad}\nStock actual elemento: {element.Stock}";

        }

        public string RestarCantidad(string nameElemento, string nameCancha, int cantidad)
        {
            if (nameElemento == null || nameCancha == null || cantidad == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var element = ElementoMG.Instancia.Buscar(nameElemento);
            var cancha = CanchaMG.Instancia.Buscar(nameCancha);

            if (ExisteAsignacion(element, cancha) == false)
            {
                throw new Exception($"No existe el elemento {element.Nombre} en la cancha {cancha.Name}.");
            }

            if (_v.SoloNumeros(cantidad) == false)
            {
                throw new Exception("No puede contener letras la cantidad. ");
            }

          
            var asig = Buscar(nameElemento, nameCancha);

            // restar cantidad
            asig.Cantidad = asig.Cantidad - cantidad;
            _context.ElementosCancha.Update(asig);

            // subir la cantidad en el stock
            element.Stock = element.Stock + cantidad;
            _context.Elementos.Update(element);

            _context.SaveChanges();

            return $"Cantidad actualizada.\n--{asig.Cantidad}\nStock actual elemento: {element.Stock}";
        }


        public string Delete(string nombreElemento, string nombreCancha)
        {
            var asig = Buscar(nombreElemento, nombreCancha);

            _context.ElementosCancha.Remove(asig);
            _context.SaveChanges();

            return $"Se ha eliminado el elemento {nombreElemento} de la cancha {nombreCancha}";
        }
    }
}
