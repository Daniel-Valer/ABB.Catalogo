using ABB.Catalogo.Entidades;
using ABB.Catalogo.Entidades.Base;
using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;
using ABB.Catalogo.Utiles.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace ABB.Catalogo.ClienteWeb.Controllers
{
    public class LoginController : BaseLN
    {
        // GET: Login
        public ActionResult Index()
        {
            Usuario user = new Usuario();
            return View(user);
        }
        [HttpPost]
        public ActionResult Index(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.CodUsuario) || string.IsNullOrEmpty(usuario.ClaveTxt))
            {
                //Log.Info("Llego usuario: " + usuario.CodUsuario);
                ModelState.AddModelError("*", "Debe llenar el usuario o clave");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    usuario.Clave = EncriptacionHelper.EncriptarByte(usuario.ClaveTxt);

                    Usuario res = new UsuariosLN().BuscarUsuario(usuario);

                    if (res != null && res.IdUsuario > 0)  //las credenciales son correctas
                    {
                        //Llenar una cookie
                        FormsAuthentication.SetAuthCookie(res.CodUsuario, true);
                        //llenar una sesion
                        List<Opcion> lista = new OpcionLN().ListaOpciones();
                        //para separar los controles de las acciones en la tabla de Opciones.
                        ParsearAcciones(lista);
                        VariablesWeb.gOpciones = lista;
                        Log.Info("Llego las opciones. " + VariablesWeb.gOpciones.Count().ToString());
                        VariablesWeb.gUsuario = res;

                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        ModelState.AddModelError("*", "Usuario / Clave no validos");
                    }

                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("*", ex.Message);
                }
            }


            return View(usuario);
        }
        [NonAction]
        private void ParsearAcciones(List<Opcion> lista)
        {

            int cantidad = 0;
            foreach (Opcion item in lista)
            {
                if (!string.IsNullOrEmpty(item.UrlOpcion))
                {
                    cantidad = item.UrlOpcion.Split('/').Count();
                    switch (cantidad)
                    {
                        case 3:
                            item.Area = item.UrlOpcion.Split('/')[0];
                            item.Controladora = item.UrlOpcion.Split('/')[1];
                            item.Accion = item.UrlOpcion.Split('/')[2];
                            break;
                        case 2:
                            item.Controladora = item.UrlOpcion.Split('/')[0];
                            item.Accion = item.UrlOpcion.Split('/')[1];
                            break;
                        case 1:
                            item.Controladora = item.UrlOpcion.Split('/')[0];
                            item.Accion = "Index";
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
