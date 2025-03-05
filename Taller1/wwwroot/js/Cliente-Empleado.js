window.onload = function () {
    listarEmpleados();
    //listarClientes();
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

/*async function listarClientes() {
    let objCliente = {
        url: "usuario/listarClientes",
        cabeceras: ["Id Cliente", "Nombre Usuario", "Nombre", "Apellido", "Password", "Teléfono", "Correo"],
        propiedades: ["id", "nombreUsuario", "nombre", "apellido", "passwordHash", "telefono", "correo"],
        eliminar: true,
        editar: true,
        divContenedorTabla: "divTableClientes",
        id: "example1"
    };

    pintar(objCliente);
}*/