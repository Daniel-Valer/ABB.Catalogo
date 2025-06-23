using ABB.Catalogo.Entidades.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ABB.Catalogo.Entidades
{
    public class VariablesWeb
    {
        public static List<Opcion> gOpciones
        {
            get
            {
                if (HttpContext.Current.Session["gOpciones"] != null)
                {
                    return (List<Opcion>)HttpContext.Current.Session["gOpciones"];
                }
                return null;
            }

            set
            {
                HttpContext.Current.Session["gOpciones"] = value;
            }
        }
        public static Usuario gUsuario
        {
            get
            {
                if (HttpContext.Current.Session["gUsuario"] != null)
                {
                    return (Usuario)HttpContext.Current.Session["gUsuario"];
                }
                return null;
            }
            set { HttpContext.Current.Session["gUsuario"] = value; }
        }


    }
}
