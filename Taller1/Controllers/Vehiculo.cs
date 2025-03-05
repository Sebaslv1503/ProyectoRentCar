using System.Data.SqlClient;
using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace Taller1.Controllers
{
    public class Vehiculo : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<VehiculoCLS> listarVehiculo()
        {
            return VehiculoBL.ListarVehiculos();
        }

        public JsonResult registrarVehiculo(VehiculoCLS obj, IFormFile imagenFile)
        {
            if (imagenFile == null || imagenFile.Length == 0)
            {
                return Json(new { success = false, message = "Debe subir una imagen válida." });
            }

            bool registrado = VehiculoBL.RegistrarVehiculo(obj, imagenFile);
            return Json(new { success = registrado });
        }
        public IActionResult ObtenerImagen(int idVehiculo)
        {
            byte[] imagenBytes = VehiculoBL.ObtenerImagenVehiculo(idVehiculo);

            if (imagenBytes != null)
            {
                return File(imagenBytes, "image/png"); // Asumiendo que la imagen es PNG
            }

            return NotFound(); // Si no hay imagen, devolver error 404
        }
        public JsonResult eliminarVehiculo(int id)
        {
            bool eliminado = VehiculoBL.EliminarVehiculo(id);
            return Json(new { success = eliminado });
        }

        public JsonResult modificarVehiculo(VehiculoCLS obj, IFormFile imagenFile)
        {
            

            bool modificado = VehiculoBL.ModificarVehiculo(obj, imagenFile);
            return Json(new { success = modificado });
        }

        public List<VehiculoCLS> filtrarVehiculo(VehiculoCLS filtro)
        {
            if (filtro == null)
            {
                filtro = new VehiculoCLS();
            }
            
            return VehiculoBL.ObtenerVehiculosFiltrados(filtro);
        }

        [HttpGet]
        public JsonResult ObtenerVehiculo(int id)
        {
            VehiculoCLS vehiculo = VehiculoBL.ObtenerVehiculoPorId(id);

            if (vehiculo != null)
            {
                return Json(new { success = true, data = vehiculo });
            }

            return Json(new { success = false, message = "Vehículo no encontrado." });
        }
        [HttpPost]
        public JsonResult DevolverVehiculo(int id)
        {
            bool resultado = VehiculoBL.DevolverVehiculo(id);
            return Json(new { success = resultado });
        }





    }
}
