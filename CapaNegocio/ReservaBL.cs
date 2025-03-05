using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class ReservaBL
    {
        public static bool RegistrarReserva(ReservaCLS obj)
        {
            ReservaDAL reservaDAL = new ReservaDAL();
            return reservaDAL.RegistrarReserva(obj);
        }
        public static List<ReservaCLS> ListarReservas(int? usuarioId = null)
        {
            ReservaDAL reservaDAL = new ReservaDAL();
            return reservaDAL.ListarReservas(usuarioId);
        }
        public static ReservaCLS ObtenerReservaCompleta(int reservaId)
        {
            ReservaDAL reservaDAL = new ReservaDAL();
            return reservaDAL.ObtenerReservaCompleta(reservaId);
        }
        public static bool ConfirmarReserva(ReservaCLS reserva)
        {
            ReservaDAL reservaDAL = new ReservaDAL();
            return reservaDAL.ConfirmarReserva(reserva);
        }
        public static bool EliminarReserva(int reservaId)
        {
            ReservaDAL reservaDAL = new ReservaDAL();
            return reservaDAL.EliminarReserva(reservaId);
        }



    }
}
