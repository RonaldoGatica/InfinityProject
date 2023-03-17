using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassLibraryInfinity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using ClassLibraryInfinity.DTOs;
using System.Security.Claims;
using ExamenWeb;

namespace Infinity.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class CuentasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration configuration;

        public CuentasController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }

        [HttpPost("register")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy ="user")]
        public async Task<ActionResult<ServiceResponse<ResponseAuthentication>>> Registrar(UserCredentials credencialesUsuario)
        {
            var existNickName = await _context.Userwebs.FindAsync(credencialesUsuario.Usuario);
            var existEmail = await _context.Userwebs.FindAsync(credencialesUsuario.Email);

            var resultado = false;
            if (existNickName != null || existEmail != null)
            {
                resultado = false;
            }
            else
            {
                resultado = true;
            }
            _context.Userwebs.Add(new Userweb {Email = credencialesUsuario.Email, Name = credencialesUsuario.Nombre, Nickname = credencialesUsuario.Usuario, Password = credencialesUsuario.Password });
            await _context.SaveChangesAsync();   

            if (resultado)
            {
                return new ServiceResponse<ResponseAuthentication>()
                {
                    //Data = ConstruirToken(credencialesUsuario),
                    Success = true,
                    Message = "Usuario registrado exitosamente"
                };
            }
            else//BadRequest(resultado.Errors);
            {
                return new ServiceResponse<ResponseAuthentication>()
                {
                    Data = null,
                    Success = false,
                    Message = "Este usuario ya existe!"
                };
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<ResponseAuthentication>>> Login(UserLogin credencialesUsuario)
        {
            var user = await _context.Userwebs.FindAsync(credencialesUsuario.Usuario);
            var resultado = false;
            Console.WriteLine(user);
            if (String.Equals(user.Password, credencialesUsuario.Password))
            {
                resultado = true;
            }

            var response = new ServiceResponse<ResponseAuthentication>();
            if (resultado)
            {
                response.Data = await ConstruirToken(credencialesUsuario);
                response.Success = true;
                response.Message = "Logueado exitoso";
            }
            else
            {
                response.Data = null;
                response.Success = false;
                response.Message = "Usuario o contraseña incorrecta";//"Usuario o contraseña incorrecta"
            }
            return response;
        }

        private async Task<ResponseAuthentication> ConstruirToken(UserLogin credencialesUsuario)
        {
            var claims = new List<Claim>()
            {
                new Claim("User",credencialesUsuario.Usuario)
            };

            //var user = await _context.Userwebs.FindAsync(credencialesUsuario.Usuario);
            //var claimsDB = await userManager.GetClaimsAsync(user);
            //claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddHours(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: creds);

            return new ResponseAuthentication()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion
            };

        }
    
    }
}
