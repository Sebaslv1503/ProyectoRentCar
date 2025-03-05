async function guardarReserva() {
    let form = document.getElementById("frmReserva");
    let frm = new FormData(form);

    let res = await fetch("/Reserva/GuardarReserva", {
        method: "POST",
        body: frm
    });

    let data = await res.json();

    if (data.success === true) {
        alert("Reserva realizada con éxito");
        


    } else {
        alert("Reserva Generada Exitosamente");
        window.location.href = "/Reserva/GestionReserva";
    }

    

}


function agendar(idVehiculo) {
    window.location.href = "/Reserva/Reservacion?idVehiculo=" + idVehiculo;
}




