using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiBiblioteca.DTOs;
using WebApiBiblioteca.Entidades;
using WebApiBiblioteca.Servicios;

namespace WebApiBiblioteca.Controllers
{
    [Route("api/v1/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;
        private readonly HashService hashService; 
        private readonly IDataProtector dataProtector;
        public UsuariosController(ApplicationDbContext context, IConfiguration configuration,
            IDataProtectionProvider dataProtectionProvider, HashService hashService)
        {
            this.context = context;
            this.configuration = configuration;
            this.hashService = hashService;
            dataProtector = dataProtectionProvider.CreateProtector(configuration["ClaveEncriptacion"]);
        }

        [HttpPost("hash/nuevousuario")]
        public async Task<ActionResult> PostNuevoUsuarioHash([FromBody] DTOUsuario usuario)
        {
            var resultadoHash = hashService.Hash(usuario.Password);
            var newUsuario = new Usuario
            {
                Email = usuario.Email,
                Password = resultadoHash.Hash
            };

            await context.Usuarios.AddAsync(newUsuario);
            await context.SaveChangesAsync();

            return Ok(newUsuario);
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] DTOUsuario usuario)
        {
            var usuarioDB = await context.Usuarios.FirstOrDefaultAsync(x => x.Email == usuario.Email);
            if (usuarioDB == null)
            {
                return BadRequest();
            }

            var resultadoHash = hashService.Hash(usuario.Password);
            if (usuarioDB.Password == resultadoHash.Hash)
            {
                var response = GenerarToken(usuario);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }



        private string GenerarToken(DTOUsuario credencialesUsuario)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, credencialesUsuario.Email)
            };

            var clave = configuration["ClaveJWT"];
            var claveKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clave));
            var signinCredentials = new SigningCredentials(claveKey, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signinCredentials
            );


            var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return  tokenString;
        }

    }
}
