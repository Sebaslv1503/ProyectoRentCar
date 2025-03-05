using System.Data.SqlClient;
using CapaDatos;
using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace Taller1.Controllers
{
    public class Reserva : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GestionReserva()
        {
            return View();
        }
        
        public IActionResult ReservasAdmin()
        {
            return View();
        }
        public JsonResult GuardarReserva(ReservaCLS reserva)
        {
            bool registrada = ReservaBL.RegistrarReserva(reserva);
            return Json(new { success = registrada }); 
        }

        public IActionResult Reservacion(int idVehiculo)
        {
            int idUsuario = HttpContext.Session.GetInt32("Id") ?? 0;
            if (idUsuario == 0) return RedirectToAction("Login", "Usuario");

            using (SqlConnection cn = new SqlConnection(new CadenaDAL().cadena))
            {
                cn.Open();

                // Obtener ClienteId
                string queryCliente = "SELECT ClienteId FROM Clientes WHERE UsuarioId = @idUsuario";
                using (SqlCommand cmd = new SqlCommand(queryCliente, cn))
                {
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    object result = cmd.ExecuteScalar();
                    ViewBag.IdCliente = result != null ? Convert.ToInt32(result) : (int?)null;
                }

                // Obtener Información del Vehículo
                string queryVehiculo = "SELECT Marca, Modelo, Anio, Precio, Categoria, Estado, Imagen FROM Vehiculos WHERE Id = @idVehiculo";
                using (SqlCommand cmd = new SqlCommand(queryVehiculo, cn))
                {
                    cmd.Parameters.AddWithValue("@idVehiculo", idVehiculo);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ViewBag.Marca = dr["Marca"].ToString();
                            ViewBag.Modelo = dr["Modelo"].ToString();
                            ViewBag.Anio = Convert.ToInt32(dr["Anio"]);
                            ViewBag.Precio = Convert.ToDecimal(dr["Precio"]);
                            ViewBag.Categoria = dr["Categoria"].ToString();
                            ViewBag.Estado = dr["Estado"].ToString();

                            if (dr["Imagen"] != DBNull.Value)
                            {
                                byte[] imagenBytes = (byte[])dr["Imagen"];
                                string imagenBase64 = Convert.ToBase64String(imagenBytes);
                                ViewBag.Imagen = "data:image/png;base64," + imagenBase64;
                            }
                            else
                            {
                                ViewBag.Imagen = "/images/no-image.png"; // Imagen por defecto si no hay imagen
                            }
                        }
                    }
                }
            }

            ViewBag.IdVehiculo = idVehiculo;
            return View();
        }

        
        public List<ReservaCLS> ListarReservas()
        {
            int usuarioId = HttpContext.Session.GetInt32("Id") ?? 0;
            return ReservaBL.ListarReservas(usuarioId);
        }
        public List<ReservaCLS> ListarTodasReservas()
        {
            
            return ReservaBL.ListarReservas();
        }
        public JsonResult RegistrarSeguro(SeguroCLS obj)
        {
            bool registrado = SeguroBL.RegistrarSeguro(obj);
            return Json(new { success = registrado });
        }
        public JsonResult ObtenerReservaCompleta(int id)
        {
            ReservaCLS reserva = ReservaBL.ObtenerReservaCompleta(id);

            if (reserva != null)
            {
                return Json(new { success = true, data = reserva });
            }

            return Json(new { success = false, message = "No se encontró la reserva." });
        }

        [HttpPost]
        public JsonResult ConfirmarReserva(ReservaCLS reserva)
        {
            bool exito = ReservaBL.ConfirmarReserva(reserva);
            return Json(new { success = exito });
        }
        [HttpPost]
        public JsonResult EliminarReserva(int idReserva)
        {
            bool eliminado = ReservaBL.EliminarReserva(idReserva);

            if (eliminado)
            {
                return Json(new { success = true, message = "Reserva eliminada correctamente." });
            }
            else
            {
                return Json(new { success = false, message = "Error al eliminar la reserva." });
            }
        }





    }
}
