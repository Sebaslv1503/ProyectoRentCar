using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class PagoBL
    {
        public static bool RegistrarPago(PagoCLS obj)
        {
            PagoDAL pagoDAL = new PagoDAL();
            return pagoDAL.RegistrarPago(obj);
        }
        public static List<ReservaCLS> ListarPagos(int? idUsuario = null)
        {
            PagoDAL reservaDAL = new PagoDAL();
            return reservaDAL.ListarPagos(idUsuario);
        }

    }
}
