using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using PruebaTecnica.Core.Entities;
using PruebaTecnica.Core.Interfaces;

namespace PruebaTecnica.API.Controllers
{
    [Authorize] 
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var clientes = await _clienteRepository.ListarAsync();
            return Ok(clientes);
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar([FromQuery] string termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
                return BadRequest("Debe proporcionar un término de búsqueda.");

            var clientes = await _clienteRepository.BuscarAsync(termino);
            return Ok(clientes);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Cliente cliente)
        {
            // Validaciones básicas requeridas por la prueba
            if (string.IsNullOrWhiteSpace(cliente.Nombre) || string.IsNullOrWhiteSpace(cliente.IdentidadRTN))
                return BadRequest("El nombre y la Identidad/RTN son obligatorios.");

            var exito = await _clienteRepository.CrearAsync(cliente);
            if (!exito) return StatusCode(500, "Error al crear el cliente.");

            return Ok(new { mensaje = "Cliente creado exitosamente." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.IdClientes)
                return BadRequest("El ID de la ruta no coincide con el ID del cliente.");

            var exito = await _clienteRepository.EditarAsync(cliente);
            if (!exito) return StatusCode(500, "Error al actualizar el cliente.");

            return Ok(new { mensaje = "Cliente actualizado exitosamente." });
        }
    }
}