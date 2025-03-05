window.onload = function () {
    listarTodosPagos();

}

async function listarTodosPagos() {

    objPagos = {
        url: "pago/ListarTodosPagos",
        cabeceras: ["Id Reserva", "Fecha Inicio", "Fecha Final", "Marca", "Modelo", "Precio Total", "Imagen"],
        propiedades: ["idPago", "fechaInicio", "fechaFin", "marcaVehiculo", "modeloVehiculo", "montoPago", "imagenVehiculo"],
        eliminar: true,
        divContenedorTabla: "divTable"

    }
    pintar(objPagos);
}

