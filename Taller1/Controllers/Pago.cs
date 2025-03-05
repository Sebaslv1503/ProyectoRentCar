using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace Taller1.Controllers
{
    public class Pago : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PagoAdmin()
        {
            return View();
        }
        public List<ReservaCLS> ListarPagos()
        {
            int usuarioId = HttpContext.Session.GetInt32("Id") ?? 0;
            return PagoBL.ListarPagos(usuarioId);
        }
        public List<ReservaCLS> ListarTodosPagos()
        {
            
            return PagoBL.ListarPagos();
        }
        public bool EliminarPago(int id)
        {
            return PagoBL.EliminarPago(id);
        }
    }
}
