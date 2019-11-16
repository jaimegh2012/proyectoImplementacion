using EducacionAvanzada.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducacionAvanzada.WebAdmin.Controllers
{
    [Authorize]
    public class NotasDetalleController : Controller
    {
        NotasBL _notaBL;
        AlumnosBL _alumnosBL;
        materiaBL _MateriaBL;
        

        public NotasDetalleController()
        {
            _notaBL = new NotasBL();
            _alumnosBL = new AlumnosBL();
            _MateriaBL = new materiaBL();
        }
        // GET: NotasDetalle
        public ActionResult Index(int notaId, int alumnoId)
        {
            //var Notas = _notaBL.ObtenerNota(notaId);
            //Notas.ListadeNotasDetalle = _notaBL.ObtenerNotasDetalle(id);
            ViewBag.notaId = notaId;
            var Notas = _alumnosBL.ObtenerAlumno(alumnoId);
            Notas.ListadeNotas = _notaBL.ObtenerNotasporAlumno(alumnoId, notaId);
            return View(Notas);
        }

        public ActionResult Crear(int notaId, int alumnoId)   
        {
            var nuevaNotasDetalle = new NotasDetalle();
            nuevaNotasDetalle.NotaId = notaId;

            ViewBag.notaId = notaId;
            ViewBag.idalumno = alumnoId;

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

                    return View(notasDetalle);
                }

                if (notasDetalle.MateriaId == 0)
                {
                    ModelState.AddModelError("MateriaId", "Seleccione una Materia");
                    ViewBag.AlumnoId = new SelectList(alumnos, "Id", "Nombre");
                    ViewBag.MateriaId = new SelectList(ObtenerMateriasNoCalificadas(notasDetalle.NotaId, notasDetalle.AlumnoId), "Id", "Descripcion");
                    ViewBag.idalumno = notasDetalle.AlumnoId;
                    return View(notasDetalle);
                }

                _notaBL.GuardarNotasDetalle(notasDetalle);
                return RedirectToAction("Index", new {notaId = notasDetalle.NotaId, alumnoId = notasDetalle.AlumnoId });
            }

            ViewBag.AlumnoId = new SelectList(alumnos, "Id", "Nombre");
            ViewBag.MateriaId = new SelectList(ObtenerMateriasNoCalificadas(notasDetalle.NotaId, notasDetalle.AlumnoId), "Id", "Descripcion");

            return View(notasDetalle);
        }

        public ActionResult Editar(int notaDetalleId, int alumnoId, int materiaId)
        {
            var notaDetalle = _notaBL.ObtenerNotasDetalleporId(notaDetalleId);
            var alumnos = _alumnosBL.ObtenerAlumnos(alumnoId);
            var materias = _MateriaBL.Obtenermaterias(materiaId);


            ViewBag.AlumnoId = new SelectList(alumnos, "Id", "Nombre", notaDetalle.AlumnoId);
            ViewBag.MateriaId = new SelectList(materias, "Id", "Descripcion", notaDetalle.MateriaId);
          


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

                    return View(notaDetalle);
                }

                _notaBL.GuardarNotasDetalle(notaDetalle, true);

                return RedirectToAction("Index",new { notaId = notaDetalle.NotaId, alumnoId=notaDetalle.AlumnoId});
            }

            ViewBag.AlumnoId = new SelectList(alumnos, "Id", "Nombre");
            ViewBag.MateriaId = new SelectList(materias, "Id", "Descripcion");
            return View(notaDetalle);
        }


        
        public ActionResult Eliminar(int id)
        {
            var notaDetalle = _notaBL.ObtenerNotasDetalleporId(id);
            ViewBag.idealumno = notaDetalle.AlumnoId;
            return View(notaDetalle);
        }
        [HttpPost]
        public ActionResult Eliminar(NotasDetalle notaDetalle)
        {
            ViewBag.idealumno = notaDetalle.AlumnoId;
            var nota = notaDetalle.NotaId;
            var alumno = notaDetalle.AlumnoId;

            _notaBL.EliminarNotaDetalle(notaDetalle.Id);
            
            return RedirectToAction("Index", new { notaId = nota, alumnoId = alumno });
        }

        public List<Materia> ObtenerMateriasNoCalificadas(int notaId, int alumnoId)
        {
            var materias =_MateriaBL.Obtenermaterias();

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