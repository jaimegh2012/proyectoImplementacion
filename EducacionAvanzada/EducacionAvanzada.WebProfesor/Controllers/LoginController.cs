using EducacionAvanzada.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EducacionAvanzada.WebProfesor.Controllers
{
    public class LoginController : Controller
    {
        SeguridadBL _seguridad;
        ProfesorBL _profesorBL;

        public LoginController()
        {
            _seguridad = new SeguridadBL();
            _profesorBL = new ProfesorBL();
        }

        // GET: Login
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection data)
        {
            var nombreUsuario = data["username"];
            var contrasena = data["password"];

            var usuarioValido = _seguridad
                .AutorizarProfesor(nombreUsuario, contrasena);


            var profesorExistente = _profesorBL.ObtenerProfesor(nombreUsuario);

            if (usuarioValido)
            {
                FormsAuthentication.SetAuthCookie(nombreUsuario, true);

                return RedirectToAction("Index", "Notas", new { id = profesorExistente.Id });
            }

            ModelState.AddModelError("", "Usuario o contraseña invalido");

            return View();
        }
    }
}