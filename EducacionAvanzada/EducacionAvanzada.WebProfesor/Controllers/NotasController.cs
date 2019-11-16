using EducacionAvanzada.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducacionAvanzada.WebProfesor.Controllers
{
    public class NotasController : Controller
    {
        NotasBL _notasBL;
        GradosBL _gradosBL;

        public NotasController()
        {
            _notasBL = new NotasBL();
            _gradosBL = new GradosBL();
        }
        // GET: Notas
        public ActionResult Index(int id)
        {
            ViewBag.profesorId = id;

            var listadeNotasparaProfesor = ObtenerNotasParaProfesor(id);

            return View(listadeNotasparaProfesor);
        }



        public List<Notas> ObtenerNotasParaProfesor(int profesorId)
        {
            var GradosImpartidos = _gradosBL.ObtenerGradosPorProfesor(profesorId);
            List<Notas> notasParaProfesor = new List<Notas>();
            foreach (var item in GradosImpartidos)
            {
                var nota = _notasBL.ObtenerNotaPorProfesor(item.GradoId, item.SeccionId, item.JornadaId, item.Anio);

                if (nota != null)
                {
                    //List<Materia> materiasNoCalificadas = new List<Materia>();
                    //notasParaProfesor.Add(new Notas() { Id = item.Id, GradoId = item.GradoId, SeccionId = item.SeccionId, JornadaId = item.JornadaId, Anio = item.Anio });
                    notasParaProfesor.Add(nota);

                }
            }

            return notasParaProfesor;
        }
    }
}