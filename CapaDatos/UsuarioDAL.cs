using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using CapaEntidad;

namespace CapaDatos
{
    public class UsuarioDAL:CadenaDAL
    {
        public bool RegistrarUsuario(UsuarioCLS obj)
        {
            bool registrado = false;
            

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                try
                {
                    cn.Open();

                    // Encriptar la contraseña antes de enviarla al SP
                    
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(obj.passwordHash);

                    using (SqlCommand cmd = new SqlCommand("usp_RegistrarUsuario", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        // Parámetros obligatorios
                        cmd.Parameters.AddWithValue("@nombreUsuario", obj.nombreUsuario ?? "");
                        cmd.Parameters.AddWithValue("@nombre", obj.nombre ?? "");
                        cmd.Parameters.AddWithValue("@apellido", obj.apellido ?? "");
                        cmd.Parameters.AddWithValue("@contrasenaHash", hashedPassword);
                        cmd.Parameters.AddWithValue("@rol", obj.rol ?? "");
                        

                        // Parámetros opcionales (depende del rol)

                        cmd.Parameters.AddWithValue("@cargo", obj.cargo);
                        cmd.Parameters.AddWithValue("@email", obj.correo);
                        cmd.Parameters.AddWithValue("@telefono", obj.telefono);


                        // Parámetro OUTPUT para recibir el ID del usuario creado
                        SqlParameter outputIdParam = new SqlParameter("@UsuarioId", SqlDbType.Int);
                        outputIdParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputIdParam);

                        // Ejecutar el procedimiento almacenado
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        // Obtener el ID del usuario insertado
                        int usuarioId = (outputIdParam.Value != DBNull.Value) ? Convert.ToInt32(outputIdParam.Value) : 0;

                        // Se considera éxito si se creó un usuario
                        registrado = (filasAfectadas > 0 && usuarioId > 0);
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


        public UsuarioCLS VerificarUsuario(UsuarioCLS obj)
        {
            UsuarioCLS usuario = null;

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("usp_VerificarUsuario", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombreUsuario", obj.nombreUsuario ?? "");

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                string hashedPassword = dr["ContrasenaHash"].ToString();

                                // Comparar la contraseña ingresada con la de la base de datos
                                if (BCrypt.Net.BCrypt.Verify(obj.passwordHash, hashedPassword))
                                {
                                    usuario = new UsuarioCLS
                                    {
                                        id = Convert.ToInt32(dr["Id"]),
                                        nombreUsuario = dr["NombreUsuario"].ToString(),
                                        rol = dr["Rol"].ToString(),
                                        nombre = dr["Nombre"].ToString(),
                                        apellido = dr["Apellido"].ToString()
                                    };
                                }
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

            return usuario;
        }
        public int ContarClientes()
        {
            int cantidad = 0;

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("usp_ContarClientes", cn))
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


        public List<UsuarioCLS> ListarEmpleados()
        {
            List<UsuarioCLS> lista = new List<UsuarioCLS>();

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_ListarEmpleados", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            UsuarioCLS empleado = new UsuarioCLS
                            {
                                id = Convert.ToInt32(dr["UsuarioId"]),
                                nombreUsuario = dr["NombreUsuario"].ToString(),
                                nombre = dr["Nombre"].ToString(),
                                apellido = dr["Apellido"].ToString(),
                                passwordHash = dr["ContrasenaHash"].ToString(),
                                cargo = dr["Cargo"].ToString()
                            };

                            lista.Add(empleado);
                        }
                    }
                }
            }

            return lista;
        }
        public List<UsuarioCLS> ListarClientes()
        {
            List<UsuarioCLS> lista = new List<UsuarioCLS>();

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_ListarClientes", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            UsuarioCLS cliente = new UsuarioCLS
                            {
                                id = Convert.ToInt32(dr["UsuarioId"]),
                                nombreUsuario = dr["NombreUsuario"].ToString(),
                                nombre = dr["Nombre"].ToString(),
                                apellido = dr["Apellido"].ToString(),
                                passwordHash = dr["ContrasenaHash"].ToString(),
                                telefono = dr["Telefono"].ToString(),
                                correo = dr["Email"].ToString()
                            };

                            lista.Add(cliente);
                        }
                    }
                }
            }

            return lista;
        }










    }
}
