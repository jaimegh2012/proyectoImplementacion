using EducacionAvanzada.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducacionAvanzada.WebAdmin.Controllers
{
    [Authorize]
    public class MateriaDetalleController : Controller
    { 
        GradosBL _gradoBL;
        materiaBL _materiaBL;

        public MateriaDetalleController()
        {
            _gradoBL = new GradosBL();
            _materiaBL = new materiaBL();
        }

        // GET: MateriaDetalle
        public ActionResult Index(int gradoDetalleId)
        {
            var GradoDetalle = _gradoBL.ObtenerGradoDetalle(gradoDetalleId);
            GradoDetalle.ListadeMateriasPorGradoyProfesor = _materiaBL.ObtenerMateriasPorGradoyProfesor(gradoDetalleId);
           
            ViewBag.gradoDetalleId = gradoDetalleId;

            return View(GradoDetalle);
        }

        public ActionResult Crear(int gradoDetalleId)
        {
            var nuevamateriaDetalle = new MateriaDetalle();
            nuevamateriaDetalle.GradoDetalleId = gradoDetalleId;

            var materias = _materiaBL.Obtenermaterias();
            
            

            //ViewBag.idalumno = alumnoId;


            ViewBag.MateriaId = new SelectList(materias, "Id", "Descripcion");
            

            return View(nuevamateriaDetalle);
        }

        [HttpPost]
        public ActionResult Crear(MateriaDetalle materiaDetalle)
        {
            var materias = _materiaBL.Obtenermaterias();

            if (ModelState.IsValid)
            {

                if (materiaDetalle.MateriaId == 0)
                {
                    ModelState.AddModelError("MateriaId", "Seleccione una Materia");
                    ViewBag.MateriaId = new SelectList(materias, "Id", "Descripcion");
                    return View(materiaDetalle);
                }

               _materiaBL.GuardarMateriaDetalle(materiaDetalle);
                return RedirectToAction("Index", new { gradoDetalleId = materiaDetalle.GradoDetalleId });
            }

            ViewBag.MateriaId = new SelectList(materias, "Id", "Descripcion");

            return View(materiaDetalle);
        }

        public ActionResult Editar(int id)
        {
            var materiaDetalle = _materiaBL.ObtenerMateriaDetalle(id);

            var materias = _materiaBL.Obtenermaterias();
            

            ViewBag.MateriaId = new SelectList(materias, "Id", "Descripcion");
            

            return View(materiaDetalle);
        }

        [HttpPost]
        public ActionResult Editar(MateriaDetalle materiaDetalle)
        {
            _materiaBL.GuardarMateriaDetalle(materiaDetalle);

            return RedirectToAction("Index", new { gradoDetalleId = materiaDetalle.GradoDetalleId});
        }

        public ActionResult Eliminar(int id)
        {
            var materiaDetalle = _materiaBL.ObtenerMateriaDetalle(id);

            return View(materiaDetalle);
        }

        [HttpPost]
        public ActionResult Eliminar(MateriaDetalle materiaDetalle)
        {
            var gradoDetalle = materiaDetalle.GradoDetalleId;

            _materiaBL.EliminarMateriaDetalle(materiaDetalle.Id);

            return RedirectToAction("Index", new { gradoDetalleId = gradoDetalle });
        }

    }
}