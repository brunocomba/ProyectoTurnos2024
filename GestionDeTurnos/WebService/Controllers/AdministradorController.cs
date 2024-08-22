using Microsoft.AspNetCore.Mvc;
using Models.Clases;
using Models.DTOs.Administrador; /// Aceceder a las DTOs de Administradores
using Models.Managers;

namespace WebService.Controllers
{
    [ApiController]
    [Route("administradores")]
    public class AdministradorController : ControllerBase
    {
        [HttpPost("add")]
        public async Task<ActionResult<Administrador>> Add(AltaDTO dto)
        {
            string response;
            try
            {
                response =  AdministradorMG.Instancia.Add(dto.Nombre, dto.Apellido, dto.Dni, dto.fechaNacimiento, dto.Calle, dto.Altura, dto.Email, dto.Password, dto.confirPass);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpPut("update/datosPersonales{id}")]
        public async Task<ActionResult<Administrador>> UpdateNombres(UpdateDatosPersonalesDTO dto, int id)
        {
            string response;
            try
            {
                response = AdministradorMG.Instancia.UpdateDatosPerosnales(id, dto.Nombre, dto.Apellido, dto.Dni, dto.fechaNacimiento, dto.Calle, dto.Altura);
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

        [HttpGet("buscar{dni}")]
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
