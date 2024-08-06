using Models.Clases;
using Models.ConnectionDB;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.Intrinsics.Arm;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;

namespace Models.Managers
{
    public class ClienteMG
    {
        private ClienteMG() { }

        private static ClienteMG? instance;
        public static ClienteMG Instancia
        {
            get
            {
                if (instance == null)
                {
                    instance = new ClienteMG();
                }
                return instance;
            }
        }


        private readonly AppDbContext _context = AppDbContext.Instancia;

        private static readonly Validaciones _v = new Validaciones();

        
        public Cliente Buscar(int dni)
        {
            if (dni == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var cli = _context.Clientes.FirstOrDefault(a => a.Dni == dni);

            if (cli == null)
            {
                throw new Exception($"No se encontro un cliente registrado con el DNI: {dni}");
            }
            return cli;
        }
        public List<Cliente> Listado()
        {
            return _context.Clientes.ToList();
        }
     

        public string Add(string name, string apellido, int dni, DateTime fecha, string calle, int alt, uint tel)
        {
            if (name == null || apellido == null || dni == null || fecha == null || calle == null || alt == null || tel == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var cliExist = _context.Clientes.FirstOrDefault(c => c.Dni == dni);

            if (cliExist != null)
            {
                throw new Exception($"Ya existe un cliente registrado con el DNI: {dni}");
            }

            if (_v.SoloLetras(name) == false || _v.SoloLetras(calle) == false || _v.SoloLetras(apellido) == false)
            {
                throw new Exception($"El nombre, apellido y/o la calle no puede contener numeros o caracteres especiales.");
            }
                                                                                        /// convertir a int para poder hacer la verificacion
            if (_v.SoloNumeros(dni) == false || _v.SoloNumeros(alt) == false || _v.SoloNumerosEnTel(tel) == false)
            {
                throw new Exception($"El DNI, telefono y/o la altura no puede contener letras.");
            }

            if (_v.DniCompleto(dni) == false)
            {
                throw new Exception($"El DNI ingresado esta incompleto.");
            }

            if (_v.TelCompleto(tel) == false)
            {
                throw new Exception($"El telefono ingresado esta incompleto.");
            }

            if (_v.Mayor18(fecha) == false)
            {
                throw new Exception("El usuario ingresado es menor de 18 años");

            }

            Cliente cli = new Cliente();
            {
                cli.Nombre = name;
                cli.Apellido = apellido;
                cli.Dni = dni;
                cli.fechaNacimiento = fecha;
                cli.Calle = calle;
                cli.Altura = alt;
                cli.Telefono = tel;
;
            }
            _context.Clientes.AddAsync(cli);
            _context.SaveChangesAsync();

            return $"Cliente {cli.Nombre} {cli.Apellido} creado con exito";
        }
        public string UpdateNombres(int dniCliMod, string name, string apellido)
        {
            if (dniCliMod == null || name == null || apellido == null )
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var cliMod = Buscar(dniCliMod);

            if (cliMod == null)
            {
                throw new Exception($"No se ha encontrado ningun cliente registrado con el DNI: {dniCliMod}");
            }

            if (_v.SoloLetras(name) == false || _v.SoloLetras(apellido) == false)
            {
                throw new Exception($"El nombre y/o el apellido no puede contener numeros o caracteres especiales.");
            }

            /// modificar el objeto
            cliMod.Nombre = name;
            cliMod.Apellido = apellido;

            _context.Clientes.Update(cliMod);
            _context.SaveChanges();

            return $"Modificacion realizada con exito";
        }
        public string UpdateDNI(int dniCliMod, int dni)
        {
            if (dniCliMod == null || dni == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var cliMod = Buscar(dniCliMod);

            if (cliMod == null)
            {
                throw new Exception($"No se ha encontrado ningun cliente registrado con el DNI: {dniCliMod}");
            }

            if (Buscar(dni) != null)
            {
                throw new Exception($"Ya existe un cliente registrado con el DNI: {dni}");
            }

            if (_v.DniCompleto(dni) == false)
            {
                throw new Exception($"El DNI ingresado esta incompleto.");
            }
            if (_v.SoloNumeros(dni) == false)
            {
                throw new Exception("El DNI no puede contener letras.");
            }

            /// modificar el objeto
            cliMod.Dni = dni;

            _context.Clientes.Update(cliMod);
            _context.SaveChanges();

            return $"Modificacion realizada con exito";
        }


        public string UpdateDireccion(int dniCliMod, string calle, int altura)
        {
            if (dniCliMod == null || calle == null || altura == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var cliMod = Buscar(dniCliMod);

            if (cliMod == null)
            {
                throw new Exception($"No se ha encontrado ningun cliente registrado con el DNI: {dniCliMod}");
            }

            if (_v.SoloLetras(calle) == false)
            {
                throw new Exception($"La calle no puede contener numeros o caracteres especiales.");
            }

            if (_v.SoloNumeros(altura) == false)
            {
                throw new Exception($"La altura no puede contener letras.");
            }

            /// modificar el objeto
            cliMod.Calle = calle;
            cliMod.Altura = altura;

            _context.Clientes.Update(cliMod);
            _context.SaveChanges();

            return $"Modificacion realizada con exito";
        }


        public string UpdateFechaNacimiento(int dniCliMod, DateTime fecha)
        {
            if (dniCliMod == null || fecha == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var cliMod = Buscar(dniCliMod);

            if (cliMod == null)
            {
                throw new Exception($"No se ha encontrado ningun cliente registrado con el DNI: {dniCliMod}");
            }

            if (_v.Mayor18(fecha) == false)
            {
                throw new Exception("Es necesario ser mayor de 18 años");
            }

            /// modificar el objeto
            cliMod.fechaNacimiento = fecha;

            _context.Clientes.Update(cliMod);
            _context.SaveChanges();

            return $"Modificacion realizada con exito";
        }

        public string UpdateTelefono(int dniCliMod, uint tel)
        {
            if (dniCliMod == null || tel == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var cliMod = Buscar(dniCliMod);

            if (cliMod == null)
            {
                throw new Exception($"No se ha encontrado ningun cliente registrado con el DNI: {dniCliMod}");
            }

            if (_v.TelCompleto(tel) == false)
            {
                throw new Exception($"El telefono ingresado esta incompleto.");
            }

            if (_v.SoloNumerosEnTel(tel) == false)
            {
                throw new Exception("El telefono no puede contener letras.");
            }

            /// modificar el objeto
            cliMod.Telefono = tel;

            _context.Clientes.Update(cliMod);
            _context.SaveChanges();

            return $"Modificacion realizada con exito";
        }

        public string Delete(int dni)
        {
            var cli = Buscar(dni);

            if (cli == null)
            {
                throw new Exception($"No se ha encontrado ningun cliente registrado con el DNI: {dni}");

            }

            _context.Clientes.Remove(cli);
            _context.SaveChanges();

            return $"Se ha eliminado el cliente con DNI {dni}";
        }

    }
}
