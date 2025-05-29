using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.Catalogo.Entidades.Core
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string CodUsuario { get; set; }
        public string ClaveTxt { get; set; }
        public byte[] Clave { get; set; }
        public string Nombres { get; set; }
        public int IdRol { get; set; }
        public string DesRol { get; set; }

    }
}
