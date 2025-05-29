using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.Catalogo.Entidades.Core
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public int IdCategoria { get; set; }
        public string NomProducto { get; set; }
        public string MarcaProducto { get; set; }
        public string LineaProducto { get; set; }
        public string GarantiaProducto { get; set; }
        public decimal Precio { get; set; }
        
    }
}
