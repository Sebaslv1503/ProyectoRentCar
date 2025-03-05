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
    public class SeguroDAL:CadenaDAL
    {
        public bool RegistrarSeguro(SeguroCLS obj)
        {
            bool registrado = false;

            using (SqlConnection cn = new SqlConnection(this.cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_RegistrarSeguro", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ReservaId", obj.idReserva);
                        cmd.Parameters.AddWithValue("@TipoSeguro", obj.tipoSeguro);
                        cmd.Parameters.AddWithValue("@Costo", obj.precio);

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


    }
}
