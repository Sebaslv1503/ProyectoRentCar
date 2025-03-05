using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class SeguroBL
    {
        public static bool RegistrarSeguro(SeguroCLS obj)
        {
            SeguroDAL seguroDAL = new SeguroDAL();
            return seguroDAL.RegistrarSeguro(obj);
        }
        public List<SeguroCLS> ListarSeguros()
        {
            SeguroDAL seguroDAL = new SeguroDAL();
            return seguroDAL.ListarTodosLosSeguros();
        }

    }
}
