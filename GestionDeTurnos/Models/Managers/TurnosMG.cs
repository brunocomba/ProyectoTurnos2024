
using Models.Clases;
using Models.ConnectionDB;
using Models.DTOs.Turno;
using System.ComponentModel.DataAnnotations;

namespace Models.Managers
{
    public class TurnosMG : GenericMG<Turno>
    {

        private readonly ClienteMG _clienteManager;
        private readonly CanchaMG _canchaManager;


        public TurnosMG(AppDbContext context, ClienteMG clienteManager, CanchaMG canchaManager) : base(context)
        {
            _clienteManager = clienteManager;
            _canchaManager = canchaManager;
        }


        private  bool TurnoRegistrado(TimeSpan horario, DateTime fecha, Cancha cancha)
        {
            var turnos =  _context.Turnos.ToList();
            foreach (var turno in turnos)
            {
                if (turno.Horario == horario && turno.Fecha.Date == fecha.Date && turno.Cancha == cancha)
                {
                    throw new Exception($"Error: El turno solicitado ya se encuentra registrado.");
                }
            }
            return false;
        }

        private bool EsFechaPasada(DateTime fecha)
        {
            var fechaHoy = DateTime.Now.Date;

            if (fecha.Date < fechaHoy)
            {
                throw new Exception("Error: No se puede registrar un turno con una fecha anterior a la actual.");
            }
            return false;
        }

        private bool EsHorarioPasado(TimeSpan horario)
        {
            var tiempoActual = DateTime.Now.TimeOfDay;

            if (horario < tiempoActual)
            {
                throw new Exception("Error: No se puede registrar un turno con un horario anterior a la actual.");
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
            bool conversionExitosa = TimeSpan.TryParse(horario, out TimeSpan timeSpan);

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

        public List<Turno> TurnosDeUnCliente(int dniCliente)
        {
            if (dniCliente == null)
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            return _context.Turnos.Where(turno => turno.Cliente.Dni == dniCliente).ToList();
        }

        public async Task<string> RegistrarAsync(AltaTurnoDTO dto)
        {
            var formatoHr = ConvertirStringEnTimeSpan(dto.Horario);
            _v.MayorDe0(dto.idCliente); _v.MayorDe0(dto.idCancha);

            var cancha = await _canchaManager.GetByIdAsync(dto.idCancha);
            var cliente = await _clienteManager.GetByIdAsync(dto.idCliente);
            
            TurnoRegistrado(formatoHr, dto.Fecha, cancha);
            EsFechaPasada(dto.Fecha);
            EsHorarioPasado(formatoHr);

            Turno turno = new Turno();
            {
                turno.Horario = formatoHr; turno.Fecha = dto.Fecha.Date ;turno.Cliente = cliente; turno.Cancha = cancha;
            }
           
            await _context.Turnos.AddAsync(turno);
            await _context.SaveChangesAsync();

            return $"Turno regitrado con exito.\nDia: {turno.Fecha.Date}\nHorario: {turno.Horario}\nCancha: {turno.Cancha.Name}\nCliente: {turno.Cliente.Nombre} {turno.Cliente.Apellido}\n" +
                $"Precio por jugador> ${CalcularPrecioPorJugador(cancha)}";

        }
      
        public async Task<string> UpdateDay(UpdateDayTurnoDTO dto)
        {
            _v.MayorDe0(dto.idTurnoMod);
            _v.SoloNumeros(dto.idTurnoMod);
            var turno = await _v.IdRegistrado(dto.idTurnoMod);
            EsFechaPasada(dto.fechaNew);
            TurnoRegistrado(turno.Horario, dto.fechaNew, turno.Cancha);

            // modificar fecha
            turno.Fecha = dto.fechaNew.Date;

            _context.Turnos.Update(turno);
            await _context.SaveChangesAsync();

            return ("Turno actualizado con exito");
        }


        public async Task<string> UpdateHorario(UpdateHorarioDTO dto)
        {
            _v.MayorDe0(dto.idTurnoMod);
            _v.SoloNumeros(dto.idTurnoMod);
            var turno = await _v.IdRegistrado(dto.idTurnoMod);

            if (dto.Horario == null)
            {
                throw new Exception("Error: Debe ingresar un horario");
            }

            var formatoHr = ConvertirStringEnTimeSpan(dto.Horario);
            EsHorarioPasado(formatoHr);
            TurnoRegistrado(formatoHr, turno.Fecha, turno.Cancha);
            
            // Modificar fecha
            turno.Horario = formatoHr;

            _context.Turnos.Update(turno);
            await _context.SaveChangesAsync();

            return $"Turno actualizado con exito";
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
