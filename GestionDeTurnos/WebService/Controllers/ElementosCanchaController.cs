using Microsoft.AspNetCore.Mvc;
using Models.Clases;
using Models.Managers;

namespace WebService.Controllers
{
    [ApiController]
    [Route("elementoscancha")]
    public class ElementosCanchaController : ControllerBase
    {
        [HttpPost("add")]
        public async Task<ActionResult<ElementosCancha>> Add(string nombreElemento, string nombreCancha, int cantidad)
        {
            string response;
            try
            {
                response = ElementosCanchaMG.Instancia.Add(nombreElemento, nombreCancha, cantidad);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpPut("update/cantidad/agregar")]
        public async Task<ActionResult<ElementosCancha>> AgregarCantidad(string nombreElemento, string nombreCancha, int cantidad)
        {
            string response;
            try
            {
                response = ElementosCanchaMG.Instancia.AddCantidad(nombreElemento, nombreCancha, cantidad);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpPut("update/cantidad/restar")]
        public async Task<ActionResult<ElementosCancha>> RestarCantidada(string nombreElemento, string nombreCancha, int cantidad)
        {
            string response;
            try
            {
                response = ElementosCanchaMG.Instancia.RestarCantidad(nombreElemento, nombreCancha, cantidad);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ElementosCancha>> Delete(string nombreElemento, string nombreCancha)
        {
            string response;
            try
            {
                response = ElementosCanchaMG.Instancia.Delete(nombreElemento, nombreCancha);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpGet("listado")]
        public async Task<ActionResult<IEnumerable<ElementosCancha>>> Listado()
        {
            IEnumerable<ElementosCancha> response;
            try
            {
                response = ElementosCanchaMG.Instancia.Listado();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);

        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<ElementosCancha>>> Buscar(string nombreElemento, string nombreCancha)
        {
            ElementosCancha response;
            try
            {
                response = ElementosCanchaMG.Instancia.Buscar(nombreElemento, nombreCancha);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);

        }
    }
}
