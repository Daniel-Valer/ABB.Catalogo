using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;

namespace WebServicesAbb.Controllers
{
    [Authorize]
    public class ProductosController : ApiController
    {
        // GET: api/Productos
        [HttpGet]
        public IEnumerable<Producto> Get()
        {
            List<Producto> catalogo = new List<Producto>();
            catalogo = new ProductoLN().ListarProductos();

            return catalogo;
        }
        [HttpGet]
        // public Producto GetProductId([FromUri] int IdProducto)
        public IHttpActionResult GetProductId([FromUri] int IdProducto)
        {
            if (IdProducto <= 0)
            {
                return BadRequest("el Id debe ser mayor que 0");
            }

            try
            {
                Producto pro = new Producto();
                ProductoLN producto = new ProductoLN();
                pro = producto.BuscaProductoId(IdProducto);
                return Ok(pro);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                throw;
            }


        }

        // POST: api/Productos
        [HttpPost]
        public IHttpActionResult Post([FromBody] Producto producto)
        {
            if (producto.IdCategoria <= 0)
                return BadRequest("IdCategoria invalido");
            if(producto.NomProducto == null || producto.NomProducto.Trim() == "")
                return BadRequest("Nombre del producto es obligatorio");
            if (producto.Precio < 0)
                return BadRequest("el precio debe ser mayor o igual a cero");
            if (producto.LineaProducto == null || producto.LineaProducto.Trim() == "")
                return BadRequest("LineaProducto del producto es obligatorio");
            if (producto.MarcaProducto == null || producto.MarcaProducto.Trim() == "")
                return BadRequest("MarcaProducto del producto es obligatorio");
            if (producto.ModeloProducto == null || producto.ModeloProducto.Trim() == "")
                return BadRequest("ModeloProducto del producto es obligatorio");
            Producto resultado = new ProductoLN().InsertarProducto(producto);
            return Ok(resultado);
        }


        // PUT: api/Productos/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Producto producto)
        {
            if (id <= 0 || producto == null)
                return BadRequest("Datos inválidos");

            Producto actualizado = new ProductoLN().ModificarProducto(id, producto);
            return Ok(actualizado);
        }


        // DELETE: api/Productos/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id no válido");

            bool eliminado = new ProductoLN().Eliminar(id);
            if (!eliminado)
                return NotFound();

            return Ok("Producto eliminado correctamente");
        }

    }
}
