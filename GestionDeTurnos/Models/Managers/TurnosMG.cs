using Models.Clases;
using Models.ConnectionDB;
using System.Net;

namespace Models.Managers
{
    public class TurnosMG
    {
        private TurnosMG() { }

        private static TurnosMG? instance;

        public static TurnosMG Instancia
        {
            get
            {
                if (instance == null)
                {
                    instance = new TurnosMG();
                }
                return instance;
            }
        }

        private readonly AppDbContext _context = AppDbContext.Instancia;

        private static readonly Validaciones _v = new Validaciones();

        private bool TurnoRegistrado(TimeSpan horario, DateTime fecha, Cancha cancha)
        {
            var turnos = Listado();
            foreach (var turno in turnos)
            {
                if (turno.Horario == horario && turno.Fecha.Date == fecha.Date && turno.Cancha == cancha)
                {
                    return true;
                }
            }
            return false;
        }

        private bool EsFechaPasada(DateTime fecha)
        {
            var fechaHoy = DateTime.Now.Date;

            if (fecha.Date < fechaHoy)
            {
                return true;
            }
            return false;
        }

        private bool EsHorarioPasado(TimeSpan horario)
        {
            var tiempoActual = DateTime.Now.TimeOfDay;

            if (horario < tiempoActual)
            {
                return true;
            }
            return false;

        }

        private decimal CalcularPrecioPorJugador(Cancha cancha)
        {
            var cantJugadores = cancha.Deporte.cantJugadores;
            var precioCancha = cancha.Precio;

            return precioCancha / cantJugadores;
        }
        
        private bool ClienteTieneMismaFechaAndHorario(Turno turnoDado, Cliente cliente)
        {
            var turnos = Listado();
            foreach (var turno in turnos)
            {
                if (turno.Cliente == cliente && turnoDado.Fecha == turno.Fecha && turnoDado.Horario == turno.Horario)
                {
                    return true;
                }
            }
            return false;

        }
        private bool CanchaTieneMismaFechaAndHorario(Turno turnoDado, Cancha cancha)
        {
            var turnos = Listado();
            foreach (var turno in turnos)
            {
                if (turno.Cancha == cancha && turnoDado.Fecha == turno.Fecha && turnoDado.Horario == turno.Horario)
                {
                    return true;
                }
            }
            return false;

        }
        private TimeSpan ConvertirStringEnTimeSpan(string horario)
        {
            bool conversionExitosa = TimeSpan.TryParse(horario, out  TimeSpan timeSpan);

            if (conversionExitosa == false)
            {
                throw new Exception("No se pudo realzar la convercion.");
            }

            return timeSpan;
        }

        public Turno Buscar(int id)
        {
            var turno = _context.Turnos.FirstOrDefault(t => t.Id == id);
            if (id == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            if (turno == null)
            {
                throw new Exception($"No se encontro ningun turno registrado con el ID: {id}");
            }


            return turno;
        }

        public List<Turno> Listado()
        {
            return _context.Turnos.ToList();
        }

        public List<Turno> TurnosDeUnCliente(int dniCliente)
        {
            if (dniCliente == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            return _context.Turnos.Where(turno => turno.Cliente.Dni == dniCliente).ToList();
        }

        public string Add(string horario, DateTime fecha, string nameCancha, int dniCliente)
        {
            var formateoHorario = ConvertirStringEnTimeSpan(horario);

            if (horario == null || fecha == null || nameCancha == null || dniCliente == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var cancha = CanchaMG.Instancia.Buscar(nameCancha);
            var cliente = ClienteMG.Instancia.Buscar(dniCliente);

            if (TurnoRegistrado(formateoHorario, fecha, cancha) == true)
            {
                throw new Exception($"El turno solicitado ya se encuentra registrado.");
            }

            if (EsFechaPasada(fecha) == true)
            {
                throw new Exception("No se puede registrar un turno con una fecha anterior a la actual.");
            }

            if (EsHorarioPasado(formateoHorario) == true)
            {
                throw new Exception("No se puede registrar un turno con un horario anterior a la actual.");
            }


            Turno turno = new Turno();
            turno.Horario = formateoHorario;
            turno.Fecha = fecha.Date;
            turno.Cliente = cliente;
            turno.Cancha = cancha;

            _context.Turnos.Add(turno);
            _context.SaveChanges();

            return $"Turno regitrado con exito.\nDia: {turno.Fecha.Date}\nHorario: {turno.Horario}\nCancha: {turno.Cancha.Name}\nCliente: {turno.Cliente.Nombre} {turno.Cliente.Apellido}\n" +
                $"Precio por jugador> ${CalcularPrecioPorJugador(cancha)}";
        }

        public string UpdateDay(int turnoID, DateTime fechaNew)
        {
            if (fechaNew == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var turnoExist = Buscar(turnoID);

            if (EsFechaPasada(fechaNew) == true)
            {
                throw new Exception("No se puede registrar un turno con una fecha anterior a la actual.");
            }

            if (TurnoRegistrado(turnoExist.Horario, fechaNew, turnoExist.Cancha) == true)
            {
                throw new Exception($"El turno solicitado ya se encuentra registrado.");
            }


            // Modificar fecha
            turnoExist.Fecha = fechaNew.Date;

            _context.Turnos.Update(turnoExist);
            _context.SaveChanges();

            return $"Modificacion realizada con exito";
        }

        public string UdpateHorario(int turnoID, string horario)
        {
            if (horario == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var formateoHorario = ConvertirStringEnTimeSpan(horario);

            var turnoExist = Buscar(turnoID);
           
            if (horario == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            if (EsHorarioPasado(formateoHorario) == true)
            {
                throw new Exception("No se puede registrar un turno con un horario anterior a la actual.");
            }

            if (TurnoRegistrado(formateoHorario, turnoExist.Fecha, turnoExist.Cancha) == true)
            {
                throw new Exception($"El turno solicitado ya se encuentra registrado.");
            }

            // Modificar fecha
            turnoExist.Horario = formateoHorario;

            _context.Turnos.Update(turnoExist);
            _context.SaveChanges();

            return $"Modificacion realizada con exito";
        }

        public string UpdateCliente(int turnoID, int dniCliente)
        {
            if (dniCliente == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var turnoExist = Buscar(turnoID);
            var cliente = ClienteMG.Instancia.Buscar(dniCliente);

           
            if (ClienteTieneMismaFechaAndHorario(turnoExist, cliente) == true)
            {
                throw new Exception("El cliente que quieres cambiar ya tiene un turno registrado para el mismo dia y horario.");
            }
            
            
            // Modificar fecha
            turnoExist.Cliente = cliente;

            _context.Turnos.Update(turnoExist);
            _context.SaveChanges();

            return $"Modificacion realizada con exito";
        }

        public string UpdateCancha(int turnoID, string nombreCancha)
        {
            if (nombreCancha == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            var turnoExist = Buscar(turnoID);
            var cancha = CanchaMG.Instancia.Buscar(nombreCancha);

            if (CanchaTieneMismaFechaAndHorario(turnoExist, cancha) == true)
            {
                throw new Exception("La cancha que quieres cambiar ya tiene un turno registrado para el mismo dia y horario.");
            }

            // Modificar fecha
            turnoExist.Cancha = cancha;

            _context.Turnos.Update(turnoExist);
            _context.SaveChanges();

            return $"Modificacion realizada con exito";
        }

        public string Delete(int turnoID)
        {
            var turno = Buscar(turnoID);

            _context.Turnos.Remove(turno);
            _context.SaveChanges();

            return $"Se ha eliminado el turno con exito.";
        }


        public decimal ResultadoEconomicoDelMes(DateTime fechaDelDia)
        {
            List<Turno> turnosDelMes = new List<Turno>();

            var mesFecha = fechaDelDia.Month;
            var turnos = _context.Turnos.ToList();

            foreach (var turno in turnos)
            {
                if (turno.Fecha.Month == mesFecha && turno.Asistido == Turno.SINO.SI)
                {
                    turnosDelMes.Add(turno);
                }
            }

            decimal result = 0;
            foreach (var t in turnosDelMes)
            {
                result = result + t.Cancha.Precio;
            }

            return result;
        }


    }
}
