using EducacionAvanzada.BL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducacionAvanzada.WebProfesor.Controllers
{
    public class AlumnosPorGradoController : Controller
    {
        AlumnosBL _alumnosBL;

        public AlumnosPorGradoController()
        {
            _alumnosBL = new AlumnosBL();
        }
        // GET: AlumnosPorGrado
        public ActionResult Index(int gradoId, int seccionId, int jornadaId, int notaId, int profesorId)
        {
            ViewBag.notaId = notaId;
            ViewBag.profesorId = profesorId;

            ViewBag.adminWebsiteUrl =
                ConfigurationManager.AppSettings["adminWebsiteUrl"];

            var listadeAlumnos = _alumnosBL.ObtenerAlumnosPorGrado(gradoId, seccionId, jornadaId);

            return View(listadeAlumnos);
        }
    }
}