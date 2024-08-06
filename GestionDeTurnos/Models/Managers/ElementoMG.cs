using Models.Clases;
using Models.ConnectionDB;
using System.Runtime.Intrinsics.Arm;

namespace Models.Managers
{
    public class ElementoMG
    {
        private ElementoMG() { }

        private static ElementoMG? instance;
        public static ElementoMG Instancia
        {
            get
            {
                if (instance == null)
                {
                    instance = new ElementoMG();
                }
                return instance;
            }
        }


        private readonly AppDbContext _context = AppDbContext.Instancia;

        private static readonly Validaciones _v = new Validaciones();

      

        public Elemento Buscar(string nombre)
        {
            if (nombre == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var nombreMAY = nombre.ToUpper();
            var element = _context.Elementos.FirstOrDefault(e => e.Nombre == nombreMAY);
            if (element == null)
            {
                throw new Exception($"No se encontro un elemento registrado con el nombre: {nombre}");
            }

            return element;
        }

        public List<Elemento> Listado()
        {
            return _context.Elementos.ToList();
        }

        public string Add(string nombre, int stock)
        {
            if (nombre == null || stock == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var elementExist = _context.Elementos.FirstOrDefault(e => e.Nombre == nombre);

            if (elementExist != null)
            {
                throw new Exception($"Ya existe un elemento registrado con el nombre: {nombre}");
            }

            if (_v.SoloNumeros(stock) == false)
            {
                throw new Exception("No puede contener letras el stock. ");
            }

            Elemento element = new Elemento();
            element.Nombre = nombre.ToUpper(); // poner todo en mayuscula
            element.Stock = stock;

            _context.Elementos.AddAsync(element);
            _context.SaveChanges();

            return $"{nombre} agregado con exito";
        }

        public string AddStock(string nombre, int stock)
        {
            if (nombre == null || stock == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }
            var elementExist = Buscar(nombre);

            if (elementExist == null)
            {
                throw new Exception($"No se ha encontrado el elemento {nombre}.");
            }
            
            elementExist.Stock = elementExist.Stock + stock;
            
            _context.Elementos.Update(elementExist);
            _context.SaveChanges();

            return $"Stock actualizado.\n--{elementExist.Stock}";

        }

        public string RestarStock(string nombre, int stock)
        {
            if (nombre == null || stock == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var elementExist = Buscar(nombre);

            if (elementExist == null)
            {
                throw new Exception($"No se ha encontrado el elemento {nombre}.");
            }

            elementExist.Stock = elementExist.Stock - stock;

            _context.Elementos.Update(elementExist);
            _context.SaveChanges();

            return $"Stock actualizado.\n--{elementExist.Stock}";
        }


        public string UpdateNombre(string nameElementMod, string nombre)
        {
            if (nameElementMod == null || nombre == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var elementoMod = Buscar(nameElementMod);

            if (elementoMod == null)
            {
                throw new Exception($"No se ha encontrado ningun elemento registrado con el nombre: {nameElementMod}");
            }

            if (elementoMod.Nombre != nombre) /// el nombre del dep se va a modificar, si pasa eso que verifique si ya existe uno registrado igual al parametro mod 
            {
                if (Buscar(nombre) != null)
                {
                    throw new Exception($"Ya existe un elemento registrado con el nombre: {nombre}");
                }
            }

            /// modificar el objeto
            elementoMod.Nombre = nombre.ToUpper();

            _context.Elementos.Update(elementoMod);
            _context.SaveChanges();

            return $"Modificacion realizada con exito";
        }


        public string Delete(string nombre)
        {
            var element = Buscar(nombre);

            if (element == null)
            {
                throw new Exception($"No se ha encontrado ningun elemento registrado con el nombre: {nombre}");

            }

            _context.Elementos.Remove(element);
            _context.SaveChanges();

            return $"Se ha eliminado el elemento {nombre}";
        }
    }
}
