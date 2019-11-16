using EducacionAvanzada.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducacionAvanzada.WebAdmin.Controllers
{
    [Authorize]
    public class ProfesoresController : Controller
    {
        ProfesorBL _profesorBL;
        public ProfesoresController()
        {
            _profesorBL = new ProfesorBL();
        }

        // GET: Profesores
        public ActionResult Index()
        {
            var listadeProfesores = _profesorBL.ObtenerProfesores();
            return View(listadeProfesores);
        }

        public ActionResult Crear()
        {
            var nuevoProfesor = new Profesor();

            return View(nuevoProfesor);
        }

        [HttpPost]
        public ActionResult Crear(Profesor profesor)
        {
            if (ModelState.IsValid)
            {
                if (profesor.Nombre != profesor.Nombre.Trim())
                {
                    ModelState.AddModelError("Nombre", "No debe de haber espacios al inicio o al final");
                    return View(profesor);
                }
                profesor.Contrasena = Encriptar.CodificarContrasena(profesor.Contrasena);
                _profesorBL.GuardarProfesor(profesor);

                return RedirectToAction("Index");
            }

            return View(profesor);
        }

        public ActionResult Editar(int id)
        {
            var profesor = _profesorBL.ObtenerProfesor(id);


            return View(profesor);
        }

        [HttpPost]
        public ActionResult Editar(Profesor profesor)
        {
            _profesorBL.GuardarProfesor(profesor);

            return RedirectToAction("Index");
        }


    }
}