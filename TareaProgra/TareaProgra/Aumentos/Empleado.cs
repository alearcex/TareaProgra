using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareaProgra.Aumentos
{
    public class Empleado
    {
        public string Nombre { get; set; }
        public int Cedula { get; set; }
        public int TipoEmpleado { get; set; }
        public int HorasLaboradas { get; set; }
        public decimal PrecioHora { get; set; }
        public decimal SalarioOrdinario { get; set; }
        public decimal Aumento { get; set; }
        public decimal SalarioBruto { get; set; }
        public decimal SalarioNeto {  get; set; }
        public decimal CCSS { get; set; }  //Deducciones

    }
}
