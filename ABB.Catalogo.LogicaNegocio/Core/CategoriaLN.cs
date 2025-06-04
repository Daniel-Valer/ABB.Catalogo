using ABB.Catalogo.AccesoDatos.Core;
using ABB.Catalogo.Entidades.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.Catalogo.LogicaNegocio.Core
{
    public class CategoriaLN
    {
        public List<Categoria> ListaCategoria()
        {
            try
            {
                CategoriaDA categorias = new CategoriaDA();
                return categorias.ListaCategoria();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
