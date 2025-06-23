using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.Catalogo.Entidades.Core
{
    public class Opcion
    {
        public int IdOpcion { get; set; }
        public string NombreOpcion { get; set; }
        public string UrlOpcion { get; set; }
        public string RutaImagen { get; set; }
        public int NroOrden { get; set; }
        public int IdOpcionRef { get; set; }
        // adicionales para deferenciar el controlador y el metodo en el string UrlOpcion
        public string Controladora { get; set; }
        public string Accion { get; set; }
        public string Area { get; set; }
    }
}
