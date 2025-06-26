using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.Utiles.Helpers;
using System.Security;

namespace ABB.Catalogo.ClienteWeb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            Usuario user = new Usuario();
            return View(user);
        }
    }
}
