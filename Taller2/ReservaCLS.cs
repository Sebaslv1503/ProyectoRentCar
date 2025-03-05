using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ReservaCLS
    {
        public int id { get; set; }

        //Atributos Usuario Completo
        public int idCliente { get; set; }
        public int idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string nombreCliente { get; set; }
        public string apellidoCliente { get; set; }
        public string emailCliente { get; set; }
        public string telefonoCliente { get; set; }

        // Datos del Vehículo
        public int idVehiculo { get; set; }
        public string marcaVehiculo { get; set; }
        public string modeloVehiculo { get; set; }
        public int anioVehiculo { get; set; }
        public decimal precioVehiculo { get; set; }
        public string estadoVehiculo { get; set; }
        public string categoriaVehiculo { get; set; }
        public string imagenVehiculo { get; set; }

        // Datos de la Reserva
        
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public string estadoReserva { get; set; }

        // Datos del Pago (puede ser nulo)
        public int idPago { get; set; }
        public decimal montoPago { get; set; }
        public string metodoPago { get; set; }
        public DateTime? fechaPago { get; set; }

        // Datos del Seguro (puede ser nulo)
        public string tipoSeguro { get; set; }
        public decimal costoSeguro { get; set; }
        











    }
}
