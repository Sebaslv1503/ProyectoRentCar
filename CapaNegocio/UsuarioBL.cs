using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class UsuarioBL
    {
        public static bool registrarUsuario(UsuarioCLS obj)
        {
            UsuarioDAL usuario = new UsuarioDAL();
            
            return usuario.RegistrarUsuario(obj);
        }
        public static UsuarioCLS verificarUsuario(UsuarioCLS obj)
        {
            UsuarioDAL usuarioDAL = new UsuarioDAL();
            return usuarioDAL.VerificarUsuario(obj);
        }
        public static List<UsuarioCLS> ListarEmpleados()
        {
            UsuarioDAL usuarioDAL = new UsuarioDAL();
            return usuarioDAL.ListarEmpleados();
        }
        public static List<UsuarioCLS> ListarClientes()
        {
            UsuarioDAL usuarioDAL = new UsuarioDAL();
            return usuarioDAL.ListarClientes();
        }

        public static int ObtenerCantidadClientes()
        {
            UsuarioDAL clienteDAL = new UsuarioDAL();
            return clienteDAL.ContarClientes();
        }


    }
}
