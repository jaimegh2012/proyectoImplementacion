using EducacionAvanzada.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducacionAvanzada.WebAdmin.Controllers
{
    [Authorize]
    public class GradoDetalleController : Controller
    {
        ProfesorBL _profesorBL;
        GradosBL _gradoBL;
        JornadasBL _jornadaBL;
        seccionBL _seccionBL;

        public GradoDetalleController()
        {
            _profesorBL = new ProfesorBL();
            _gradoBL = new GradosBL();
            _jornadaBL = new JornadasBL();
            _seccionBL = new seccionBL();
             
        }
        // GET: GradoDetalle
        public ActionResult Index(int profesorId)
        {
            //probando por un error que se ocasiono
            try
            {
                var Profesor = _profesorBL.ObtenerProfesor(profesorId);
                Profesor.ListadeGradosPorProfesor = _gradoBL.ObtenerGradosPorProfesor(profesorId);
                ViewBag.profesorId = profesorId;
                ViewBag.nombreProfesor = Profesor.Nombre;
                return View(Profesor);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { profesorId = profesorId });
            }

        }

        public ActionResult Crear(int profesorId)
        {
            var nuevoGradoDetalle = new GradoDetalle();
            nuevoGradoDetalle.ProfesorId = profesorId;

            var grados = _gradoBL.ObtenerGrados();
            var jornadas = _jornadaBL.ObtenerJornadas();
            var secciones = _seccionBL.ObtenerSecciones();

            var profesor = _profesorBL.ObtenerProfesorComoLista(profesorId);


            //ViewBag.notaId = notaId;

            //ViewBag.idalumno = alumnoId;


            ViewBag.GradoId = new SelectList(grados, "Id", "Descripcion");
            ViewBag.JornadaId = new SelectList(jornadas, "Id", "Descripcion");
            ViewBag.SeccionId = new SelectList(secciones, "Id", "Descripcion");

            ViewBag.ProfesorId = new SelectList(profesor, "Id", "Nombre");





            return View(nuevoGradoDetalle);
        }

        [HttpPost]
        public ActionResult Crear(GradoDetalle GradoDetalle)
        {
            var grados = _gradoBL.ObtenerGrados();
            var jornadas = _jornadaBL.ObtenerJornadas();
            var secciones = _seccionBL.ObtenerSecciones();
            var profesor = _profesorBL.ObtenerProfesorComoLista(GradoDetalle.ProfesorId);

            if (ModelState.IsValid)
            {

                if (GradoDetalle.GradoId == 0)
                {
                    ModelState.AddModelError("GradoId", "Seleccione un Grado");
                    ViewBag.GradoId = new SelectList(grados, "Id", "Descripcion");
                    ViewBag.JornadaId = new SelectList(jornadas, "Id", "Descripcion");
                    ViewBag.SeccionId = new SelectList(secciones, "Id", "Descripcion");
                    ViewBag.ProfesorId = new SelectList(profesor, "Id", "Nombre");
                    return View(GradoDetalle);
                }

                if (GradoDetalle.SeccionId == 0)
                {
                    ModelState.AddModelError("SeccionId", "Seleccione una Seccion");
                    ViewBag.GradoId = new SelectList(grados, "Id", "Descripcion");
                    ViewBag.JornadaId = new SelectList(jornadas, "Id", "Descripcion");
                    ViewBag.SeccionId = new SelectList(secciones, "Id", "Descripcion");
                    ViewBag.ProfesorId = new SelectList(profesor, "Id", "Nombre");
                    //ViewBag.idalumno = notasDetalle.AlumnoId;
                    return View(GradoDetalle);
                }

                if (GradoDetalle.JornadaId == 0)
                {
                    ModelState.AddModelError("JornadaId", "Seleccione una Jornada");
                    ViewBag.GradoId = new SelectList(grados, "Id", "Descripcion");
                    ViewBag.JornadaId = new SelectList(jornadas, "Id", "Descripcion");
                    ViewBag.SeccionId = new SelectList(secciones, "Id", "Descripcion");
                    ViewBag.ProfesorId = new SelectList(profesor, "Id", "Nombre");
                    return View(GradoDetalle);
                }

                _gradoBL.GuardarGradosDetalle(GradoDetalle);
                return RedirectToAction("Index", new { profesorId = GradoDetalle.ProfesorId});
            }

            ViewBag.GradoId = new SelectList(grados, "Id", "Descripcion");
            ViewBag.JornadaId = new SelectList(jornadas, "Id", "Descripcion");
            ViewBag.SeccionId = new SelectList(secciones, "Id", "Descripcion");
            ViewBag.ProfesorId = new SelectList(profesor, "Id", "Nombre");

            return View(GradoDetalle);
        }


        public ActionResult Editar(int id)
        {
            var gradoDetalle = _gradoBL.ObtenerGradoDetalle(id);

            var grados = _gradoBL.ObtenerGrados();
            var jornadas = _jornadaBL.ObtenerJornadas();
            var secciones = _seccionBL.ObtenerSecciones();
            var profesor = _profesorBL.ObtenerProfesorComoLista(gradoDetalle.ProfesorId);

            ViewBag.GradoId = new SelectList(grados, "Id", "Descripcion");
            ViewBag.JornadaId = new SelectList(jornadas, "Id", "Descripcion");
            ViewBag.SeccionId = new SelectList(secciones, "Id", "Descripcion");
            ViewBag.ProfesorId = new SelectList(profesor, "Id", "Nombre");


            return View(gradoDetalle);
        }

        [HttpPost]
        public ActionResult Editar(GradoDetalle gradoDetalle)
        {
            _gradoBL.GuardarGradosDetalle(gradoDetalle);

            return RedirectToAction("Index", new { profesorId = gradoDetalle.ProfesorId });
        }

        public ActionResult Eliminar(int id)
        {
            var gradoDetalle = _gradoBL.ObtenerGradoDetalle(id);

            return View(gradoDetalle);
        }

        [HttpPost]
        public ActionResult Eliminar(GradoDetalle gradoDetalle)
        {
            var profesor = gradoDetalle.ProfesorId;

            _gradoBL.EliminarGradoDetalle(gradoDetalle.Id);

            return RedirectToAction("Index", new { profesorId = profesor });
        }
    }
}