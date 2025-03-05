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
    public class ReservaDAL:CadenaDAL
    {
        public bool RegistrarReserva(ReservaCLS obj)
        {
            bool registrado = false;

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_RegistrarReserva", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ClienteId", obj.idCliente);
                        cmd.Parameters.AddWithValue("@VehiculoId", obj.idVehiculo);
                        cmd.Parameters.AddWithValue("@FechaInicio", obj.fechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", obj.fechaFin);
                        cmd.Parameters.AddWithValue("@Estado", obj.estadoReserva ?? "Pendiente");

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
        public List<ReservaCLS> ListarReservas(int? usuarioId = null)
        {
            List<ReservaCLS> lista = new List<ReservaCLS>();

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_ListarReservasPorUsuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Si el usuarioId es NULL, enviamos DBNull.Value para que el SP devuelva todas las reservas
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId.HasValue ? (object)usuarioId.Value : DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ReservaCLS reserva = new ReservaCLS
                            {
                                id = Convert.ToInt32(dr["ReservaId"]),
                                idCliente = Convert.ToInt32(dr["ClienteId"]),
                                idVehiculo = Convert.ToInt32(dr["VehiculoId"]),
                                marcaVehiculo = dr["Marca"].ToString(),
                                modeloVehiculo = dr["Modelo"].ToString(),
                                anioVehiculo = Convert.ToInt32(dr["Anio"]),
                                precioVehiculo = Convert.ToDecimal(dr["Precio"]),
                                fechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                                fechaFin = Convert.ToDateTime(dr["FechaFin"]),
                                estadoReserva = dr["Estado"].ToString()
                            };

                            lista.Add(reserva);
                        }
                    }
                }
            }

            return lista;
        }

        public ReservaCLS ObtenerReservaCompleta(int reservaId)
        {
            ReservaCLS reserva = null;

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("usp_ObtenerReservaCompleta", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ReservaId", reservaId);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                reserva = new ReservaCLS
                                {
                                    // Datos del Cliente
                                    idCliente = Convert.ToInt32(dr["ClienteId"]),
                                    idUsuario = Convert.ToInt32(dr["UsuarioId"]),
                                    nombreUsuario = dr["NombreUsuario"].ToString(),
                                    nombreCliente = dr["Nombre"].ToString(),
                                    apellidoCliente = dr["Apellido"].ToString(),
                                    emailCliente = dr["Email"].ToString(),
                                    telefonoCliente = dr["Telefono"].ToString(),

                                    // Datos del Vehículo
                                    idVehiculo = Convert.ToInt32(dr["VehiculoId"]),
                                    marcaVehiculo = dr["Marca"].ToString(),
                                    modeloVehiculo = dr["Modelo"].ToString(),
                                    anioVehiculo = Convert.ToInt32(dr["Anio"]),
                                    precioVehiculo = Convert.ToDecimal(dr["Precio"]),
                                    estadoVehiculo = dr["Estado"].ToString(),
                                    categoriaVehiculo = dr["Categoria"].ToString(),

                                    // Imagen del Vehículo
                                    imagenVehiculo = dr["Imagen"] != DBNull.Value
                                        ? "data:image/png;base64," + Convert.ToBase64String((byte[])dr["Imagen"])
                                        : "",

                                    // Datos de la Reserva
                                    id = Convert.ToInt32(dr["ReservaId"]),
                                    fechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                                    fechaFin = Convert.ToDateTime(dr["FechaFin"]),
                                    estadoReserva = dr["EstadoReserva"].ToString(),

                                    // Datos del Pago (puede ser nulo)
                                    montoPago = dr["Monto"] != DBNull.Value ? Convert.ToDecimal(dr["Monto"]) : 0,
                                    metodoPago = dr["MetodoPago"] != DBNull.Value ? dr["MetodoPago"].ToString() : "No pagado",
                                    fechaPago = dr["FechaPago"] != DBNull.Value ? Convert.ToDateTime(dr["FechaPago"]) : (DateTime?)null,
                                    // Datos del Seguro (puede ser nulo)
                                    tipoSeguro = dr["TipoSeguro"] != DBNull.Value ? dr["TipoSeguro"].ToString() : "Sin seguro",
                                    costoSeguro = dr["Costo"] != DBNull.Value ? Convert.ToDecimal(dr["Costo"]) : 0
                                };
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    throw;
                }
            }

            return reserva;
        }
        public bool ConfirmarReserva(ReservaCLS reservaCompleta)
        {
            bool confirmada = false;

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("usp_ConfirmarReserva", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros para actualizar la reserva
                        cmd.Parameters.AddWithValue("@ReservaId", reservaCompleta.id);
                        cmd.Parameters.AddWithValue("@FechaInicio", reservaCompleta.fechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", reservaCompleta.fechaFin);

                        // Parámetros del seguro (puede ser opcional)
                        cmd.Parameters.AddWithValue("@TipoSeguro", string.IsNullOrEmpty(reservaCompleta.tipoSeguro) ? DBNull.Value : (object)reservaCompleta.tipoSeguro);
                        cmd.Parameters.AddWithValue("@CostoSeguro", reservaCompleta.costoSeguro > 0 ? (object)reservaCompleta.costoSeguro : DBNull.Value);

                        // Parámetros del pago
                        cmd.Parameters.AddWithValue("@MetodoPago", reservaCompleta.metodoPago);
                        cmd.Parameters.AddWithValue("@MontoPago", reservaCompleta.montoPago);
                        cmd.Parameters.AddWithValue("@FechaPago", reservaCompleta.fechaPago);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        confirmada = filasAfectadas > 0;
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    throw;
                }
            }

            return confirmada;
        }
        public bool EliminarReserva(int reservaId)
        {
            bool eliminado = false;

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("usp_EliminarReserva", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ReservaId", reservaId);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        eliminado = filasAfectadas > 0;
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    throw;
                }
            }

            return eliminado;
        }






    }
}
