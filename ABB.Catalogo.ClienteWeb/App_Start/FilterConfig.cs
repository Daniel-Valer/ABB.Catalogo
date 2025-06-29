using System.Web;
using System.Web.Mvc;
using ABB.Catalogo.Entidades.Filter;

namespace ABB.Catalogo.ClienteWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionFilterAtributes());// esto hace que el filtro de excepciones funcione en todaslas clases
        }

    }
}
