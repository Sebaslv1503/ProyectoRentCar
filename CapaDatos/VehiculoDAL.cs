using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using Microsoft.AspNetCore.Http;

namespace CapaDatos
{
    public class VehiculoDAL:CadenaDAL
    {
        public bool RegistrarVehiculo(VehiculoCLS obj, IFormFile imagenFile)
        {
            bool registrado = false;

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("usp_RegistrarVehiculo", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros obligatorios
                        cmd.Parameters.AddWithValue("@marca", obj.marca ?? "");
                        cmd.Parameters.AddWithValue("@modelo", obj.modelo ?? "");
                        cmd.Parameters.AddWithValue("@anio", obj.anio);
                        cmd.Parameters.AddWithValue("@precio", obj.precio);
                        cmd.Parameters.AddWithValue("@estado", obj.estado ?? "");
                        cmd.Parameters.AddWithValue("@categoria", obj.categoria ?? "");

                        // Convertir la imagen a byte[] si hay archivo
                        byte[] imageData = null;
                        if (imagenFile != null)
                        {
                            using (var ms = new MemoryStream())
                            {
                                imagenFile.CopyTo(ms);
                                imageData = ms.ToArray();
                            }
                        }
                        cmd.Parameters.AddWithValue("@imagen", (object)imageData ?? DBNull.Value);

                        // Ejecutar el procedimiento almacenado y obtener el ID del vehículo insertado
                        int idVehiculo = Convert.ToInt32(cmd.ExecuteScalar());
                        registrado = idVehiculo > 0;
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
        public byte[] ObtenerImagenVehiculo(int idVehiculo)
        {
            byte[] imagenBytes = null;

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT Imagen FROM Vehiculos WHERE id = @id", cn))
                    {
                        cmd.Parameters.AddWithValue("@id", idVehiculo);
                        object resultado = cmd.ExecuteScalar();

                        if (resultado != DBNull.Value && resultado != null)
                        {
                            imagenBytes = (byte[])resultado;
                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    throw;
                }
            }

            return imagenBytes;
        }
        public List<VehiculoCLS> ListarVehiculos()
        {
            List<VehiculoCLS> lista = new List<VehiculoCLS>();

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_ListarVehiculos", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            VehiculoCLS vehiculo = new VehiculoCLS
                            {
                                id = Convert.ToInt32(dr["VehiculoId"]),
                                marca = dr["Marca"].ToString(),
                                modelo = dr["Modelo"].ToString(),
                                anio = Convert.ToInt32(dr["Anio"]),
                                precio = Convert.ToDecimal(dr["Precio"]),
                                estado = dr["Estado"].ToString(),
                                categoria = dr["Categoria"].ToString(),

                            };

                            // Convertir la imagen a Base64 si no es NULL
                            if (dr["Imagen"] != DBNull.Value)
                            {
                                byte[] imagenBytes = (byte[])dr["Imagen"];
                                string imagenBase64 = Convert.ToBase64String(imagenBytes);
                                vehiculo.imagenString = "data:image/png;base64," + imagenBase64; 
                            }
                            else
                            {
                                vehiculo.imagenString = ""; // Si no tiene imagen
                            }

                            lista.Add(vehiculo);
                        }
                    }
                }
            }

            return lista;
        }

        public bool EliminarVehiculo(int id)
        {
            bool eliminado = false;

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("usp_EliminarVehiculo", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        eliminado = filasAfectadas > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al eliminar vehículo: " + ex.Message);
                }
            }
            return eliminado;
        }

        public int ContarVehiculos()
        {
            int cantidad = 0;

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("usp_ContarVehiculos", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cantidad = Convert.ToInt32(cmd.ExecuteScalar()); 
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    throw;
                }
            }

            return cantidad;
        }

        public bool ModificarVehiculo(VehiculoCLS obj, IFormFile imagenFile)
        {
            bool modificado = false;

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("usp_ModificarVehiculo", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id", obj.id);
                        cmd.Parameters.AddWithValue("@marca", obj.marca ?? "");
                        cmd.Parameters.AddWithValue("@modelo", obj.modelo ?? "");
                        cmd.Parameters.AddWithValue("@anio", obj.anio);
                        cmd.Parameters.AddWithValue("@precio", obj.precio);
                        cmd.Parameters.AddWithValue("@estado", obj.estado ?? "");
                        cmd.Parameters.AddWithValue("@categoria", obj.categoria ?? "");

                        // Convertir la imagen a byte[] si se proporciona
                        byte[] imageData = null;
                        if (imagenFile != null)
                        {
                            using (var ms = new MemoryStream())
                            {
                                imagenFile.CopyTo(ms);
                                imageData = ms.ToArray();
                            }
                        }
                        else
                        {
                            imageData = null;
                        }

                            cmd.Parameters.AddWithValue("@imagen", (object)imageData );

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        modificado = filasAfectadas > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al modificar vehículo: " + ex.Message);
                }
            }
            return modificado;
        }
        public List<VehiculoCLS> FiltrarVehiculos(VehiculoCLS filtro)
        {
            List<VehiculoCLS> lista = new List<VehiculoCLS>();

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("usp_FiltrarVehiculos", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@categoria", string.IsNullOrEmpty(filtro.categoria) ? (object)DBNull.Value : filtro.categoria);
                        cmd.Parameters.AddWithValue("@marca", string.IsNullOrEmpty(filtro.marca) ? (object)DBNull.Value : filtro.marca);
                        cmd.Parameters.AddWithValue("@anio", filtro.anio > 0 ? (object)filtro.anio : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@valorMax", filtro.precio > 0 ? (object)filtro.precio : (object)DBNull.Value);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                VehiculoCLS vehiculo = new VehiculoCLS
                                {
                                    id = Convert.ToInt32(dr["Id"]),
                                    marca = dr["Marca"].ToString(),
                                    modelo = dr["Modelo"].ToString(),
                                    anio = Convert.ToInt32(dr["Anio"]),
                                    precio = Convert.ToDecimal(dr["Precio"]),
                                    estado = dr["Estado"].ToString(),
                                    categoria = dr["Categoria"].ToString()
                                };

                                // Convertir imagen a Base64 si no es NULL
                                if (dr["Imagen"] != DBNull.Value)
                                {
                                    byte[] imagenBytes = (byte[])dr["Imagen"];
                                    vehiculo.imagenString = "data:image/png;base64," + Convert.ToBase64String(imagenBytes);
                                }
                                else
                                {
                                    vehiculo.imagenString = ""; // Si no tiene imagen
                                }

                                lista.Add(vehiculo);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    cn.Close();
                    throw new Exception("Error al filtrar vehículos: " + ex.Message);
                }
            }

            return lista;
        }
        public VehiculoCLS ObtenerVehiculoPorId(int id)
        {
            VehiculoCLS vehiculo = null;

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_ObtenerVehiculoPorId", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VehiculoId", id);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            vehiculo = new VehiculoCLS
                            {
                                id = Convert.ToInt32(dr["VehiculoId"]),
                                marca = dr["Marca"].ToString(),
                                modelo = dr["Modelo"].ToString(),
                                anio = Convert.ToInt32(dr["Anio"]),
                                precio = Convert.ToDecimal(dr["Precio"]),
                                estado = dr["Estado"].ToString(),
                                categoria = dr["Categoria"].ToString()
                            };

                            // Convertir la imagen a Base64 si no es NULL
                            if (dr["Imagen"] != DBNull.Value)
                            {
                                byte[] imagenBytes = (byte[])dr["Imagen"];
                                string imagenBase64 = Convert.ToBase64String(imagenBytes);
                                vehiculo.imagenString = "data:image/png;base64," + imagenBase64;
                            }
                            else
                            {
                                vehiculo.imagenString = "";
                            }
                        }
                    }
                }
            }

            return vehiculo;
        }


        public bool Devolver(int idVehiculo)
        {
            bool actualizado = false;

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlTransaction transaccion = cn.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand cmdVehiculo = new SqlCommand("UPDATE Vehiculos SET Estado = 'Disponible' WHERE Id = @IdVehiculo", cn, transaccion))
                            {
                                cmdVehiculo.Parameters.AddWithValue("@IdVehiculo", idVehiculo);
                                cmdVehiculo.ExecuteNonQuery();
                            }

                            using (SqlCommand cmdReserva = new SqlCommand("UPDATE Reservas SET Estado = 'Cancelada' WHERE VehiculoId = @IdVehiculo AND Estado = 'Confirmada'", cn, transaccion))
                            {
                                cmdReserva.Parameters.AddWithValue("@IdVehiculo", idVehiculo);
                                cmdReserva.ExecuteNonQuery();
                            }

                            transaccion.Commit();
                            actualizado = true;
                        }
                        catch (Exception)
                        {
                            transaccion.Rollback();
                            throw;
                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    throw;
                }
            }
            return actualizado;
        }








    }
}
