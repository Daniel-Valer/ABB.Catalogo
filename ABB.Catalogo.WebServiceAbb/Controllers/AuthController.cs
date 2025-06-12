using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace WebServicesAbb.Controllers
{
    public class AuthController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Valida([FromBody] UsuariosApi usuarioAutentica)
        {
            //validaciones antes de procesar
            if (string.IsNullOrEmpty(usuarioAutentica.Clave)
                || string.IsNullOrEmpty(usuarioAutentica.UserName))
            {
                return BadRequest("Debe enviar la clave o codigo");
            }

            //validas los datos
            //validacion contra la bd
            UsuariosApi usuarioapi = new UsuariosApi();
            usuarioapi = new UsuarioApiLN().BuscaUsuarioApi(usuarioAutentica);

            if ((usuarioapi.Codigo <= 0))
            {
                return BadRequest("Credenciales no validas");

            }
            var expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];

            //crear la semilla
            string clave = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            byte[] claveEnBytes = Encoding.UTF8.GetBytes(clave);
            //para convertir la clave que esta como un arreglo de bytes en simetrica
            SymmetricSecurityKey key = new SymmetricSecurityKey(claveEnBytes);

            //generar algoritmo de ofuscacion
            SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //payload
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId,  usuarioAutentica.UserName.ToString()),
                //new Claim("nombre", "David"),
                new Claim("nombre", usuarioapi.Nombre),
               // new Claim("apellidos", "Espinoza"),
                new Claim("rol", usuarioapi.Rol),
               // new Claim(JwtRegisteredClaimNames.Email, "despinoza@avanza....com"),
               // new Claim(ClaimTypes.Role, "admin")
            };

            //encriptador
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                audience: ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"],
                issuer: ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"],
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)), //DateTime.Now.AddMinutes(10),
                claims: _Claims,
                signingCredentials: cred
                );


            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);


            return Ok(new TokenResponse
            {
                Token = token,
                User = usuarioAutentica.UserName

            });
        }
    }
}


