using Microsoft.AspNetCore.Mvc;
using Models.Clases;
using Models.Managers;

namespace WebService.Controllers
{
    [ApiController]
    [Route("clientes")]
    public class ClienteController : ControllerBase
    {
        [HttpPost("add")]
        public async Task<ActionResult<Cliente>> Add(string nombre, string apellido, int dni, DateTime fechaNac, string calle, int alt, uint tel)
        {
            string response;
            try
            {
                response = ClienteMG.Instancia.Add(nombre, apellido, dni, fechaNac, calle, alt, tel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpPut("update/nombres")]
        public async Task<ActionResult<Cliente>> UpdateNombres(int dni, string nombre, string apellido)
        {
            string response;
            try
            {
                response = ClienteMG.Instancia.UpdateNombres(dni, nombre, apellido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpPut("update/fechanacimiento")]
        public async Task<ActionResult<Cliente>> UpdateFechaNacimiento(int dni, DateTime fecha)
        {
            string response;
            try
            {
                response = ClienteMG.Instancia.UpdateFechaNacimiento(dni, fecha);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }


        [HttpPut("update/direccion")]
        public async Task<ActionResult<Cliente>> UpdateDireccion(int dni, string calle, int altura)
        {
            string response;
            try
            {
                response = ClienteMG.Instancia.UpdateDireccion(dni, calle, altura);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpPut("update/dni")]
        public async Task<ActionResult<Cliente>> UpdateDni(int dniMod, int dniNew, string confirPass)
        {
            string response;
            try
            {
                response = ClienteMG.Instancia.UpdateDNI(dniMod, dniNew);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpPut("update/telefono")]
        public async Task<ActionResult<Cliente>> UpdateTelefono(int dni, uint tel)
        {
            string response;
            try
            {
                response = ClienteMG.Instancia.UpdateTelefono(dni, tel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }


        [HttpDelete("delete")]
        public async Task<ActionResult<Cliente>> Delete(int dni)
        {
            string response;
            try
            {
                response = ClienteMG.Instancia.Delete(dni);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpGet("listado")]
        public async Task<ActionResult<IEnumerable<Cliente>>> Listado()
        {
            IEnumerable<Cliente> response;
            try
            {
                response = ClienteMG.Instancia.Listado();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);

        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Cliente>>> Buscar(int dni)
        {
            Cliente response;
            try
            {
                response = ClienteMG.Instancia.Buscar(dni);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);

        }

    }
}
