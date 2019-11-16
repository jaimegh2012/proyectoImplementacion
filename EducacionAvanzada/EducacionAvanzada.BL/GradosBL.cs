using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducacionAvanzada.BL
{
    public class GradosBL
    {
        Contexto _contexto;

        public List<Grado> ListadeGrados { get; set; }

        public GradosBL()
        {
            _contexto = new Contexto();
            ListadeGrados = new List<Grado>();
        }

        public List<Grado> ObtenerGrados()
        {
            ListadeGrados = _contexto.Grados.ToList();
            return ListadeGrados;
        }

        public void GuardarGrado(Grado grado)
        {
            if (grado.Id == 0)
            {
                _contexto.Grados.Add(grado);
            }
            else
            {
                var gradoExistente = _contexto.Grados.Find(grado.Id);
                gradoExistente.Descripcion = grado.Descripcion;
            }

            _contexto.SaveChanges();
        }

        public Grado ObtenerGrado(int id)
        {
            var grado = _contexto.Grados.Find(id);

            return grado;
        }

        public GradoDetalle ObtenerGradoDetalle(int id)
        {
            var grado = _contexto.GradosDetalle
                .Include("Profesor")
                .Include("Grado")
                .Include("Seccion")
                .Include("Jornada")
                .FirstOrDefault(p => p.Id == id);

            return grado;
        }

        public List<GradoDetalle> ObtenerGradosPorProfesor(int profesorId)
        {
            var Anio = Convert.ToInt32((DateTime.Now).Year);
            var listadeGrados = _contexto.GradosDetalle
                .Include("Grado")
                .Include("seccion")
                .Include("Jornada")
                .Include("Profesor")
                .Where(n => n.ProfesorId == profesorId && n.Anio == Anio).ToList();
            return listadeGrados;
        }

        public void GuardarGradosDetalle(GradoDetalle gradoDetalle, bool editar = false)
        {

            //var profesor = _contexto.Notas.Find(gradoDetalle.AlumnoId);



            if (gradoDetalle.Id == 0)
            {
                _contexto.GradosDetalle.Add(gradoDetalle);

            }
            else
            {
                var gradoDetalleExistente = _contexto.GradosDetalle.Find(gradoDetalle.Id);

                gradoDetalleExistente.GradoId = gradoDetalle.GradoId;
                gradoDetalleExistente.SeccionId = gradoDetalle.SeccionId;
                gradoDetalleExistente.JornadaId = gradoDetalle.JornadaId;
                gradoDetalleExistente.ProfesorId = gradoDetalle.ProfesorId;
                gradoDetalleExistente.Anio = gradoDetalle.Anio;
                
            }

            _contexto.SaveChanges();
        }

        public void EliminarGrado(int id)
        {
            var grado = _contexto.Grados.Find(id);

            _contexto.Grados.Remove(grado);
            _contexto.SaveChanges();
        }

        public void EliminarGradoDetalle(int id)
        {
            var gradoDetalle = _contexto.GradosDetalle.Find(id);

            _contexto.GradosDetalle.Remove(gradoDetalle);
            _contexto.SaveChanges();
        }
    }
}
