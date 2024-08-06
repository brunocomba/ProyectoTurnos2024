using Microsoft.EntityFrameworkCore;
using Models.Clases;
using Models.ConnectionDB;

namespace Models.Managers
{
    public class DeporteMG
    {
        private DeporteMG() { }

        private static DeporteMG? instance;
        public static DeporteMG Instancia
        {
            get
            {
                if (instance == null)
                {
                    instance = new DeporteMG();
                }
                return instance;
            }
        }


        private readonly AppDbContext _context = AppDbContext.Instancia;

        private static readonly Validaciones _v = new Validaciones();


        public Deporte Buscar(string nombre)
        {
            if (nombre == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var nombreMAY = nombre.ToUpper();
            var dep = _context.Deportes.FirstOrDefault(a => a.Name == nombreMAY);
            if (dep == null)
            {
                throw new Exception($"No se encontro un deporte registrado con el nombre: {nombre}");
            }

            return dep;
        }

        public List<Deporte> Listado()
        {
            return _context.Deportes.ToList();
        }

        public string Add(string nombre, int cantJugadores)
        {
            if (nombre == null || cantJugadores == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var depExist = _context.Deportes.FirstOrDefaultAsync(d => d.Name == nombre);

            if (depExist != null)
            {
                throw new Exception($"Ya existe un deporte registrado con el nombre: {nombre}");

            }

            if (_v.SoloLetras(nombre) == false)
            {
                throw new Exception($"El nombre no puede contener numeros o caracteres especiales.");
            }

            if (_v.SoloNumeros(cantJugadores) == false)
            {
                throw new Exception("No puede contener letras la cantidad de jugadores. ");
            }

            Deporte dep = new Deporte();
            dep.Name = nombre.ToUpper(); // poner todo en mayuscula
            dep.cantJugadores = cantJugadores;

            _context.Deportes.AddAsync(dep);
            _context.SaveChanges();

            return $"{nombre} agregado con exito";
        }


        public string Update(string nameDepMod, string nombre, int cantJugadores)
        {
            if (nameDepMod == null ||nombre == null || cantJugadores == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var depMod = Buscar(nameDepMod);

            if (depMod == null)
            {
                throw new Exception($"No se ha encontrado ningun deporte registrado con el nombre: {nameDepMod}");
            }

            if (depMod.Name != nombre) /// el nombre del dep se va a modificar, si pasa eso que verifique si ya existe uno registrado igual al parametro mod 
            {
                if (Buscar(nombre) != null)
                {
                    throw new Exception($"Ya existe un deporte registrado con el nombre: {nombre}");
                }
            }

            if (_v.SoloLetras(nombre) == false)
            {
                throw new Exception($"El nombre no puede contener numeros o caracteres especiales.");
            }

            if (_v.SoloNumeros(cantJugadores) == false)
            {
                throw new Exception("No puede contener letras la cantidad de jugadores. ");
            }

            /// modificar el objeto
            depMod.Name = nombre.ToUpper();
            depMod.cantJugadores = cantJugadores;
          
            _context.Deportes.Update(depMod);
            _context.SaveChanges();

            return $"Modificacion realizada con exito";
        }


        public string Delete(string nombre)
        {
            var dep = Buscar(nombre);

            if (dep == null)
            {
                throw new Exception($"No se ha encontrado ningun deporte registrado con el nombre: {nombre}");

            }

            _context.Deportes.Remove(dep);
            _context.SaveChanges();

            return $"Se ha eliminado el deporte {nombre}";
        }

    }
}
