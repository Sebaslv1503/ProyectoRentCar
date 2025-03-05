window.onload = function () {
    listarReservas();

}

async function listarReservas() {

    objReservas = {
        url: "reserva/ListarTodasReservas",
        cabeceras: ["Id Reserva", "Fecha Inicio", "Fecha Final", "Marca", "Modelo", "Año", "Precio x Dia", "Estado de la Reserva"],
        propiedades: ["id", "fechaInicio", "fechaFin", "marcaVehiculo", "modeloVehiculo", "anioVehiculo", "precioVehiculo", "estadoReserva"],

        eliminar: true,
        
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