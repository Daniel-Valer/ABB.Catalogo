﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABB.Catalogo.ClienteWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult PaginaPrueba()
        {
            return View();
        }
        public ActionResult Error500()
        {
            return View();
        }
        public ActionResult PaginaNoEncontrada()
        {
            return View();
        }

    }
}