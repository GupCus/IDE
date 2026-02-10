using Microsoft.AspNetCore.Mvc;
using Barroso.Context;
using Barroso.Clases;

namespace Barroso.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LiquidacionController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var liquidaciones = await LiquidacionRepository.EncontrarLiquidacionesAsync();
            if (liquidaciones.Count != 0)
            {
                return Ok(liquidaciones);
            }
            return NotFound();
        }
        [HttpGet("/estado/{estado}")]
        public async Task<IActionResult> GetPorEstados(string estado)
        {
            var liquidaciones = await LiquidacionRepository.EncontrarLiquidacionesPorEstadoAsync(estado);
            if (liquidaciones.Count != 0)
            {
                return Ok(liquidaciones);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Liquidacion liq)
        {
            try
            {
                liq.Id = null;
                liq.Estado = "Pendiente";
                ValidarLiquidacion(liq);
                var l = await LiquidacionRepository.CrearLiquidacionAsync(liq);
                return Created();
            }
            catch (MalaSolicitudException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Liquidacion l)
        {
            try
            {
                l.Id = id;
                await LiquidacionRepository.ActualizarLiquidacionAsync(l);
                return NoContent();
            }
            catch (MalaSolicitudException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NoEncontradoException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Hubo un problema con la solicitud");
            }
        }

        private static void ValidarLiquidacion(Liquidacion liq)
        {
            if (string.IsNullOrWhiteSpace(liq.Empleado)) throw new MalaSolicitudException("El campo empleado es obligatorio");

            if (liq.Monto < 0 || liq.Monto > 1_000_000) throw new MalaSolicitudException("El monto tienq que ser un valor positivo no mayor a 1.000.000");

            var hoy = DateOnly.FromDateTime(DateTime.Now);
            if (liq.Fecha < hoy.AddDays(-30) || liq.Fecha > hoy.AddDays(30)) throw new MalaSolicitudException("La fecha debe estar entre los 30 dias mas cercanos a la fecha actual");
        }

    }


}
