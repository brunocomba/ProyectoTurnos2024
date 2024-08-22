using Models.ConnectionDB;
using Models.Clases;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Models.Managers
{
    public class AdministradorMG 
    {
        private AdministradorMG() { }

        private static AdministradorMG? instance;

        public static AdministradorMG Instancia
        {
            get
            {
                if (instance == null)
                {
                    instance = new AdministradorMG();
                }
                return instance;
            }
        }

        private readonly AppDbContext _context = AppDbContext.Instancia;

        private static readonly Validaciones _v = new Validaciones();


        public Administrador Buscar(int id)
        {
            if (id == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var adm = _context.Administradores.FirstOrDefault(a => a.Id == id);

            if (adm == null)
            {
                throw new Exception($"No se encontro un administrador registrado con el ID: {id}");
            }
            return adm;
        }

        public List<Administrador> Listado()
        {
            return _context.Administradores.ToList();
        }
        private bool ExisteDNI(int dni)
        {
            var adm = _context.Administradores.FirstOrDefault(a => a.Dni == dni);
            if (adm != null)
            {
                return true;
            }
            return false;
        }

        private bool EmailRegistrado(string email)
        {
            var emailRegistrado = _context.Administradores.FirstOrDefault(a => a.Email == email);

            if (emailRegistrado != null)
            {
                return true;
            }
            return false;
        }

        public string Add(string name, string apellido, int dni, DateTime fecha, string calle, int alt, string email, string password, string confirPass)
        {
            if (name == null || apellido == null || dni == null || fecha == null || calle == null || alt == null || email == null || password == null || confirPass == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var admExist = _context.Administradores.FirstOrDefault(a => a.Dni == dni);

            if (admExist != null)
            {
                throw new Exception($"Ya existe un administador registrado con el DNI: {dni}");
            }


            if (EmailRegistrado(email) == true)
            {
                throw new Exception($"Ya existe un administrador registrado con el E-Mail: {email}");
            }

            if (_v.SoloLetras(name) == false || _v.SoloLetras(calle) == false || _v.SoloLetras(apellido) == false)
            {
                throw new Exception($"El nombre, apellido y/o la calle no puede contener numeros o caracteres especiales.");
            }

            if (_v.SoloNumeros(dni) == false || _v.SoloNumeros(alt) == false)
            {
                throw new Exception($"El DNI y/o la altura no puede contener letras.");
            }

            if (_v.DniCompleto(dni) == false)
            {
                throw new Exception($"El DNI ingresado esta incompleto.");
            }

            if (_v.CumpleRequisitosEmail(email) == false)
            {
                throw new Exception("El email ingresado esta en un formato incorrecto.");
            }

            if (_v.CumpleRequisitosPass(password) == false)
            {
                throw new Exception("La contraseña debe contener al menos una mayuscula y un numero.");
            }

            if (_v.ConfirmarPass(password, confirPass) == false)
            {
                throw new Exception("Las contraseñas ingresadas no coinciden.");
            }

            if (_v.Mayor18(fecha) == false)
            {
                throw new Exception("Es necesario ser mayor de 18 años");

            }

            Administrador adm = new Administrador();
            {
                adm.Nombre = name;
                adm.Apellido = apellido;
                adm.Dni = dni;
                adm.fechaNacimiento = fecha.Date;
                adm.Calle = calle;
                adm.Altura = alt;
                adm.Email = email;
                adm.Password = password;
            }
            _context.Administradores.AddAsync(adm);
            _context.SaveChangesAsync();

            return $"Administrador {adm.Nombre} {adm.Apellido} creado con exito";
        }


        public string UpdateDatosPerosnales(int idAdmiMod, string name, string apellido, int dni, DateTime fecha, string calle, int alt)
        {
            if (idAdmiMod.ToString() == null || name == null || apellido == null || dni.ToString() == null || fecha.ToString() == null || calle == null || alt.ToString() == null )
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            if (_v.SoloLetras(name) == false || _v.SoloLetras(apellido) == false || _v.SoloLetras(calle) == false)
            {
                throw new Exception($"El nombre, el apellido y/o calle no puede contener numeros o caracteres especiales.");
            }

            if (_v.SoloNumeros(alt) == false || _v.SoloNumeros(dni) == false)
            {
                throw new Exception($"La altura y/o el DNI no puede contener letras.");
            }

            if (_v.DniCompleto(dni) == false)
            {
                throw new Exception($"El DNI ingresado esta incompleto.");
            }

            if (ExisteDNI(dni) == true)
            {
                throw new Exception($"Ya existe un administrador registrado con el DNI: {dni}");
            }

            if (_v.Mayor18(fecha) == false)
            {
                throw new Exception("Es necesario ser mayor de 18 años");
            }


            var admiMod = _context.Administradores.FirstOrDefault(a => a.Id == idAdmiMod);
            if (admiMod == null)
            {
                throw new Exception("No se ha encontrado el administrador.");
            }

            /// modificar el objeto
            admiMod.Nombre = name;
            admiMod.Apellido = apellido;
            admiMod.Dni = dni;
            admiMod.fechaNacimiento = fecha;
            admiMod.Calle = calle;
            admiMod.Altura = alt;

            _context.Administradores.Update(admiMod);
            _context.SaveChanges();

            return $"Modificacion realizada con exito";

        }







        public string UdpateUsuario(int dniAdmiMod, string email)
        {
            if (dniAdmiMod == null || email == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }
            var admiMod = Buscar(dniAdmiMod);
          

            if (EmailRegistrado(email) == true)
            {
                throw new Exception($"Ya existe un administrador registrado con el E-Mail: {email}");
            }

            if (_v.CumpleRequisitosEmail(email) == false)
            {
                throw new Exception("El email ingresado esta en un formato incorrecto.");
            }

            /// modificar el objeto
            admiMod.Email = email;

            _context.Administradores.Update(admiMod);
            _context.SaveChanges();

            return $"Modificacion realizada con exito";
        }

        public string UpdatePassword(int dniAdmiMod, string password, string confirPass)
        {
            if (dniAdmiMod == null || password == null || confirPass == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }
            var admiMod = Buscar(dniAdmiMod);


            if (_v.CumpleRequisitosPass(password) == false)
            {
                throw new Exception("La contraseña debe contener al menos una mayuscula y un numero.");
            }

            if (_v.ConfirmarPass(password, confirPass) == false)
            {
                throw new Exception("Las contraseñas ingresadas no coinciden.");
            }

            /// modificar el objeto
            admiMod.Password = password;

            _context.Administradores.Update(admiMod);
            _context.SaveChanges();

            return $"Modificacion realizada con exito";
        }


        public string Delete(int dni)
        {
            var adm = Buscar(dni);

            _context.Administradores.Remove(adm);
            _context.SaveChanges();

            return $"Se ha eliminado el administrador con DNI {dni}";
        }

    }
}
