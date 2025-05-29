using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.Catalogo.Entidades.Core
{
    public class Stock
    {
        public int IdProducto { get; set; }
        public int StockItems { get; set; }
        public int PuntoRepo { get; set; }
        public decimal PrecioVenta { get; set; }

    }
}
