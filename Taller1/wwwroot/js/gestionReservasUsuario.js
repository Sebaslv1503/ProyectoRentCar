window.onload = function () {
    listarReservas();

}

async function listarReservas() {

    objReservas = {
        url: "reserva/ListarReservas",
        cabeceras: ["Id Reserva", "Fecha Inicio", "Fecha Final", "Marca", "Modelo", "Año", "Precio x Dia", "Estado de la Reserva"],
        propiedades: ["id", "fechaInicio", "fechaFin", "marcaVehiculo", "modeloVehiculo", "anioVehiculo", "precioVehiculo", "estadoReserva"],
        
        eliminar: true,
        continuar: true,
        divContenedorTabla: "divTable"

    }
    pintar(objReservas);
}

document.addEventListener("click", async function (event) {
    if (event.target.closest(".eliminar-btn")) {
        let id = event.target.closest(".eliminar-btn").dataset.id;
        let url4 = `reserva/EliminarReserva?idReserva=${id}`;


        window.Swal.fire({
            title: "¿Estás seguro de eliminar esta reserva?",
            text: "Esta acción eliminará la reserva permanentemente.",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Sí, eliminar",
            cancelButtonText: "Cancelar"
        }).then(async (result) => {
            if (result.isConfirmed) {

                let formData = new FormData();
                formData.append("id", id);


                await fetchPost(url4, "json", formData, function (res) {

                    exitoAlert();
                    listarReservas();

                });
            }
        });
    }
});
document.addEventListener("click", async function (event) {

    if (event.target.closest(".continuar-btn")) {
        let id = event.target.closest(".continuar-btn").dataset.id;
        document.getElementById("reservaId").value = id;
        obtenerReserva(id);
    }
});

function actualizarSeguro() {
    let tipoSeguro = document.getElementById("tipoSeguro").value;
    let costoSeguro = 0;
    let descripcion = "";

    switch (tipoSeguro) {
        case "Básico":
            costoSeguro = 10;
            descripcion = "Cobertura mínima contra daños menores y accidentes básicos.";
            break;
        case "Intermedio":
            costoSeguro = 25;
            descripcion = "Cobertura media con protección contra robo y daños más extensos.";
            break;
        case "Premium":
            costoSeguro = 50;
            descripcion = "Cobertura total incluyendo responsabilidad civil y asistencia 24/7.";
            break;
        default:
            descripcion = "Seleccione un seguro para ver la descripción.";
    }

    document.getElementById("costo").value = costoSeguro;
    document.getElementById("descripcionSeguro").textContent = descripcion;

    // 📌 Volver a calcular el total con el nuevo seguro
    calcularTotalPago();
}

async function obtenerReserva(idReserva) {
    let res = await fetch(`/Reserva/ObtenerReservaCompleta?id=${idReserva}`);
    let data = await res.json();

    if (data.success) {
        let reserva = data.data;

        // Asignar datos al modal
        document.getElementById("reservaId").textContent = idReserva;
        document.getElementById("nombreCliente").textContent = reserva.nombreCliente + " " + reserva.apellidoCliente;
        document.getElementById("emailCliente").textContent = reserva.emailCliente;
        document.getElementById("telefonoCliente").textContent = reserva.telefonoCliente;

        document.getElementById("marcaVehiculo").textContent = reserva.marcaVehiculo;
        document.getElementById("modeloVehiculo").textContent = reserva.modeloVehiculo;
        document.getElementById("anioVehiculo").textContent = reserva.anioVehiculo;
        document.getElementById("precioVehiculo").textContent = "$" + reserva.precioVehiculo;
        document.getElementById("estadoVehiculo").textContent = reserva.estadoVehiculo;
        document.getElementById("categoriaVehiculo").textContent = reserva.categoriaVehiculo;
        document.getElementById("imagenVehiculo").src = reserva.imagenVehiculo;

        //Input Ocultos
        document.getElementById("idReservaInput").value = idReserva;
        

        function formatFecha(fecha) {
            if (!fecha) return ""; 
            return new Date(fecha).toISOString().split('T')[0]; 
        }

        document.getElementById("fechaInicio").value = formatFecha(reserva.fechaInicio) || new Date().toISOString().split('T')[0];
        document.getElementById("fechaFin").value = formatFecha(reserva.fechaFin) || new Date(new Date().setDate(new Date().getDate() + 1)).toISOString().split('T')[0];

        let fechaInicio = document.getElementById("fechaInicio").value;
        let fechaFin = document.getElementById("fechaFin").value;

        
        calcularTotalPago(fechaInicio, fechaFin, reserva.precioVehiculo);
        
        
        document.getElementById("fechaPago").textContent = reserva.fechaPago ? reserva.fechaPago : "No pagado";

        
        let fechaActual = new Date();

        
        let año = fechaActual.getFullYear();
        let mes = String(fechaActual.getMonth() + 1).padStart(2, '0'); 
        let dia = String(fechaActual.getDate()).padStart(2, '0');

        let fechaFormateada = `${año}-${mes}-${dia}`;

        // Asignar la fecha al input
        document.getElementById('fechaPago').value = fechaFormateada;

    } else {
        alert("❌ No se encontró la reserva.");
    }
}

function calcularTotalPago(fechaInicio, fechaFin, precioVehiculo) {
    let fecha1 = new Date(fechaInicio);
    let fecha2 = new Date(fechaFin);

    

    
    let diferenciaDias = Math.ceil((fecha2 - fecha1) / (1000 * 60 * 60 * 24));
    if (diferenciaDias <= 0) diferenciaDias = 1; 
    let totalAlquiler = diferenciaDias * precioVehiculo;

    
    let costoSeguro = parseFloat(document.getElementById("costo").value) || 0;

    
    let totalPagar = totalAlquiler + costoSeguro;

    
    document.getElementById("montoPago").textContent = `$${totalPagar.toFixed(2)}`;

    document.getElementById("montoPagoInput").value = totalPagar;
}

document.getElementById("fechaInicio").addEventListener("change", function () {
    let fechaInicio = this.value;
    let fechaFin = document.getElementById("fechaFin").value;
    let precioVehiculo = parseFloat(document.getElementById("precioVehiculo").textContent.replace("$", "")) || 0;

    calcularTotalPago(fechaInicio, fechaFin, precioVehiculo);
});

document.getElementById("fechaFin").addEventListener("change", function () {
    let fechaInicio = document.getElementById("fechaInicio").value;
    let fechaFin = this.value;
    let precioVehiculo = parseFloat(document.getElementById("precioVehiculo").textContent.replace("$", "")) || 0;

    calcularTotalPago(fechaInicio, fechaFin, precioVehiculo);
});


document.getElementById("tipoSeguro").addEventListener("change", function () {
    let fechaInicio = document.getElementById("fechaInicio").value;
    let fechaFin = document.getElementById("fechaFin").value;
    let precioVehiculo = parseFloat(document.getElementById("precioVehiculo").textContent.replace("$", "")) || 0;

    calcularTotalPago(fechaInicio, fechaFin, precioVehiculo);
    
    calcularTotalPago(fechaInicio, fechaFin, precioVehiculo);
});

async function confirmarReservaJS() {
    let form = document.getElementById("frmReservaCompleta");
    let frm = new FormData(form);
    alert("aaaa");
    let res = await fetch("/Reserva/ConfirmarReserva", {
        method: "POST",
        body: frm
    });

    let data = await res.json();
    listarReservas();
    if (data.success) {
        alert("✅ Reserva confirmada con éxito.");
        window.location.reload();
    } else {
        alert("❌ Error al confirmar la reserva.");
    }
}
