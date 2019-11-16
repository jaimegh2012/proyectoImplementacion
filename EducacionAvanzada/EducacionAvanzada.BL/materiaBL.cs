using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducacionAvanzada.BL
{
    public class materiaBL
    {
        Contexto _contexto;
        public List<Materia> Listadematerias { get; set; }
        public List<MateriaDetalle> ListademateriasDetalle { get; set; }

        public materiaBL()
        {
            _contexto = new Contexto();
            Listadematerias = new List<Materia>();
            ListademateriasDetalle = new List<MateriaDetalle>();
        }

        public List<Materia> Obtenermaterias()
        {
            Listadematerias = _contexto.Materia.ToList();

            return Listadematerias;

        }

        //OBTENER UN SOLA MATERIA COMO LISTA
        public List<Materia> Obtenermaterias(int id)
        {
            Listadematerias = _contexto.Materia
                .Where(a => a.Id == id)
                .ToList();

            return Listadematerias;

        }

        public List<MateriaDetalle> ObtenerMateriasPorGradoyProfesor(int id)
        {
            ListademateriasDetalle = _contexto.MateriasDetalle
                .Include("Materia")
                .Where(a => a.GradoDetalleId == id)
                .ToList();

            return ListademateriasDetalle;
        }

        public Materia ObtenerMaterias(int id)
        {
            var materia = _contexto.Materia.Find(id);

            return materia;
        }

        public MateriaDetalle ObtenerMateriaDetalle(int id)
        {
            var materiaDetalle = _contexto.MateriasDetalle
                .Include("Materia")
                .FirstOrDefault(p => p.Id == id);

            return materiaDetalle;
        }

        public void GuardarMateria(Materia materia)
        {
            if (materia.Id == 0)
            {
                _contexto.Materia.Add(materia);
            }
            else
            {
                var MateriaExistente = _contexto.Materia.Find(materia.Id);
                MateriaExistente.Descripcion = materia.Descripcion;
            }

            _contexto.SaveChanges();
        }

        public void GuardarMateriaDetalle(MateriaDetalle materiaDetalle, bool editar = false)
        {

            if (materiaDetalle.Id == 0)
            {
                _contexto.MateriasDetalle.Add(materiaDetalle);
            }
            else
            {
                var materiaDetalleExistente = _contexto.MateriasDetalle.Find(materiaDetalle.Id);

                materiaDetalleExistente.GradoDetalleId = materiaDetalle.GradoDetalleId;
                materiaDetalleExistente.MateriaId = materiaDetalle.MateriaId;

            }
            _contexto.SaveChanges();
        }

        public Materia ObtenerMateria(int id)
        {
            var materia = _contexto.Materia.Find(id);

            return materia;
        }

        

        public void EliminarMateria(int id)
        {
            var materia = _contexto.Materia.Find(id);

            _contexto.Materia.Remove(materia);
            _contexto.SaveChanges();
        }

        public void EliminarMateriaDetalle(int id)
        {
            var materiaDetalle = _contexto.MateriasDetalle.Find(id);

            _contexto.MateriasDetalle.Remove(materiaDetalle);
            _contexto.SaveChanges();
        }

        
        
    }
}



    

