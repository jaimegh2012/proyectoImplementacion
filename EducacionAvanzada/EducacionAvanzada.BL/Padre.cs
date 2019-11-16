using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducacionAvanzada.BL
{
    public class Padre
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese un Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese el Usuario")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Ingrese una Contraseña")]
        public string Contrasena { get; set; }
    }
}
