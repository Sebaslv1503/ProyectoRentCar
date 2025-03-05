using System.Text.Json;
using CapaDatos;
using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace Taller1.Controllers
{
    public class Usuario : Controller
    {
        
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Clientes()
        {
            return View();
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Login", "Usuario"); 
        }





        public JsonResult registrarUsuario(UsuarioCLS obj) 
        {
            
            bool registrado = UsuarioBL.registrarUsuario(obj);
            return Json(new { success = registrado });
        }

        public JsonResult VerificarUsuario(UsuarioCLS obj)
        {
            UsuarioCLS usuario = UsuarioBL.verificarUsuario(obj);

            if (usuario != null)
            {
                HttpContext.Session.SetString("NombreUsuario", usuario.nombreUsuario);
                HttpContext.Session.SetString("Rol", usuario.rol);
                HttpContext.Session.SetString("Nombre", usuario.nombre);
                HttpContext.Session.SetString("Apellido", usuario.apellido);
                HttpContext.Session.SetInt32("Id", usuario.id);

                return Json(new { success = true, rol = usuario.rol });
            }

            return Json(new { success = false });
        }


        public List<UsuarioCLS> listarEmpleados()
        {
            return UsuarioBL.ListarEmpleados();
        }
        public List<UsuarioCLS> listarClientes()
        {
            return UsuarioBL.ListarClientes();
        }











        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Usuario");
        }

        public JsonResult ObtenerCantidadVehiculos()
        {
            int cantidadVehiculos = VehiculoBL.ObtenerCantidadVehiculos();
            return Json(new { cantidad = cantidadVehiculos }, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        public JsonResult ObtenerCantidadClientes()
        {
            int cantidadClientes = UsuarioBL.ObtenerCantidadClientes();
            return Json(new { cantidad = cantidadClientes }, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }


    }
}
