using EducacionAvanzada.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducacionAvanzada.WebProfesor.Controllers
{
    [Authorize]
    public class NotasDetalleController : Controller
    {
        AlumnosBL _alumnosBL;
        NotasBL _notaBL;
        materiaBL _MateriaBL;


        public NotasDetalleController()
        {
            _alumnosBL = new AlumnosBL();
            _notaBL = new NotasBL();
            _MateriaBL = new materiaBL();
        }

        // GET: NotasDetalle
        public ActionResult Index(int notaId, int alumnoId, int profesorId)
        {
            ViewBag.notaId = notaId;
            ViewBag.profesorId = profesorId;
            ViewBag.alumnoid = alumnoId;

            //var nota = new Alumno();
            
            
            var nota = _alumnosBL.ObtenerAlumno(alumnoId);

            nota.ListadeNotas = _notaBL.ObtenerNotasporAlumno(alumnoId, notaId);
            
            return View(nota);
            
        }

        //public int profeId;
        public ActionResult Crear(int notaId, int alumnoId, int profesorId)
        {
            var nuevaNotasDetalle = new NotasDetalle();
            nuevaNotasDetalle.NotaId = notaId;
            nuevaNotasDetalle.ProfesorId = profesorId;
            nuevaNotasDetalle.AlumnoId = alumnoId;
            

            ViewBag.notaId = notaId;
            ViewBag.idalumno = alumnoId;
            ViewBag.profeId = profesorId;
            //profeId = profesorId;

            var alumnos = _alumnosBL.ObtenerAlumnos(alumnoId);
            ViewBag.AlumnoId = new SelectList(alumnos, "Id", "Nombre");

            var materias = _MateriaBL.Obtenermaterias();

            ViewBag.MateriaId = new SelectList(ObtenerMateriasNoCalificadas(notaId, alumnoId), "Id", "Descripcion");


            return View(nuevaNotasDetalle);
        }


        [HttpPost]
        public ActionResult Crear(NotasDetalle notasDetalle)
        {
            var alumnos = _alumnosBL.ObtenerAlumnos(notasDetalle.AlumnoId);

            if (ModelState.IsValid)
            {


                if (notasDetalle.AlumnoId == 0)
                {
                    ModelState.AddModelError("AlumnoId", "Seleccione un Alumno");
                    ViewBag.AlumnoId = new SelectList(alumnos, "Id", "Nombre");
                    ViewBag.MateriaId = new SelectList(ObtenerMateriasNoCalificadas(notasDetalle.NotaId, notasDetalle.AlumnoId), "Id", "Descripcion");
                    ViewBag.idalumno = notasDetalle.AlumnoId;
                    ViewBag.profeId = notasDetalle.ProfesorId;

                    return View(notasDetalle);
                }

                if (notasDetalle.MateriaId == 0)
                {
                    ModelState.AddModelError("MateriaId", "Seleccione una Materia");
                    ViewBag.AlumnoId = new SelectList(alumnos, "Id", "Nombre");
                    ViewBag.MateriaId = new SelectList(ObtenerMateriasNoCalificadas(notasDetalle.NotaId, notasDetalle.AlumnoId), "Id", "Descripcion");
                    ViewBag.idalumno = notasDetalle.AlumnoId;
                    ViewBag.profeId = notasDetalle.ProfesorId;

                    return View(notasDetalle);
                }

                _notaBL.GuardarNotasDetalle(notasDetalle);
                return RedirectToAction("Index", new { notaId = notasDetalle.NotaId, alumnoId = notasDetalle.AlumnoId, profesorId = notasDetalle.ProfesorId});
            }

            ViewBag.AlumnoId = new SelectList(alumnos, "Id", "Nombre");
            ViewBag.MateriaId = new SelectList(ObtenerMateriasNoCalificadas(notasDetalle.NotaId, notasDetalle.AlumnoId), "Id", "Descripcion");
            ViewBag.idalumno = notasDetalle.AlumnoId;
            ViewBag.profeId = notasDetalle.ProfesorId;

            return View(notasDetalle);
        }

        

        public ActionResult Editar(int notaDetalleId, int alumnoId, int materiaId, int profesorId)
        {
            var notaDetalle = _notaBL.ObtenerNotasDetalleporId(notaDetalleId);
            var alumnos = _alumnosBL.ObtenerAlumnos(alumnoId);
            var materias = _MateriaBL.Obtenermaterias(materiaId);

            notaDetalle.ProfesorId = profesorId;


            ViewBag.AlumnoId = new SelectList(alumnos, "Id", "Nombre", notaDetalle.AlumnoId);
            ViewBag.MateriaId = new SelectList(materias, "Id", "Descripcion", notaDetalle.MateriaId);

            ViewBag.profeId = profesorId;



            return View(notaDetalle);
        }

        [HttpPost]
        public ActionResult Editar(NotasDetalle notaDetalle)
        {
            var alumnos = _alumnosBL.ObtenerAlumnos(notaDetalle.AlumnoId);
            var materias = _MateriaBL.Obtenermaterias(notaDetalle.MateriaId);

            if (ModelState.IsValid)
            {


                if (notaDetalle.AlumnoId == 0 || notaDetalle.MateriaId == 0)
                {
                    if (notaDetalle.AlumnoId == 0)
                    {
                        ModelState.AddModelError("Alumno", "Seleccione un Alumno");
                    }

                    if (notaDetalle.MateriaId == 0)
                    {
                        ModelState.AddModelError("Materia", "Seleccione una Materia");
                    }

                    ViewBag.AlumnoId = new SelectList(alumnos, "Id", "Nombre");
                    ViewBag.MateriaId = new SelectList(materias, "Id", "Descripcion");
                    ViewBag.profeId = notaDetalle.ProfesorId;

                    return View(notaDetalle);
                }

                _notaBL.GuardarNotasDetalle(notaDetalle, true);

                return RedirectToAction("Index", new { notaId = notaDetalle.NotaId, alumnoId = notaDetalle.AlumnoId, profesorId = notaDetalle.ProfesorId });
            }

            ViewBag.AlumnoId = new SelectList(alumnos, "Id", "Nombre");
            ViewBag.MateriaId = new SelectList(materias, "Id", "Descripcion");
            ViewBag.profeId = notaDetalle.ProfesorId;

            return View(notaDetalle);
        }


        //FUNCION PARA BUSCAR MATERIAS NO CALIFICADAS AUN
        public List<Materia> ObtenerMateriasNoCalificadas(int notaId, int alumnoId)
        {
            var materias = _MateriaBL.Obtenermaterias();

            List<Materia> materiasNoCalificadas = new List<Materia>();
            foreach (var item in materias)
            {
                var notasDetalle = _notaBL.ObtenerNotasDetallePorAlumno(notaId, alumnoId, item.Id);

                if (notasDetalle == null)
                {
                    //List<Materia> materiasNoCalificadas = new List<Materia>();
                    materiasNoCalificadas.Add(new Materia() { Id = item.Id, Descripcion = item.Descripcion });
                }
            }

            return materiasNoCalificadas;
        }
    }
}