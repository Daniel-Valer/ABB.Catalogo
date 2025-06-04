using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ABB.Catalogo.ClienteWeb.Controllers
{
    public class ProductosController : Controller
    {
        string RutaApi = "https://localhost:44318/Api/"; //define la ruta del web api
        string jsonMediaType = "application/json"; // define el tipo de dat
        // GET: Productos
        public ActionResult Index()
        {
            string controladora = "Productos";
            string metodo = "Get";
            List<Producto> listaProductos = new List<Producto>();
            using (WebClient producto = new WebClient())
            {
                producto.Headers.Clear();//borra datos anteriores
                //establece el tipo de dato de tranferencia
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
            
            return View();
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
                using (WebClient producto = new WebClient())
                {
                    producto.Headers.Clear();//borra datos anteriores
                    //establece el tipo de dato de tranferencia
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
            return View();
        }

        // POST: Productos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Productos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
