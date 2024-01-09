using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReseñaTuMundo_Api.Data.Repositories;
using ReseñaTuMundo_Api.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReseñaTuMundo_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(await _userRepository.GetAllUser());
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetUserDetail(int id)
        {
            return Ok(await _userRepository.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
            
                return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var created = await _userRepository.InsertUser(user);
                return Created("created", created);
            
        }

        [HttpPost("auth")]
        public async Task<IActionResult> AuthUser([FromBody] User userInput)
        {
            try
            {
                if (userInput == null)
                {
                    return BadRequest(new { Mensaje = "Datos de entrada inválidos" });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var authenticatedUser = await _userRepository.AuthenticateUser(userInput.Nombre_Usuario, userInput.Contrasena);

                if (authenticatedUser == null)
                {
                    return Unauthorized(new { Mensaje = "Credenciales inválidas" });
                }

                // Puedes generar un token JWT aquí si es necesario
                 var token = GenerateJwtToken(authenticatedUser);

                return Ok(new { Mensaje = "Autenticación exitosa", Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = "Error interno del servidor", Error = ex.Message });
            }
        }
        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TuClaveSecretaCon128BitsO_Más_Aquí")); // Cambia esto por tu propia clave secreta
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Nombre_Usuario),
                new Claim(ClaimTypes.NameIdentifier, user.Id_Usuario.ToString()),
                // Puedes agregar más claims según sea necesario
            };

            var token = new JwtSecurityToken(
                issuer: "TuIssuer",
                audience: "TuAudience",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Cambia la duración según tus necesidades
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (user == null)
            
                return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

              await _userRepository.UpdateUser(user);
                return NoContent();
            
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userRepository.DeleteUser(new User { Id_Usuario = id});
            return NoContent();
        }

    }
}
