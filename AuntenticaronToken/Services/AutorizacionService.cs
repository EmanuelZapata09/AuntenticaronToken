using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutenticarConToken.Models;
using AutenticarConToken.Models.Customs;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;


namespace AutenticarConToken.Services
{
    public class AutorizacionService : IAutorizacionService
    {
        private readonly DbpruebaContext _context;
        private readonly IConfiguration _configuration;

        public AutorizacionService(DbpruebaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string GenerarToken(string idUsuario)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");
            //Convertimos esa llave en array
            var keyBytes = Encoding.ASCII.GetBytes(key);
            //Los claims contienen informacion
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));

            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),

                SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(3),
                SigningCredentials = credencialesToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);
            return tokenCreado;
        }


        public async Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion)
        {
            var usuario_encontrado = _context.Usuarios.FirstOrDefault(x =>
                x.NombreUsuario == autorizacion.NombreUsuario &&
                x.Clave == autorizacion.Clave
            );
            if(usuario_encontrado == null)
            {
                return await Task.FromResult<AutorizacionResponse>(null);
            }
            string tokenCreado = GenerarToken(usuario_encontrado.IdUsuario.ToString());
            //Retornar token
            return new AutorizacionResponse() { Token = tokenCreado,Resultado=true, Mensaje="Ok"};
            throw new NotImplementedException();
        }
    }
}
