using Microsoft.AspNetCore.Mvc;
using Models.Managers;
using Models;
using Models.Clases;

namespace WebService.Controllers
{
    [ApiController]
    [Route("elementos")]
    public class ElementoController : ControllerBase
    {
        [HttpPost("add")]
        public async Task<ActionResult<Elemento>> Add(string nombre, int stock)
        {
            string response;
            try
            {
                response = ElementoMG.Instancia.Add(nombre, stock);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpPut("update/stock/agregar")]
        public async Task<ActionResult<Elemento>> AgregarStock(string nombre,  int stock)
        {
            string response;
            try
            {
                response = ElementoMG.Instancia.AddStock(nombre, stock);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpPut("update/stock/restar")]
        public async Task<ActionResult<Elemento>> RestarStock(string nombre, int stock)
        {
            string response;
            try
            {
                response = ElementoMG.Instancia.RestarStock(nombre, stock);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpPut("update/nombre")]
        public async Task<ActionResult<Elemento>> UpdateNombre(string nombreMod, string nombreNew)
        {
            string response;
            try
            {
                response = ElementoMG.Instancia.UpdateNombre(nombreMod, nombreNew);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<Elemento>> Delete(string nombre)
        {
            string response;
            try
            {
                response = ElementoMG.Instancia.Delete(nombre);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpGet("listado")]
        public async Task<ActionResult<IEnumerable<Elemento>>> Listado()
        {
            IEnumerable<Elemento> response;
            try
            {
                response = ElementoMG.Instancia.Listado();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);

        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Elemento>>> Buscar(string nombre)
        {
            Elemento response;
            try
            {
                response = ElementoMG.Instancia.Buscar(nombre);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);

        }
    }
}
