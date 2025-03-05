using System.Text.Json;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace Taller1.Controllers
{
    public class EmpleadoController : Controller
    {
        public IActionResult DashboardEmpleado()
        {
            if (HttpContext.Session.GetString("Rol") != "Empleado")
            {
                return RedirectToAction("Login", "Usuario"); 
            }

            return View();
        }

        


    }
}
