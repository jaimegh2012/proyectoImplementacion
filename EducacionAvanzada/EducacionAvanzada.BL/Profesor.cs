using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducacionAvanzada.BL
{
    public class Profesor
    {
        public Profesor()
        {
            Activo = true;
            ListadeGradosPorProfesor = new List<GradoDetalle>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese un Usuario")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Ingrese la contraseña")]
        public string Contrasena { get; set; }

        public bool Activo { get; set; }

        public List<GradoDetalle> ListadeGradosPorProfesor { get; set; }

    }

    
}
