window.onload = function () {
    listarClientes();
}



async function listarClientes() {
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
}