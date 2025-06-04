using ABB.Catalogo.AccesoDatos.Core;
using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ABB.Catalogo.WebServiceAbb.Controllers
{
    public class UsuariosController : ApiController
    {
        // GET: api/Usuarios
        public IEnumerable<Usuario> Get()
        {
            List<Usuario> usuarios = new List<Usuario>();
            usuarios = new UsuariosLN().ListarUsuarios();

            return usuarios;
        }
        // GET: api/Usuarios?idusuario=5
        public Usuario Get([FromUri] int IdUsuario)
        {
            try
            {
                UsuariosLN usuario = new UsuariosLN();
                return usuario.BuscaUsuarioId(IdUsuario);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                throw;
            }
        }

        // GET api/usuarios?pUsuario=juan&pPassword=123
        public int Get([FromUri] string pUsuario, [FromUri] string pPassword)
        {
            try
            {
                UsuariosLN usuario = new UsuariosLN();
                return usuario.GetUsuarioId(pUsuario, pPassword);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                return -1;
            }


        }


        // POST: api/Usuarios
        public void Post([FromBody] Usuario value)
        {
            Usuario usuario = new UsuariosLN().InsertarUsuario(value);
        }


        // PUT: api/Usuarios/5
        public void Put(int id, [FromBody] Usuario value)
        {
            Usuario usuario = new Usuario();
            usuario = new UsuariosLN().ModificarUsuario(id,value);

        }


        // DELETE: api/Usuarios/5
        public void Delete(int id)
        {
            bool usuario = new UsuariosLN().Eliminar(id);
        }
    }
}
