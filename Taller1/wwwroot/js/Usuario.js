async function registroUsuarioJS() {

    let form = document.getElementById("frmRegistroUsuario");
    //Constructor que nos trae toda la informacion 
    let frm = new FormData(form);


    await fetchPost("usuario/registrarUsuario", "json", frm, function (res) {
        
            alert("¡Registro exitoso! Redirigiendo al Login...");
            window.location.href = "/Usuario/Login"; 
        
    });
}


async function verificarUsuarioJS() {
    let form = document.getElementById("frmLogin");
    let frm = new FormData(form);

    let res = await fetch("/Usuario/VerificarUsuario", {
        method: "POST",
        body: frm
    });

    let data = await res.json();

    if (data.success) {
        exitoAlert();

        // Redirigir según el rol
        if (data.rol === "Empleado") {
            window.location.href = "/Empleado/DashboardEmpleado";
        } else if (data.rol === "Cliente") {
            window.location.href = "/Cliente/DashboardCliente";
        }
    } else {
        alert("ERROR EN LOGIN");
    }
}

async function cerrarSesion() {
    let res = await fetch("/Usuario/CerrarSesion", {
        method: "POST"
    });

    if (res.ok) {
        await Swal.fire({
            title: "Sesión cerrada",
            text: "Has cerrado sesión exitosamente.",
            icon: "success",
            confirmButtonText: "Aceptar"
        });

        window.location.href = "/Usuario/Login";
    } else {
        Swal.fire({
            title: "Error",
            text: "No se pudo cerrar la sesión.",
            icon: "error",
            confirmButtonText: "Aceptar"
        });
    }
}



async function obtenerEstadisticas() {
    try {
        let resVehiculos = await fetch("/usuario/ObtenerCantidadVehiculos");
        let resClientes = await fetch("/usuario/ObtenerCantidadClientes");

        if (!resVehiculos.ok || !resClientes.ok) {
            throw new Error(`Error en la respuesta del servidor: Vehiculos (${resVehiculos.status}), Clientes (${resClientes.status})`);
        }

        let dataVehiculos = await resVehiculos.json();
        let dataClientes = await resClientes.json();

        

        document.getElementById("cantidadVehiculos").textContent = dataVehiculos.cantidad;
        document.getElementById("cantidadClientes").textContent = dataClientes.cantidad;
    } catch (error) {
        console.error("Error obteniendo estadísticas:", error);
        document.getElementById("cantidadVehiculos").textContent = "Error";
        document.getElementById("cantidadClientes").textContent = "Error";
    }
}





