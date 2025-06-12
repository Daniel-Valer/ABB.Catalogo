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

        // GET: api/Productos/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Productos
        [HttpPost]
        public IHttpActionResult Post([FromBody] Producto producto)
        {
            if (producto == null)
                return BadRequest("El producto no puede ser nulo");

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
