using Microsoft.AspNetCore.Mvc;
using Models.Clases;
using Models.Managers;

namespace WebService.Controllers
{
    [ApiController]
    [Route("administradores")]
    public class AdministradorController : ControllerBase
    {
        [HttpPost("add")]
        public async Task<ActionResult<Administrador>> Add(string nombre, string apellido, int dni, DateTime fechaNac, string calle, int alt, string email, string pass, string confirPass)
        {
            string response;
            try
            {
                response = AdministradorMG.Instancia.Add(nombre, apellido, dni, fechaNac, calle, alt, email, pass, confirPass);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpPut("update/nombres")]
        public async Task<ActionResult<Administrador>> UpdateNombres(int dni, string nombre, string apellido)
        {
            string response;
            try
            {
                response = AdministradorMG.Instancia.UpdateNombres(dni, nombre, apellido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpPut("update/fechanacimiento")]
        public async Task<ActionResult<Administrador>> UpdateFechaNacimiento(int dni, DateTime fecha)
        {
            string response;
            try
            {
                response = AdministradorMG.Instancia.UpdateFechaNacimiento(dni, fecha);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }


        [HttpPut("update/direccion")]
        public async Task<ActionResult<Administrador>> UpdateDireccion(int dni, string calle, int altura)
        {
            string response;
            try
            {
                response = AdministradorMG.Instancia.UpdateDireccion(dni, calle, altura);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }


        [HttpPut("update/usuario")]
        public async Task<ActionResult<Administrador>> UpdateUsuario(int dni, string email)
        {
            string response;
            try
            {
                response = AdministradorMG.Instancia.UdpateUsuario(dni, email);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpPut("update/password")]
        public async Task<ActionResult<Administrador>> UpdatePass(int dni, string pass, string confirPass)
        {
            string response;
            try
            {
                response = AdministradorMG.Instancia.UpdatePassword(dni, pass, confirPass);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpPut("update/dni")]
        public async Task<ActionResult<Administrador>> UpdateDni(int dniMod, int dniNew, string confirPass)
        {
            string response;
            try
            {
                response = AdministradorMG.Instancia.UpdateDNI(dniMod, dniNew);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<Administrador>> Delete(int dni)
        {
            string response;
            try
            {
                response = AdministradorMG.Instancia.Delete(dni);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpGet("listado")]
        public async Task<ActionResult<IEnumerable<Administrador>>> Listado()
        {
            IEnumerable<Administrador> response;
            try
            {
                response = AdministradorMG.Instancia.Listado();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);

        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Administrador>>> Buscar(int dni)
        {
            Administrador response;
            try
            {
                response = AdministradorMG.Instancia.Buscar(dni);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);

        }

    }
}
