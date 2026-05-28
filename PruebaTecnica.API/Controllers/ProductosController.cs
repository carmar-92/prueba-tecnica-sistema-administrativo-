using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Core.Entities;
using PruebaTecnica.Core.Interfaces;

namespace PruebaTecnica.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoRepository _productoRepository;

        public ProductosController(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var productos = await _productoRepository.ListarAsync();
            return Ok(productos);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Codigo) || string.IsNullOrWhiteSpace(producto.Nombre))
                return BadRequest("El código y el nombre son obligatorios.");

            if (producto.Precio <= 0)
                return BadRequest("El precio debe ser mayor a cero.");

            var exito = await _productoRepository.CrearAsync(producto);
            if (!exito) return StatusCode(500, "Error al crear el producto.");

            return Ok(new { mensaje = "Producto creado exitosamente." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] Producto producto)
        {
            if (id != producto.IdProductos)
                return BadRequest("El ID de la ruta no coincide con el ID del producto.");

            var exito = await _productoRepository.EditarAsync(producto);
            if (!exito) return StatusCode(500, "Error al actualizar el producto o no existe.");

            return Ok(new { mensaje = "Producto actualizado exitosamente." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            var exito = await _productoRepository.DesactivarAsync(id);
            if (!exito) return StatusCode(500, "Error al desactivar el producto o ya estaba inactivo.");

            return Ok(new { mensaje = "Producto desactivado exitosamente." });
        }
    }
}