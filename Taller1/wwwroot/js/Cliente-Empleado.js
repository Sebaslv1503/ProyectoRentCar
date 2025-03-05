window.onload = function () {
    listarEmpleados();
}

async function listarEmpleados() {
    let objEmpleado = {
        url: "usuario/listarEmpleados",
        cabeceras: ["Id Empleado", "Nombre Usuario", "Nombre", "Apellido", "Password", "Cargo"],
        propiedades: ["id", "nombreUsuario", "nombre", "apellido", "passwordHash", "cargo"],
        eliminar: true,
        editar: true,
        divContenedorTabla: "divTableEmpleados",
        id: "example2"
    };

    pintar(objEmpleado);
}

