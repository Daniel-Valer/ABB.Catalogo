using ABB.Catalogo.Entidades.Base;
using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.AccesoDatos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.Catalogo.LogicaNegocio.Core
{
    public class UsuarioApiLN : BaseLN
    {
        public UsuariosApi BuscaUsuarioApi(UsuariosApi PamUsuarioApi)
        {
            return new UsuarioApiAD().BuscaUsuarioApi(PamUsuarioApi);
        }

    }
}
