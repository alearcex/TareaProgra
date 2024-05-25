using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareaProgra.Aumentos
{
    public class Venta
    {
        public int NumeroFactura { get; set; }
        public int Cedula { get; set; }
        public string Nombre { get; set; }
        public int Localidad { get; set; }
        public int Cantidad { get; set; } 
        public int Cargos { get; set; }
        public int Subtotal { get; set; }
        public int Total { get; set; }

    }
}
