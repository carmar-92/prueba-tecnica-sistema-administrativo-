using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Core.Entities;
using PruebaTecnica.Core.Interfaces;

namespace PruebaTecnica.API.Controllers
{
    [Authorize] 
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturaRepository _facturaRepository;
        private readonly IProductoRepository _productoRepository;

        // Inyectamos ambos repositorios porque necesitamos validar el precio y stock de los productos
        public FacturasController(IFacturaRepository facturaRepository, IProductoRepository productoRepository)
        {
            _facturaRepository = facturaRepository;
            _productoRepository = productoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var facturas = await _facturaRepository.ListarAsync();
            return Ok(facturas);
        }

        [HttpPost]
        public async Task<IActionResult> CrearFactura([FromBody] CrearFacturaDto request)
        {
            if (request.Detalles == null || !request.Detalles.Any())
                return BadRequest("La factura debe contener al menos un producto.");

            // 1. Extraer el IdUsuario directamente del Token JWT por seguridad
            var usuarioIdClaim = User.FindFirst("idUsuario")?.Value;
            if (string.IsNullOrEmpty(usuarioIdClaim))
                return Unauthorized("Token inválido o no contiene el ID del usuario.");

            int idUsuario = int.Parse(usuarioIdClaim);

            // 2. Inicializar la entidad Factura
            var nuevaFactura = new Factura
            {
                IdUsuarios = idUsuario,
                IdClientes = request.IdClientes,
                Fecha = DateTime.Now,
                Subtotal = 0,
                Detalles = new List<FacturaDetalle>()
            };

            // 3. Procesar cada detalle y calcular la matemática real
            foreach (var item in request.Detalles)
            {
                var productoBD = await _productoRepository.ObtenerPorIdAsync(item.IdProductos);

                if (productoBD == null)
                    return BadRequest($"El producto con ID {item.IdProductos} no existe o está inactivo.");

                if (productoBD.Stock < item.Cantidad)
                    return BadRequest($"Stock insuficiente para el producto '{productoBD.Nombre}'. Stock actual: {productoBD.Stock}");

                // Tomamos el precio de la base de datos, NO del frontend
                decimal subtotalLinea = productoBD.Precio * item.Cantidad;

                nuevaFactura.Subtotal += subtotalLinea;

                nuevaFactura.Detalles.Add(new FacturaDetalle
                {
                    IdProductos = productoBD.IdProductos,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = productoBD.Precio,
                    Subtotal = subtotalLinea
                });
            }

            // 4. Calcular los totales finales (ISV 15%)
            nuevaFactura.ISV = nuevaFactura.Subtotal * 0.15m;
            nuevaFactura.Total = nuevaFactura.Subtotal + nuevaFactura.ISV;

            // 5. Enviar al repositorio para guardar con Transacción SQL
            var exito = await _facturaRepository.CrearFacturaAsync(nuevaFactura);

            if (!exito)
                return StatusCode(500, "Ocurrió un error interno al guardar la factura.");

            return Ok(new
            {
                mensaje = "Factura procesada y guardada exitosamente.",
                totalFacturado = nuevaFactura.Total
            });
        }
    }

    // DTOs para recibir solo la información estrictamente necesaria desde el Frontend
    public class CrearFacturaDto
    {
        public int IdClientes { get; set; }
        public List<DetalleFacturaDto> Detalles { get; set; } = new List<DetalleFacturaDto>();
    }

    public class DetalleFacturaDto
    {
        public int IdProductos { get; set; }
        public int Cantidad { get; set; }
    }
}
