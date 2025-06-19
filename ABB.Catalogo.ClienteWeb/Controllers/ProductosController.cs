using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ABB.Catalogo.ClienteWeb.Controllers
{
    public class ProductosController : Controller
    {
        string RutaApi = "https://localhost:44318/Api/";
        //string RutaApi = "http://localhost/WebServiceAbb/Api/"; //define la ruta del web api
        string jsonMediaType = "application/json"; // define el tipo de dat
        // GET: Productos
        public ActionResult Index()
        {
            string controladora = "Productos";
            TokenResponse tokenrsp = Respuest();
            List<Producto> listaProductos = new List<Producto>();
            using (WebClient producto = new WebClient())
            {
                producto.Headers.Clear();//borra datos anteriores
                //establece el tipo de dato de tranferencia
                producto.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenrsp.Token;
                producto.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                producto.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = producto.DownloadString(new Uri(rutacompleta));
                // convierte los datos traidos por la api a tipo lista de productos
                listaProductos = JsonConvert.DeserializeObject<List<Producto>>(data);
            }
            return View(listaProductos);
        }

        // GET: Productos/Details/5
        public ActionResult Details(int id)
        {
            string controladora = "Productos";
            TokenResponse tokenrsp = Respuest(); // Método que recupera el token
            Producto producto = new Producto();

            using (WebClient client = new WebClient())
            {
                client.Headers.Clear();
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenrsp.Token;
                client.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                client.Encoding = UTF8Encoding.UTF8;

                string rutacompleta = RutaApi + controladora + "?IdProducto=" + id;
                var data = client.DownloadString(new Uri(rutacompleta));
                producto = JsonConvert.DeserializeObject<Producto>(data);
            }
            Categoria categoria = new CategoriaLN().ListaCategoria().FirstOrDefault(c => c.IdCategoria == producto.IdCategoria);
            ViewBag.NombreCategoria = categoria != null ? categoria.DescCategoria : "Sin categoría";
            return View(producto);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            Producto producto = new Producto();// se crea una instancia de la clase producto
            List<Categoria> listacategoria = new List<Categoria>();
            listacategoria = new CategoriaLN().ListaCategoria();
            listacategoria.Add(new Categoria() { IdCategoria = 0, DescCategoria = "[Seleccione Categoria...]" });
            ViewBag.listaCategorias = listacategoria;
            return View(producto);
        }

        // POST: Productos/Create
        [HttpPost]
        public ActionResult Create(Producto collection)
        {
            string controladora = "Productos";
            try
            {
                // TODO: Add insert logic here
                TokenResponse tokenrsp = Respuest();
                using (WebClient producto = new WebClient())
                {
                    producto.Headers.Clear();//borra datos anteriores
                    //establece el tipo de dato de tranferencia
                    producto.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenrsp.Token;
                    producto.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    //typo de decodificador reconocimiento carecteres especiales
                    producto.Encoding = UTF8Encoding.UTF8;
                    //convierte el objeto de tipo Productos a una trama Json
                    var productoJson = JsonConvert.SerializeObject(collection);
                    string rutacompleta = RutaApi + controladora;
                    var resultado = producto.UploadString(new Uri(rutacompleta), productoJson);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int id)
        {
            string controladora = "Productos";
            TokenResponse tokenrsp = Respuest(); // <--- AÑADIR
            Producto products = new Producto();
            using (WebClient producto = new WebClient())
            {
                producto.Headers.Clear();
                producto.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenrsp.Token; // <--- AÑADIR
                producto.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                producto.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora + "?IdProducto=" + id;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = producto.DownloadString(new Uri(rutacompleta));

                // convierte los datos traidos por la api a tipo lista de productos
                products = JsonConvert.DeserializeObject<Producto>(data);
            }
            List<Categoria> listaCategorias = new CategoriaLN().ListaCategoria();
            listaCategorias.Add(new Categoria() { IdCategoria = 0, DescCategoria = "[Seleccione Categoria...]" });
            ViewBag.listaCategorias = listaCategorias.OrderBy(c => c.IdCategoria).ToList();
            return View(products);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        public ActionResult Edit(int idProducto, Producto collection)
        {
            string controladora = "Productos"; // Nombre del controlador en la API
            try
            {
                TokenResponse tokenrsp = Respuest(); // <--- AÑADIR

                using (WebClient cliente = new WebClient())
                {
                    cliente.Headers.Clear();
                    cliente.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenrsp.Token; // <--- AÑADIR
                    cliente.Headers[HttpRequestHeader.ContentType] = "application/json"; // Tipo de contenido JSON
                    cliente.Encoding = UTF8Encoding.UTF8; // Establecer la codificación de caracteres

                    // Serializar el objeto Producto a formato JSON
                    var productoJson = JsonConvert.SerializeObject(collection);

                    // Construir la URL completa del API para hacer el PUT
                    string url = RutaApi + controladora + "/" + idProducto;

                    // Hacer la llamada PUT al API
                    var resultado = cliente.UploadString(new Uri(url), "PUT", productoJson);
                }

                // Redirigir a la acción "Index" después de actualizar el usuario
                return RedirectToAction("Index");
            }
            catch
            {
                // Si ocurre un error, vuelve a cargar la vista con la lista de roles
                ViewBag.listaCategorias = new CategoriaLN().ListaCategoria();
                return View(collection);
            }
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int id)
        {
            string controladora = "Productos";
            TokenResponse tokenrsp = Respuest(); // <--- AÑADIR
            Producto products = new Producto();
            using (WebClient producto = new WebClient())
            {
                producto.Headers.Clear();
                producto.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenrsp.Token; // <--- AÑADIR
                producto.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                producto.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora + "?IdProducto=" + id;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = producto.DownloadString(new Uri(rutacompleta));

                // convierte los datos traidos por la api a tipo lista de productos
                products = JsonConvert.DeserializeObject<Producto>(data);
            }
            return View(products);
        }

        // POST: Productos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Producto collection)
        {
            string controladora = "Productos"; // Nombre del controlador en la API
            try
            {
                TokenResponse tokenrsp = Respuest(); // <--- AÑADIR

                using (WebClient cliente = new WebClient())
                {
                    cliente.Headers.Clear();
                    cliente.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenrsp.Token; // <--- AÑADIR
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
                ViewBag.listaCategoria = new CategoriaLN().ListaCategoria();
                return View(collection);
            }
        }
        private TokenResponse Respuest()
        {
            TokenResponse respuesta = new TokenResponse();
            string controladora = "Auth";
            //string metodo = "Post";
            var resultado = "";
            UsuariosApi usuapi = new UsuariosApi();
            usuapi.Codigo = Convert.ToInt32(ConfigurationManager.AppSettings["UsuApiCodigo"]);
            usuapi.UserName = ConfigurationManager.AppSettings["UsuApiUserName"];
            usuapi.Clave = ConfigurationManager.AppSettings["UsuApiClave"];
            usuapi.Nombre = ConfigurationManager.AppSettings["UsuApiNombre"];
            usuapi.Rol = ConfigurationManager.AppSettings["UsuApiRol"];
            using (WebClient usuarioapi = new WebClient())
            {
                usuarioapi.Headers.Clear();//borra datos anteriores
                //establece el tipo de dato de tranferencia
                usuarioapi.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                usuarioapi.Encoding = UTF8Encoding.UTF8;
                //convierte el objeto de tipo Usuarios a una trama Json
                var usuarioJson = JsonConvert.SerializeObject(usuapi);
                string rutacompleta = RutaApi + controladora;
                resultado = usuarioapi.UploadString(new Uri(rutacompleta), usuarioJson);
                respuesta = JsonConvert.DeserializeObject<TokenResponse>(resultado);
            }
            return respuesta;
        }
    }
}
