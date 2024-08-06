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


        public Administrador Buscar(int dni)
        {
            if (dni == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var adm = _context.Administradores.FirstOrDefault(a => a.Dni == dni);

            if (adm == null)
            {
                throw new Exception($"No se encontro un administrador registrado con el DNI: {dni}");
            }
            return adm;
        }

        public List<Administrador> Listado()
        {
            return _context.Administradores.ToList();
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


        public string UpdateNombres(int dniAdmiMod, string name, string apellido)
        {
            if (dniAdmiMod == null ||name == null || apellido == null )
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var admiMod = Buscar(dniAdmiMod);

           

            if (_v.SoloLetras(name) == false || _v.SoloLetras(apellido) == false)
            {
                throw new Exception($"El nombre y/o el apellido no puede contener numeros o caracteres especiales.");
            }

            /// modificar el objeto
            admiMod.Nombre = name;
            admiMod.Apellido = apellido;

            _context.Administradores.Update(admiMod);
            _context.SaveChanges();

            return $"Modificacion realizada con exito";

        }



        public string UpdateFechaNacimiento(int dniAdmiMod, DateTime fecha)
        {
            if (dniAdmiMod == null || fecha == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var admiMod = Buscar(dniAdmiMod);

           
            if (_v.Mayor18(fecha) == false)
            {
                throw new Exception("Es necesario ser mayor de 18 años");
            }

            /// modificar el objeto
            admiMod.fechaNacimiento = fecha.Date;

            _context.Administradores.Update(admiMod);
            _context.SaveChanges();

            return $"Modificacion realizada con exito";
        }

        

        public string UpdateDireccion(int dniAdmiMod, string calle, int altura)
        {
            if (dniAdmiMod == null || calle == null || altura == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var admiMod = Buscar(dniAdmiMod);

      

            if (_v.SoloLetras(calle) == false )
            {
                throw new Exception($"La calle no puede contener numeros o caracteres especiales.");
            }

            if (_v.SoloNumeros(altura) == false)
            {
                throw new Exception($"La altura no puede contener letras.");
            }

            /// modificar el objeto
            admiMod.Calle = calle;
            admiMod.Altura = altura;

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

        public string UpdateDNI(int dniAdmiMod, int dni)
        {
            if (dniAdmiMod == null || dni == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }
            var admiMod = Buscar(dniAdmiMod);

           
            if (Buscar(dni) != null)
            {
                throw new Exception($"Ya existe un administrador registrado con el DNI: {dni}");
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
            admiMod.Dni = dni;

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
