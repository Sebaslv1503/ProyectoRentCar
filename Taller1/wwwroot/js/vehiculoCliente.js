window.onload = function () {
    listarVehiculo();
    BuscarVehiculo();
     
}

let objVehiculo;

async function listarVehiculo() {

    objVehiculo = {
        url: "vehiculo/listarVehiculo",
        cabeceras: ["Marca", "Modelo", "Anio", "Precio x Dia", "Categoria", "Imagen"],
        propiedades: ["marca", "modelo", "anio", "precio", "categoria", "imagenString"],
        eliminar: false,
        editar: false,
        divContenedorTabla: "divTable",
        agregar: true

    }
    pintar(objVehiculo);
}
function BuscarVehiculo() {
    let form = document.getElementById("frmBusqueda");
    //Constructor que nos trae toda la informacion 
    let frm = new FormData(form);

    fetchPost("vehiculo/filtrarVehiculo", "json", frm, function (res) {
        document.getElementById("divTable").innerHTML = generarTabla(res);
    });
}


function mostrarSubDuracion() {
    let duracion = document.getElementById("duracion").value;
    let subduracion = document.getElementById("subduracion");

    subduracion.innerHTML = ""; // Vaciar opciones previas
    subduracion.classList.remove("d-none");

    if (duracion === "dias") {
        for (let i = 1; i <= 6; i++) {
            subduracion.innerHTML += `<option value="${i}">${i} día(s)</option>`;
        }
    } else if (duracion === "semanas") {
        for (let i = 1; i <= 4; i++) {
            subduracion.innerHTML += `<option value="${i}">${i} semana(s)</option>`;
        }
    } else if (duracion === "meses") {
        for (let i = 1; i <= 12; i++) {
            subduracion.innerHTML += `<option value="${i}">${i} mes(es)</option>`;
        }
    } else {
        subduracion.classList.add("d-none"); // Ocultar si no hay selección
    }
}

// Función para limpiar el formulario
function limpiarFormulario() {

    document.getElementById("frmBusqueda").reset();
    document.getElementById("subduracion").classList.add("d-none");
    BuscarVehiculo();
}





