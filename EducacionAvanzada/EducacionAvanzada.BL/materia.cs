using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducacionAvanzada.BL
{
    public class Materia
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese una Descripcion de Materia")]
        public string Descripcion { get; set; }

        public List<MateriaDetalle> ListadeMateriasPorGradoyProfesor { get; set; }

        public Materia()
        {
            ListadeMateriasPorGradoyProfesor = new List<MateriaDetalle>();
        }
    }

    public class MateriaDetalle
    {
        public int Id { get; set; }

        public int MateriaId { get; set; }
        public Materia Materia { get; set; }

        public int GradoDetalleId { get; set; }
        public GradoDetalle GradoDetalle { get; set; }

    }
}
