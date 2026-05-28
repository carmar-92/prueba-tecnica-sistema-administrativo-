using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Core.Interfaces;

namespace PruebaTecnica.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly IReporteRepository _reporteRepository;

        public ReportesController(IReporteRepository reporteRepository)
        {
            _reporteRepository = reporteRepository;
        }

        [HttpGet("top-productos")]
        public async Task<IActionResult> GetTopProductos()
        {
            var reporte = await _reporteRepository.Top5ProductosMasVendidosAsync();
            return Ok(reporte);
        }

        [HttpGet("mejores-clientes")]
        public async Task<IActionResult> GetMejoresClientes()
        {
            var reporte = await _reporteRepository.ClientesMayorFacturacionAsync();
            return Ok(reporte);
        }

        [HttpGet("inventario-bajo")]
        public async Task<IActionResult> GetInventarioBajo()
        {
            var reporte = await _reporteRepository.InventarioBajoAsync();
            return Ok(reporte);
        }
    }
}
