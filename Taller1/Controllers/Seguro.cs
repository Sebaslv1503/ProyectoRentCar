using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace Taller1.Controllers
{
    public class Seguro : Controller
    {
        public IActionResult segurosAdmin()
        {
            return View();
        }
        public List<SeguroCLS> listarSeguros()
        {
            SeguroBL seguroBL = new SeguroBL();
            return seguroBL.ListarSeguros();
        }
    }
}
