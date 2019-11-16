using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducacionAvanzada.BL
{
    public class ProfesorBL
    {
        Contexto _contexto;
        public List<Profesor> ListadeProfesores { get; set; }

        public ProfesorBL()
        {
            _contexto = new Contexto();
            ListadeProfesores = new List<Profesor>();
        }
        public List<Profesor> ObtenerProfesores()
        {
            ListadeProfesores = _contexto.Profesores.ToList();
            return ListadeProfesores;
        }

        public void GuardarProfesor(Profesor profesor)
        {
            if (profesor.Id == 0)
            {
                _contexto.Profesores.Add(profesor);
            }
            else
            {
                var profesorExistente = _contexto.Profesores.Find(profesor.Id);
                profesorExistente.Nombre = profesor.Nombre;
                profesorExistente.Usuario = profesor.Usuario;
                profesorExistente.Contrasena = Encriptar.CodificarContrasena(profesor.Contrasena);
                profesorExistente.Activo = profesor.Activo;
            }

            _contexto.SaveChanges();
        }

        public Profesor ObtenerProfesor(int id)
        {
            var profesor = _contexto.Profesores.Find(id);

            return profesor;
        }

        public Profesor ObtenerProfesor(string usuario)
        {
            var profesor = _contexto.Profesores
                .FirstOrDefault(p => p.Usuario == usuario.Trim());
            return profesor;
        }

        public List<Profesor> ObtenerProfesorComoLista(int id)
        {
            ListadeProfesores = _contexto.Profesores
                .Where(a => a.Activo == true && a.Id == id)
                .ToList();

            return ListadeProfesores;
        }

        public void EliminarProfesor(int id)
        {
            var profesor = _contexto.Profesores.Find(id);

            _contexto.Profesores.Remove(profesor);
            _contexto.SaveChanges();
        }
    }
}
