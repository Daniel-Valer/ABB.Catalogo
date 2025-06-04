using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using ABB.Catalogo.AccesoDatos.Core;
using System.Web.Security;
using System.Web.WebSockets;

namespace ABB.Catalogo.ClienteWeb.Controllers
{
    public class UsuariosController : Controller
    {
        string RutaApi = "https://localhost:44318/Api/"; //define la ruta del web api
        string jsonMediaType = "application/json"; // define el tipo de dat
        // GET: Usuarios
        public ActionResult Index()
        {
            string controladora = "Usuarios";
            string metodo = "Get";
            List<Usuario> listausuarios = new List<Usuario>();
            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                //establece el tipo de dato de tranferencia
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                usuario.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = usuario.DownloadString(new Uri(rutacompleta));
                // convierte los datos traidos por la api a tipo lista de usuarios
                listausuarios = JsonConvert.DeserializeObject<List<Usuario>>(data);
            }

            return View(listausuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            Usuario usuario = new Usuario();// se crea una instancia de la clase usuario
            List<Rol> listarol = new List<Rol>();
            listarol = new RolLN().ListaRol();
            listarol.Add(new Rol() { IdRol = 0, DesRol = "[Seleccione Rol...]" });
            ViewBag.listaRoles = listarol;
            return View(usuario);
        }


        // POST: Usuarios/Create
        [HttpPost]
        public ActionResult Create(Usuario collection)
        {
            string controladora = "Usuarios";
            try
            {
                // TODO: Add insert logic here
                using (WebClient usuario = new WebClient())
                {
                    usuario.Headers.Clear();//borra datos anteriores
                    //establece el tipo de dato de tranferencia
                    usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    //typo de decodificador reconocimiento carecteres especiales
                    usuario.Encoding = UTF8Encoding.UTF8;
                    //convierte el objeto de tipo Usuarios a una trama Json
                    var usuarioJson = JsonConvert.SerializeObject(collection);
                    string rutacompleta = RutaApi + controladora;
                    var resultado = usuario.UploadString(new Uri(rutacompleta), usuarioJson);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {
            string controladora = "Usuarios";
            string metodo = "GetUserId";
            Usuario users = new Usuario();
            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                //establece el tipo de dato de tranferencia
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                usuario.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora + "?IdUsuario=" + id;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = usuario.DownloadString(new Uri(rutacompleta));

                // convierte los datos traidos por la api a tipo lista de usuarios
                users = JsonConvert.DeserializeObject<Usuario>(data);
            }
            List<Rol> listaRoles = new RolLN().ListaRol();
            listaRoles.Add(new Rol() { IdRol = 0, DesRol = "[Seleccione Rol...]" });
            ViewBag.listaRoles = listaRoles.OrderBy(r => r.IdRol).ToList();
            return View(users);
        }


        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int idUsuario, Usuario collection)
        {
            string controladora = "Usuarios"; // Nombre del controlador en la API
            try
            {
                using (WebClient cliente = new WebClient())
                {
                    cliente.Headers.Clear(); // Borra cabeceras anteriores
                    cliente.Headers[HttpRequestHeader.ContentType] = "application/json"; // Tipo de contenido JSON
                    cliente.Encoding = UTF8Encoding.UTF8; // Establecer la codificación de caracteres

                    // Serializar el objeto Usuario a formato JSON
                    var usuarioJson = JsonConvert.SerializeObject(collection);

                    // Construir la URL completa del API para hacer el PUT
                    string url = RutaApi + controladora + "/" + idUsuario;

                    // Hacer la llamada PUT al API
                    var resultado = cliente.UploadString(new Uri(url), "PUT", usuarioJson);
                }

                // Redirigir a la acción "Index" después de actualizar el usuario
                return RedirectToAction("Index");
            }
            catch
            {
                // Si ocurre un error, vuelve a cargar la vista con la lista de roles
                ViewBag.listaRoles = new RolLN().ListaRol();
                return View(collection);
            }
        }

        // GET: Usuarios/ChangePassword/5
        public ActionResult ChangePassword(int id)
        {
            string controladora = "Usuarios";
            string metodo = "GetUserId";
            Usuario users = new Usuario();
            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                //establece el tipo de dato de tranferencia
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                usuario.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora + "?IdUsuario=" + id;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = usuario.DownloadString(new Uri(rutacompleta));

                // convierte los datos traidos por la api a tipo lista de usuarios
                users = JsonConvert.DeserializeObject<Usuario>(data);
            }
            List<Rol> listaRoles = new RolLN().ListaRol();
            listaRoles.Add(new Rol() { IdRol = 0, DesRol = "[Seleccione Rol...]" });
            ViewBag.listaRoles = listaRoles.OrderBy(r => r.IdRol).ToList();
            return View(users);
        }

        // POST: Usuarios/ChangePassword/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(int idUsuario, Usuario collection)
        {
            string controladora = "Usuarios"; // Nombre del controlador en la API
            try
            {
                using (WebClient cliente = new WebClient())
                {
                    cliente.Headers.Clear(); // Borra cabeceras anteriores
                    cliente.Headers[HttpRequestHeader.ContentType] = "application/json"; // Tipo de contenido JSON
                    cliente.Encoding = UTF8Encoding.UTF8; // Establecer la codificación de caracteres

                    //Usuario usuarioN = new Usuario(BuscaUsuarioId(idUsuario));
                    // Serializar el objeto Usuario a formato JSON
                    var usuarioJson = JsonConvert.SerializeObject(collection);

                    // Construir la URL completa del API para hacer el PUT
                    string url = RutaApi + controladora + "/" + idUsuario;

                    // Hacer la llamada PUT al API
                    var resultado = cliente.UploadString(new Uri(url), "PUT", usuarioJson);
                }

                // Redirigir a la acción "Index" después de actualizar el usuario
                return RedirectToAction("Index");
            }
            catch
            {
                // Si ocurre un error, vuelve a cargar la vista con la lista de roles
                ViewBag.listaRoles = new RolLN().ListaRol();
                return View(collection);
            }
        }
        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            string controladora = "Usuarios";
            string metodo = "GetUserId";
            Usuario users = new Usuario();
            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                //establece el tipo de dato de tranferencia
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                usuario.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora + "?IdUsuario=" + id;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = usuario.DownloadString(new Uri(rutacompleta));

                // convierte los datos traidos por la api a tipo lista de usuarios
                users = JsonConvert.DeserializeObject<Usuario>(data);
            }
            return View(users);
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string controladora = "Usuarios"; // Nombre del controlador en la API
            try
            {
                using (WebClient cliente = new WebClient())
                {
                    cliente.Headers.Clear(); // Borra cabeceras anteriores
                    cliente.Headers[HttpRequestHeader.ContentType] = "application/json"; // Tipo de contenido JSON
                    cliente.Encoding = UTF8Encoding.UTF8; // Establecer la codificación de caracteres
                    // Serializar el objeto Usuario a formato JSON
                    var usuarioJson = JsonConvert.SerializeObject(collection);

                    // Construir la URL completa del API para hacer el PUT
                    string url = RutaApi + controladora + "/" + id;

                    // Hacer la llamada PUT al API
                    var resultado = cliente.UploadString(new Uri(url), "DELETE", usuarioJson);
                }

                // Redirigir a la acción "Index" después de actualizar el usuario
                return RedirectToAction("Index");
            }
            catch
            {
                // Si ocurre un error, vuelve a cargar la vista con la lista de roles
                ViewBag.listaRoles = new RolLN().ListaRol();
                return View(collection);
            }
        }
    }
}

