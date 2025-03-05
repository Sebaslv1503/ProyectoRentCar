using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class SeguroCLS
    {
        public int id { get; set; }
        public int idReserva { get; set; }
        public string tipoSeguro { get; set; }
        public decimal precio { get; set; }
    }
}
