window.onload = function () {
    listarSeguros();

}

async function listarSeguros() {

    objPagos = {
        url: "seguro/listarSeguros",
        cabeceras: ["Id Seguro", "Reserva Id", "Tipo de Seguro ","Costo"],
        propiedades: ["id", "idReserva", "tipoSeguro", "precio"],
        
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