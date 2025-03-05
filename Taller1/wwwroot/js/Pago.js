window.onload = function () {
    listarPagos();

}

async function listarPagos() {

    objPagos = {
        url: "pago/ListarPagos",
        cabeceras: ["Id Reserva", "Fecha Inicio", "Fecha Final", "Marca", "Modelo", "Precio Total", "Imagen"],
        propiedades: ["idPago", "fechaInicio", "fechaFin", "marcaVehiculo", "modeloVehiculo", "montoPago", "imagenVehiculo"],
        divContenedorTabla: "divTable"

    }
    pintar(objPagos);
}

async function registrarPagoJS() {
    let form = document.getElementById("frmPago");
    let frm = new FormData(form);

    await fetchPost("pago/registrarPago", "json", frm, function (res) {
        if (res.success) {
            alert("¡Pago registrado con éxito!");
            window.location.reload(); // Recargar la página para actualizar la lista de pagos
        } else {
            alert("Error al registrar el pago. Verifique la reserva.");
        }
    });
}

