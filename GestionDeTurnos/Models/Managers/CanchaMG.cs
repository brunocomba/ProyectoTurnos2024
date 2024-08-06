using Models.Clases;
using Models.ConnectionDB;
using System.Net;
using System.Runtime.Intrinsics.Arm;

namespace Models.Managers
{
    public class CanchaMG
    {
        private CanchaMG() { }

        private static CanchaMG? instance;
        public static CanchaMG Instancia
        {
            get
            {
                if (instance == null)
                {
                    instance = new CanchaMG();
                }
                return instance;
            }
        }


        private readonly AppDbContext _context = AppDbContext.Instancia;

        private static readonly Validaciones _v = new Validaciones();


        public Cancha Buscar(string nombre)
        {
            if (nombre == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var nombreMAY = nombre.ToUpper();
            var cancha = _context.Canchas.FirstOrDefault(c => c.Name == nombreMAY);

            if (cancha == null)
            {
                throw new Exception($"No se encontro una cancha registrada con el nombre: {nombre}");
            }
            return cancha;
        }
        public List<Cancha> Listado()
        {
            return _context.Canchas.ToList();
        }

        public string Add(string nombreDep, string nombre, decimal precio)
        {
            if (nombreDep == null || nombre == null || precio == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var canchaExist = _context.Canchas.FirstOrDefault(c =>c.Name == nombre); 

            if (canchaExist != null)
            {
                throw new Exception($"Ya existe una cancha registrada con el nombre: {nombre}");
            }

            if (_v.SoloLetras(nombre) == false)
            {
                throw new Exception($"El nombre no puede contener numeros o caracteres especiales.");
            }

            if (_v.SoloNumerosEnPrecio(precio) == false)
            {
                throw new Exception("No puede contener letras el precio. ");
            }

            var dep = DeporteMG.Instancia.Buscar(nombreDep);

            Cancha cancha = new Cancha();
            cancha.Deporte = dep; 
            cancha.Name = nombre.ToUpper(); // poner todo en mayuscula
            cancha.Precio = precio;

            _context.Canchas.AddAsync(cancha);
            _context.SaveChanges();

            return $"Cancha {nombre} agregada con exito";
        }


        public string Update(string nameCanchaMod, string nombreDep, string nombre, decimal precio)
        {
            if (nameCanchaMod == null || nombreDep == null || nombre == null || precio == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var canchaMod = _context.Canchas.FirstOrDefault(c => c.Name == nombre);

            if (canchaMod == null)
            {
                throw new Exception($"No se ha encontrado ninguna cancha registrada con el nombre: {nameCanchaMod}");
            }

            if (canchaMod.Name != nombre) /// el nombre de la cancha se va a modificar, si pasa eso que verifique si ya existe uno registrado igual al parametro mod 
            {
                var nombreIgual = _context.Canchas.FirstOrDefault(c => c.Name == nombre);
                if (nombreIgual != null)
                {
                    throw new Exception($"Ya existe una cancha registrada con el nombre: {nombre}");
                }
            }

            if (_v.SoloLetras(nombre) == false)
            {
                throw new Exception($"El nombre no puede contener numeros o caracteres especiales.");
            }

            if (_v.SoloNumerosEnPrecio(precio) == false)
            {
                throw new Exception("No puede contener letras el precio. ");
            }

            var dep = DeporteMG.Instancia.Buscar(nombreDep);

            /// modificar el objeto
            canchaMod.Deporte = dep;
            canchaMod.Name = nombre.ToUpper();
            canchaMod.Precio = precio;

            _context.Canchas.Update(canchaMod);
            _context.SaveChanges();

            return $"Modificacion realizada con exito";
        }


        public string Delete(string nombre)
        {
            var cancha = Buscar(nombre);

            _context.Canchas.Remove(cancha);
            _context.SaveChanges();

            return $"Se ha eliminado la cancha {nombre}";
        }

    }
}
