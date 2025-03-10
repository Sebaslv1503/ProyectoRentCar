﻿window.onload = function () {
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

document.addEventListener("click", async function (event) {
    if (event.target.closest(".eliminar-btn")) {
        let id = event.target.closest(".eliminar-btn").dataset.id;
        let url4 = `pago/EliminarPago?id=${id}`;


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
                    listarTodosPagos();

                });
            }
        });
    }
});