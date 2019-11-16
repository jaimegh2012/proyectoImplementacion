using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducacionAvanzada.BL
{
    public class Grado
    {

        public List<GradoDetalle> ListadeGradosPorProfesor { get; set; }
        

        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese el grado")]
        public string Descripcion { get; set; }

        public Grado()
        {
            ListadeGradosPorProfesor = new List<GradoDetalle>();
            
        }

    }

    public class GradoDetalle
    {
        public List<MateriaDetalle> ListadeMateriasPorGradoyProfesor { get; set; }

        public GradoDetalle()
        {
            ListadeMateriasPorGradoyProfesor = new List<MateriaDetalle>();
            Anio = Convert.ToInt32((DateTime.Now).Year);
        }

        public int Id { get; set; }

        public int ProfesorId { get; set; }
        public Profesor Profesor { get; set; }

        public int GradoId { get; set; }
        public Grado Grado { get; set; }

        public int SeccionId { get; set; }
        public seccion seccion { get; set; }

        public int JornadaId { get; set; }
        public Jornada Jornada { get; set; }

        [Required(ErrorMessage = "Ingrese el año")]
        [Display(Name = "Año")]
        public int Anio { get; set; }
    
    }
}
