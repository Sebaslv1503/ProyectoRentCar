using Microsoft.AspNetCore.Mvc;

namespace Taller1.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult DashboardCliente()
        {
            
            if (HttpContext.Session.GetString("Rol") != "Cliente")
            {
                return RedirectToAction("Login", "Usuario"); // Redirige si no es cliente
            }

            return View();
        }
    }
}
