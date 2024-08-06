using Microsoft.AspNetCore.Mvc;
using Models.Clases;
using Models.Managers;

namespace WebService.Controllers
{
    [ApiController]
    [Route("canchas")]
    public class CanchaController : ControllerBase
    {
        [HttpPost("add")]
        public async Task<ActionResult<Cancha>> Add(string nombreDep, string nombre, decimal precio)
        {
            string response;
            try
            {
                response = CanchaMG.Instancia.Add(nombreDep, nombre, precio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<ActionResult<Cancha>> Update(string nombreCanchaMod, string nombreDep, string nombre, decimal precio)
        {
            string response;
            try
            {
                response = CanchaMG.Instancia.Update(nombreCanchaMod, nombreDep, nombre, precio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<Cancha>> Delete(string nombre)
        {
            string response;
            try
            {
                response = CanchaMG.Instancia.Delete(nombre);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpGet("listado")]
        public async Task<ActionResult<IEnumerable<Cancha>>> Listado()
        {
            IEnumerable<Cancha>response;
            try
            {
                response = CanchaMG.Instancia.Listado();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);

        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Cancha>>> Buscar(string nombre)
        {
            Cancha response;
            try
            {
                response = CanchaMG.Instancia.Buscar(nombre);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);

        }

    }

}
