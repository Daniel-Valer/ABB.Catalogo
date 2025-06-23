using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using ABB.Catalogo.Entidades.Core;

namespace ABB.Catalogo.Utiles.Helpers
{
    public static class MenuHelper
    {
        public static MvcHtmlString HelperMenu(this HtmlHelper helper, List<Opcion> listaOpciones, string titulo)
        {

            //.................................................
            // final eleaborado por miguel algoritmo recursivo para construir el muenu.
            //////////////////////////////////////////////////
            var principal = new TagBuilder("ul");
            principal.AddCssClass("sidebar-menu");
            principal.Attributes.Add("data-widget", "tree");

            var liHeader = new TagBuilder("li");
            liHeader.AddCssClass("header");
            liHeader.InnerHtml = titulo;
            principal.InnerHtml += liHeader.ToString();

            if (listaOpciones == null || listaOpciones.Count() == 0)
            {
                return new MvcHtmlString(principal.ToString());
            }
            bool tieneHijo = false;

            foreach (Opcion item in listaOpciones
                .Where(t => t.IdOpcionRef == 0)
                .OrderBy(r => r.NroOrden).ToList())
            {
                tieneHijo = listaOpciones.Where(t => t.IdOpcionRef == item.IdOpcion).Any();

                var itemLista = new TagBuilder("li");
                if (tieneHijo)
                {
                    itemLista.AddCssClass("treeview");
                }

                var linkLista = new TagBuilder("a");
                linkLista.Attributes["href"] = GeneraHRef(helper, item);

                var iLista = new TagBuilder("i");
                iLista.AddCssClass(item.RutaImagen);
                linkLista.InnerHtml += iLista.ToString();

                var spanLista = new TagBuilder("span");
                spanLista.InnerHtml = item.NombreOpcion;
                linkLista.InnerHtml += spanLista;

                if (tieneHijo)
                {
                    var spanHijo = new TagBuilder("span");
                    spanHijo.AddCssClass("pull-right-container");

                    var iHijo = new TagBuilder("i");
                    iHijo.AddCssClass("fa fa-angle-left pull-right");
                    spanHijo.InnerHtml += iHijo.ToString();

                    linkLista.InnerHtml += spanHijo;
                }


                itemLista.InnerHtml += linkLista.ToString();
                if (tieneHijo)
                {
                    LlenarOpcionMenu(itemLista, item, listaOpciones, helper);
                }


                principal.InnerHtml += itemLista.ToString();
            }

            return new MvcHtmlString(principal.ToString());


        }

        //  Lazy patre recursiva del menu
        private static void LlenarOpcionMenu(TagBuilder itemLista, Opcion item, List<Opcion> listaOpciones, HtmlHelper helper)
        {
            var ulHijo = new TagBuilder("ul");
            ulHijo.AddCssClass("treeview-menu");

            foreach (Opcion itemOpcion in (from x in listaOpciones
                                           where x.IdOpcionRef == item.IdOpcion
                                           select x).ToList())
            {

                bool tieneHijo = (from x in listaOpciones
                                  where x.IdOpcionRef == itemOpcion.IdOpcion
                                  select x).Any();

                var liHijo = new TagBuilder("li");
                if (tieneHijo)
                {
                    liHijo.AddCssClass("treeview");
                }

                var aHijo = new TagBuilder("a");
                aHijo.Attributes["href"] = GeneraHRef(helper, itemOpcion);

                var iHijo = new TagBuilder("i");
                iHijo.AddCssClass(itemOpcion.RutaImagen);
                aHijo.InnerHtml += iHijo;

                aHijo.InnerHtml += itemOpcion.NombreOpcion;

                if (tieneHijo)
                {
                    var spanHijo = new TagBuilder("span");
                    spanHijo.AddCssClass("pull-right-container");

                    var iHijo2 = new TagBuilder("i");
                    iHijo2.AddCssClass("fa fa-angle-left pull-right");
                    spanHijo.InnerHtml += iHijo2.ToString();

                    aHijo.InnerHtml += spanHijo;
                }

                liHijo.InnerHtml += aHijo.ToString();

                if (tieneHijo)
                {
                    LlenarOpcionMenu(liHijo, itemOpcion, listaOpciones, helper);
                }

                ulHijo.InnerHtml += liHijo.ToString();

            }

            itemLista.InnerHtml += ulHijo.ToString();


        }


        private static string GeneraHRef(HtmlHelper helper, Opcion item)
        {
            string rutaUrl = string.Empty;
            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            if (!string.IsNullOrEmpty(item.UrlOpcion) && item.UrlOpcion != "#")
            {
                string[] ruta = item.UrlOpcion.Split('/');
                switch (ruta.Count())
                {
                    case 1:
                        rutaUrl = urlHelper.Action("Index", ruta[0]);
                        break;
                    case 2:
                        if (ruta[1].Split('?').Count() > 1)
                        {
                            rutaUrl = urlHelper.Action(ruta[1].Split('?')[0], ruta[0], RetornaObjetoParametros(ruta[1].Split('?')[1]));
                        }
                        else
                        {
                            rutaUrl = urlHelper.Action(ruta[1], ruta[0]);
                        }

                        break;
                    case 3:
                        if (ruta[2].Split('?').Count() > 1)
                        {
                            rutaUrl = urlHelper.Action(ruta[2].Split('?')[0], ruta[0] + "/" + ruta[1], RetornaObjetoParametros(ruta[2].Split('?')[1]));
                        }
                        else
                        {
                            rutaUrl = urlHelper.Action(ruta[2], ruta[0] + "/" + ruta[1]);
                        }

                        break;
                    default:
                        rutaUrl = item.UrlOpcion;
                        break;
                }

            }
            else
            {
                rutaUrl = "#";
            }

            return rutaUrl;

        }
        private static RouteValueDictionary RetornaObjetoParametros(string parametro)
        {
            RouteValueDictionary rvd = new RouteValueDictionary();

            string[] parametros = parametro.Split('&');
            Dictionary<string, string> lista = new Dictionary<string, string>();
            foreach (string item in parametros)
            {
                rvd.Add(item.Split('=')[0], item.Split('=')[1]);
                //lista.Add(item.Split('=')[0], item.Split('=')[1]);
            }
            return rvd;
        }
    }

}

