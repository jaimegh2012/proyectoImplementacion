using EducacionAvanzada.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducacionAvanzada.WebAdmin.Controllers
{
    [Authorize]
    public class AlumnosPorGradoController : Controller
    {
        AlumnosBL _alumnosBL;
        public AlumnosPorGradoController()
        {
            _alumnosBL = new AlumnosBL();
        }
        // GET: AlumnosPorGrado
        public ActionResult Index(int gradoId, int seccionId, int jornadaId, int notaId)
        {
            //ViewBag.gradoId = gradoId;
            //ViewBag.seccionId = seccionId;

            //int grado = Convert.ToInt32(gradoId);
            //int seccion = Convert.ToInt32(seccionId);
            ViewBag.notaId = notaId;
            var listadeAlumnos = _alumnosBL.ObtenerAlumnosPorGrado(gradoId, seccionId, jornadaId);

            return View(listadeAlumnos);
        }
    }
}