using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXAMEN_PARCIAL
{
    internal class Citas
    {
        public string ID_doctor { get; set; } 
        public string DPI_paciente { get; set; }
        public DateTime Fecha_de_cita { get; set; } 
        public string Hora_de_cita { get; set; }
    }
}
