window.onload = function () {
    listarPagos();

}

async function listarPagos() {

    objPagos = {
        url: "pago/ListarPagos",
        cabeceras: ["Id Reserva", "Fecha Inicio", "Fecha Final", "Marca", "Modelo", "Precio Total", "Imagen"],
        propiedades: ["idPago", "fechaInicio", "fechaFin", "marcaVehiculo", "modeloVehiculo", "montoPago", "imagenVehiculo"],
        devolver: true,
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
            window.location.reload(); 
        } else {
            alert("Error al registrar el pago. Verifique la reserva.");
        }
    });
}

async function devolver(idVehiculo) {
    let result = await Swal.fire({
        title: "¿Devolver vehículo?",
        text: "Esta acción marcará la reserva como cancelada.",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Sí, devolver",
        cancelButtonText: "Cancelar"
    });

    if (!result.isConfirmed) {
        return;
    }

    try {
        let res = await fetch(`/Vehiculo/DevolverVehiculo/${idVehiculo}`, {
            method: "POST",
            headers: { "Content-Type": "application/json" }
        });

        let data = await res.json();

        if (data.success) {
            await Swal.fire({
                title: "Éxito",
                text: "El vehículo ha sido devuelto y la reserva cancelada.",
                icon: "success",
                confirmButtonText: "Aceptar"
            });
            location.reload();
        } else {
            Swal.fire({
                title: "Error",
                text: "No se pudo completar la devolución.",
                icon: "error",
                confirmButtonText: "Aceptar"
            });
        }
    } catch (error) {
        Swal.fire({
            title: "Error",
            text: "No se pudo conectar con el servidor.",
            icon: "error",
            confirmButtonText: "Aceptar"
        });
    }
}



