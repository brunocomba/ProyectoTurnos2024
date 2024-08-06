using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Managers;

namespace WebService.Controllers
{
    [ApiController]
    [Route("deportes")]
    public class DeporteController : ControllerBase
    {
        [HttpPost("add")]
        public async Task<ActionResult<Deporte>> Add(string nombreDep, int cantJugadores)
        {
            string response;
            try
            {
                response = DeporteMG.Instancia.Add(nombreDep, cantJugadores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<ActionResult<Deporte>> Update(string nombreDepMod, string nombre, int cantJugadores)
        {
            string response;
            try
            {
                response = DeporteMG.Instancia.Update(nombreDepMod, nombre, cantJugadores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<Deporte>> Delete(string nombre)
        {
            string response;
            try
            {
                response = DeporteMG.Instancia.Delete(nombre);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpGet("listado")]
        public async Task<ActionResult<IEnumerable<Deporte>>> Listado()
        {
            IEnumerable<Deporte> response;
            try
            {
                response = DeporteMG.Instancia.Listado();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);

        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Deporte>>> Buscar(string nombre)
        {
            Deporte response;
            try
            {
                response = DeporteMG.Instancia.Buscar(nombre);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);

        }
    }
}
