using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PruebaTecnica.Core.Entities;
using PruebaTecnica.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PruebaTecnica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _config;
        private readonly IBitacoraRepository _bitacoraRepository;
        
        public AuthController(IUsuarioRepository usuarioRepository, IConfiguration config, IBitacoraRepository bitacoraRepository)
        {
            _usuarioRepository = usuarioRepository;
            _config = config;
            _bitacoraRepository = bitacoraRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var usuario = await _usuarioRepository.ObtenerPorCorreoAsync(request.Correo);

            if (usuario == null || usuario.Contrasena != request.Contrasena)
            {                
                await _bitacoraRepository.RegistrarAsync(new BitacoraError
                {
                    TipoEvento = "Intento Fallido",
                    Mensaje = $"Intento de inicio de sesión fallido para el correo: {request.Correo}"
                });

                return Unauthorized(new { mensaje = "Correo o contraseña incorrectos." });
            }

            var token = GenerarJwtToken(usuario);
            return Ok(new { token = token, nombre = usuario.Nombre });
        }

        private string GenerarJwtToken(Usuario usuario)
        {          
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtConfig:Secret"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Nombre),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Correo),
                new Claim("idUsuario", usuario.IdUsuarios.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["JwtConfig:Issuer"],
                audience: _config["JwtConfig:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    public class LoginDto
    {
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
    }
}