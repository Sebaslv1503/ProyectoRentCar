
async function guardarReserva() {
    
    let result = await Swal.fire({
        title: "Confirmar Reserva",
        text: "¿Estás seguro de que deseas guardar esta reserva?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Sí, guardar",
        cancelButtonText: "Cancelar"
    });

    
    if (!result.isConfirmed) {
        return;
    }

    let form = document.getElementById("frmReserva");
    let frm = new FormData(form);

    try {
        let res = await fetch("/Reserva/GuardarReserva", {
            method: "POST",
            body: frm
        });

        let data = await res.json();

        if (data.success === true) {
            
            
        } else {
            await Swal.fire({
                title: "¡Éxito!",
                text: "Reserva guardada correctamente.",
                icon: "success",
                confirmButtonText: "Aceptar"
            });

            window.location.href = "/Reserva/GestionReserva";
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




function agendar(idVehiculo) {
    window.location.href = "/Reserva/Reservacion?idVehiculo=" + idVehiculo;
}




