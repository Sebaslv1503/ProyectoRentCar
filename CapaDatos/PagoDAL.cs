using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class PagoDAL:CadenaDAL
    {
        public bool RegistrarPago(PagoCLS obj)
        {
            bool registrado = false;

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                try
                {
                    cn.Open();

                    // Verificar si la reserva existe antes de registrar el pago
                    int reservaExiste = 0;
                    using (SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM Reservas WHERE Id = @ReservaId", cn))
                    {
                        cmdCheck.Parameters.AddWithValue("@ReservaId", obj.idReserva);
                        reservaExiste = Convert.ToInt32(cmdCheck.ExecuteScalar());
                    }

                    if (reservaExiste == 0)
                    {
                        throw new Exception("La reserva especificada no existe.");
                    }

                    using (SqlCommand cmd = new SqlCommand("usp_RegistrarPago", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ReservaId", obj.idReserva);
                        cmd.Parameters.AddWithValue("@Monto", obj.monto);
                        cmd.Parameters.AddWithValue("@MetodoPago", obj.metodoPago);
                        cmd.Parameters.AddWithValue("@FechaPago", obj.fechaPago);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        registrado = filasAfectadas > 0;
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    throw;
                }
            }
            return registrado;
        }
        public List<ReservaCLS> ListarPagos(int? usuarioId = null)
        {
            List<ReservaCLS> lista = new List<ReservaCLS>();

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_ListarPagosPorUsuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId.HasValue ? (object)usuarioId.Value : DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ReservaCLS pago = new ReservaCLS
                            {
                                idPago = Convert.ToInt32(dr["PagoId"]),
                                id = Convert.ToInt32(dr["ReservaId"]),
                                montoPago = Convert.ToDecimal(dr["Monto"]),
                                metodoPago = dr["MetodoPago"].ToString(),
                                fechaPago = Convert.ToDateTime(dr["FechaPago"]),
                                fechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                                fechaFin = Convert.ToDateTime(dr["FechaFin"]),
                                estadoReserva = dr["EstadoReserva"].ToString(),
                                marcaVehiculo = dr["Marca"].ToString(),
                                modeloVehiculo = dr["Modelo"].ToString(),
                                anioVehiculo = Convert.ToInt32(dr["Anio"]),
                                precioVehiculo = Convert.ToDecimal(dr["PrecioVehiculo"]),
                                categoriaVehiculo = dr["Categoria"].ToString(),
                            };

                            // Convertir imagen a Base64 si no es NULL
                            if (dr["Imagen"] != DBNull.Value)
                            {
                                byte[] imagenBytes = (byte[])dr["Imagen"];
                                string imagenBase64 = Convert.ToBase64String(imagenBytes);
                                pago.imagenVehiculo = "data:image/png;base64," + imagenBase64;
                            }
                            else
                            {
                                pago.imagenVehiculo = ""; // Si no tiene imagen
                            }

                            lista.Add(pago);
                        }
                    }
                }
            }

            return lista;
        }

    }
}
