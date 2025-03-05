using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;
using Microsoft.AspNetCore.Http;

namespace CapaNegocio
{
    public class VehiculoBL
    {
        public static bool RegistrarVehiculo(VehiculoCLS obj, IFormFile imagenFile)
        {
            VehiculoDAL vehiculo = new VehiculoDAL();
            return vehiculo.RegistrarVehiculo(obj, imagenFile);
        }
        public static byte[] ObtenerImagenVehiculo(int idVehiculo)
        {
            VehiculoDAL vehiculoDAL = new VehiculoDAL();
            return vehiculoDAL.ObtenerImagenVehiculo(idVehiculo);
        }

        public static List<VehiculoCLS> ListarVehiculos()
        {
            VehiculoDAL vehiculoDAL = new VehiculoDAL();
            return vehiculoDAL.ListarVehiculos();
        }

        public static bool EliminarVehiculo(int idVehiculo)
        {
            VehiculoDAL vehiculoDAL = new VehiculoDAL();
            return vehiculoDAL.EliminarVehiculo(idVehiculo);
        }
        public static bool  ModificarVehiculo(VehiculoCLS obj, IFormFile imagenFile)
        {
            VehiculoDAL vehiculoDAL = new VehiculoDAL();
            return vehiculoDAL.ModificarVehiculo(obj, imagenFile);
        }
        public static int ObtenerCantidadVehiculos()
        {
            VehiculoDAL vehiculoDAL = new VehiculoDAL();
            return vehiculoDAL.ContarVehiculos();
        }
        public static List<VehiculoCLS> ObtenerVehiculosFiltrados(VehiculoCLS filtro)
        {
            VehiculoDAL vehiculoDAL = new VehiculoDAL();
            return vehiculoDAL.FiltrarVehiculos(filtro);
        }

        public static VehiculoCLS ObtenerVehiculoPorId(int id)
        {
            VehiculoDAL vehiculoDAL = new VehiculoDAL();
            return vehiculoDAL.ObtenerVehiculoPorId(id);
        }





    }
}
