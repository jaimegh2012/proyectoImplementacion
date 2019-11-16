using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducacionAvanzada.BL
{
    public class NotasBL
    {
        Contexto _contexto;

        public List<Notas> ListadeNotas { get; set; }

        public NotasBL()
        {
            _contexto = new Contexto();
            ListadeNotas = new List<Notas>();
         


        }
        public List<Notas> ObtenerNotas()
        {
            ListadeNotas = _contexto.Notas
                .Include("Grado")
                .Include("Jornada")
                .Include("Seccion")
                .ToList();

            return ListadeNotas;
        }

        public List<NotasDetalle> ObtenerNotasDetalle(int notaId)
        {

            var listadeNotasDetalle = _contexto.NotasDetalle
                .Include("Alumno")
                .Include("Materia")
                .Where(n => n.NotaId == notaId).ToList();
            return listadeNotasDetalle;
        }

        public List<NotasDetalle> ObtenerNotasporAlumno(int alumnoId)
        {
            var listadeNotasporAlumno = _contexto.NotasDetalle
                .Include("Alumno")
                .Include("Materia")
                .Where(n => n.AlumnoId == alumnoId).ToList();
            return listadeNotasporAlumno;
        }

        //funcion ObtenerNotasporAlumno sobrecargada para obtener notas de acuerdo al curso y seccion
        public List<NotasDetalle> ObtenerNotasporAlumno(int alumnoId, int notaId)
        {
            var listadeNotasporAlumno = _contexto.NotasDetalle
                .Include("Alumno")
                .Include("Materia")
                .Where(n => n.AlumnoId == alumnoId && n.NotaId == notaId).ToList();
            return listadeNotasporAlumno;
        }
        //---------------------------------
        /*public List<Notas> ObtenerNotasParaProfesor(int profesorId)
        {
            var GradoImpartidos = _gradosBL.ObtenerGradosPorProfesor(profesorId);
            List<Notas> notasParaProfesor = new List<Notas>();
            foreach (var item in GradoImpartidos)
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
        }*/


        //--------------------------------------
        /*public List<NotasDetalle> ObtenerNotasporAlumno(Alumno alumno)
        {
            var AlumnoExistente = _contexto.Alumnos
                .FirstOrDefault(r => r.Id == alumno.Id);
            AlumnoExistente.Id = alumno.Id;

            if (AlumnoExistente != null)
            {
                return _contexto.NotasDetalle.Include("Alumno").Include("materia")
                .Where(r => r.AlumnoId == AlumnoExistente.Id).ToList();
            }

            return new List<NotasDetalle>();
        }*/

        //
        public NotasDetalle ObtenerNotasDetallePorAlumno(int notaId, int alumnoId, int materiaId)
        {
            var notadetalle = _contexto.NotasDetalle
                 .Include("Materia")
                 .Include("Alumno").FirstOrDefault(p => p.NotaId == notaId && p.AlumnoId == alumnoId && p.MateriaId == materiaId);

            return notadetalle;
        }
        //

        public NotasDetalle ObtenerNotasDetalleporId(int id)
        {
            var notadetalle = _contexto.NotasDetalle
                 .Include("Materia")
                 .Include("Alumno").FirstOrDefault(p => p.Id == id);

            return notadetalle;
        }


        public void GuardarNota(Notas nota)
        {
            if (nota.Id == 0)
            {
                _contexto.Notas.Add(nota);
            }
            else
            {
                var notaExistente = _contexto.Notas.Find(nota.Id);
                notaExistente.GradoId = nota.GradoId;
                notaExistente.JornadaId = nota.JornadaId;
                notaExistente.SeccionId = nota.SeccionId;
                notaExistente.Anio = nota.Anio;
            }

            _contexto.SaveChanges();
        }

        public Notas ObtenerNota(int id)
        {
            var nota = _contexto.Notas
                .Include("Grado")
                .Include("Jornada")
                .Include("Seccion")
                .FirstOrDefault(p => p.Id == id);

            return nota;
        }
        //REVISAR DESPUES SI SE USA
        public Notas ObtenerNotaPorProfesor(int gradoId, int seccionId, int jornadaId, int anio)
        {
            /*var listadeNotasparaProfesor = _contexto.Notas
                .Include("Grado")
                .Include("Jornada")
                .Include("Seccion")
                .Where(p => p.GradoId == gradoId && p.SeccionId == seccionId && p.JornadaId == jornadaId && p.Anio == anio).ToList();
           
                    */

            var nota = _contexto.Notas
                .Include("Grado")
                .Include("Jornada")
                .Include("Seccion")
                .FirstOrDefault(p => p.GradoId == gradoId);

            return nota;
        }

        public void GuardarNotasDetalle(NotasDetalle notasDetalle, bool editar = false)
        {
            
            var alumno = _contexto.Notas.Find(notasDetalle.AlumnoId);
     
            notasDetalle.NotaFinal = ((notasDetalle.PrimerParcial + notasDetalle.SegundoParcial + notasDetalle.TercerParcial + notasDetalle.CuartoParcial) / 4);


            if (notasDetalle.Id == 0)
            {
                _contexto.NotasDetalle.Add(notasDetalle);

            }
            else
            {
                var notaDetalleExistente = _contexto.NotasDetalle.Find(notasDetalle.Id);

                notaDetalleExistente.AlumnoId = notasDetalle.AlumnoId;
                notaDetalleExistente.MateriaId = notasDetalle.MateriaId;
                notaDetalleExistente.PrimerParcial = notasDetalle.PrimerParcial;
                notaDetalleExistente.SegundoParcial = notasDetalle.SegundoParcial;
                notaDetalleExistente.TercerParcial = notasDetalle.TercerParcial;
                notaDetalleExistente.CuartoParcial = notasDetalle.CuartoParcial;
                notaDetalleExistente.NotaFinal = notasDetalle.NotaFinal;
            }

            var nota = _contexto.Notas.Find(notasDetalle.NotaId);
            nota.NotaFinal = nota.NotaFinal + notasDetalle.NotaFinal;
            _contexto.SaveChanges();
        }

        public void EliminarNotaDetalle(int id)
        {
            var notaDetalle = _contexto.NotasDetalle.Find(id);
            _contexto.NotasDetalle.Remove(notaDetalle);

            var nota = _contexto.Notas.Find(notaDetalle.NotaId);
            nota.NotaFinal = nota.NotaFinal - notaDetalle.NotaFinal;

            _contexto.SaveChanges();
        }


    }
}
