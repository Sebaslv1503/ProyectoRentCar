window.onload = function () {
    listarVehiculo();

}

async function listarVehiculo() {

    objVehiculo = {
        url: "vehiculo/listarVehiculo",
        cabeceras: ["Id Vehiculo", "Marca", "Modelo","Anio", "Precio", "Estado", "Categoria","Imagen" ],
        propiedades: ["id", "marca", "modelo", "anio", "precio", "estado","categoria", "imagenString"],
        eliminar: true,
        editar: true,
        divContenedorTabla: "divTable"

    }
    pintar(objVehiculo);
}

async function registroVehiculoJS() {
    let form = document.getElementById("frmVehiculo");
    let frm = new FormData(form);
    window.Swal.fire({
        title: "¿Estás seguro de registrar este vehiculo? ",
        text: "Esta acción eliminará el vehiculo permanentemente.",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Sí, eliminar",
        cancelButtonText: "Cancelar"
    }).then(async (result) => {
        if (result.isConfirmed) {
            await fetchPost("vehiculo/registrarVehiculo", "json", frm, function (res) {
                exitoAlert();
                listarVehiculo();
                
            });

        }
    });

    
}


document.addEventListener("DOMContentLoaded", function () {
    idMarcaRegistro = "marca";
    idAnioRegistro = "anio";
    idMarcaModificar = "marcaM";
    idAnioModificar = "anioM";
    llenarMarcas(idMarcaRegistro);
    llenarAnios(idAnioRegistro);
    llenarMarcas(idMarcaModificar);
    llenarAnios(idAnioModificar);
});


const marcas = ["Toyota", "Ford", "Chevrolet", "Honda", "BMW", "Nissan", "Mazda", "Volkswagen", "Hyundai", "Kia"];


function llenarMarcas(id) {
    let selectMarca = document.getElementById(id);
    marcas.forEach(marca => {
        let option = document.createElement("option");
        option.value = marca;
        option.textContent = marca;
        selectMarca.appendChild(option);
    });
}


function llenarAnios(id) {
    let selectAnio = document.getElementById(id);
    let yearActual = new Date().getFullYear();

    for (let i = yearActual; i >= 2000; i--) {
        let option = document.createElement("option");
        option.value = i;
        option.textContent = i;
        selectAnio.appendChild(option);
    }
}

document.addEventListener("click", async function (event) {
    if (event.target.closest(".eliminar-btn")) {
        let id = event.target.closest(".eliminar-btn").dataset.id;
        let url4 = `vehiculo/eliminarVehiculo?id=${id}`;


        window.Swal.fire({
            title: "¿Estás seguro de eliminar el vehiculo con el id " + id,
            text: "Esta acción eliminará el vehiculo permanentemente.",
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
                    listarVehiculo();
                    
                });
            }
        });
    }
});

//Rellenar Modal Modificar
document.addEventListener("click", function (event) {
    if (event.target.closest(".modificar-btn")) {
        let btn = event.target.closest(".modificar-btn");
        let tr = btn.closest("tr");

        if (!tr) {
            console.error("Error: No se encontró la fila <tr>");
            return;
        }

        let id = btn.dataset.id;
        let tds = tr.querySelectorAll("td");

        if (tds.length < 7) { 
            console.error("Error: No se encontraron suficientes celdas en la fila.");
            return;
        }

        let marca = tds[1].textContent.trim();
        let modelo = tds[2].textContent.trim();
        let anio = tds[3].textContent.trim();
        let precio = tds[4].textContent.trim();
        let estado = tds[5].textContent.trim();
        let categoria = tds[6].textContent.trim();

        document.getElementById("marcaM").value = marca;
        document.getElementById("modeloM").value = modelo;
        document.getElementById("anioM").value = anio;
        document.getElementById("precioM").value = precio;
        document.getElementById("estadoM").value = estado;
        document.getElementById("categoriaM").value = categoria;

        document.getElementById("txtId").value = id;
        

        
    }
});



async function modificarVehiculoJS() {
    let form = document.getElementById("frmModificar");
    let frm = new FormData(form);
    window.Swal.fire({
        title: "¿Estás seguro de registrar este vehiculo? ",
        text: "Esta acción eliminará el vehiculo permanentemente.",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Sí, eliminar",
        cancelButtonText: "Cancelar"
    }).then(async (result) => {
        if (result.isConfirmed) {
            await fetchPost("vehiculo/modificarVehiculo", "json", frm, function (res) {
                exitoAlert();
                listarVehiculo();

            });

        }
    });


}
