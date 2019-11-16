using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducacionAvanzada.WebProfesor.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.profeId = "hola";
            ViewBag.imagen = "/Imagenes/portada.jpg";
            ViewBag.adminWebsiteUrl =
                ConfigurationManager.AppSettings["adminWebsiteUrl"];

            return RedirectToAction("Index", "Login");
        }
    }
}