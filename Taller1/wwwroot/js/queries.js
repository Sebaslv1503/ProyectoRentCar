function get(idControl) {
    return document.getElementById(idControl).value;
}
function set(idControl, valor) {
    return document.getElementById(idControl).value = valor;
}

function LimpiarDatos(idFormulario) {
    let elementsName = document.querySelectorAll('#' + idFormulario + " [name]");

    elementsName.forEach(element => {
        element.value = "";
    });
}

async function fetchGet(url, tipoRespuesta, callback) {
    try {
        let urlCompleta = window.location.protocol + "//" + window.location.host + "/" + url;
        let res = await fetch(urlCompleta);

        if (!res.ok) {
            throw new Error(`Error en la petición: ${res.status} ${res.statusText}`);
        }

        let data;
        if (tipoRespuesta === "json") {
            data = await res.json();
        } else if (tipoRespuesta === "text") {
            data = await res.text();
        } else {
            data = null;
        }

        callback(data);
    } catch (e) {
        console.error("Error en fetchGet:", e);
        callback(null);
    }
}

async function fetchPost(url, tipoRespuesta, frm, callback) {
    try {
        let urlCompleta = window.location.protocol + "//" + window.location.host + "/" + url;

        let res = await fetch(urlCompleta, {
            method: "POST",
            body: frm
        });

        if (!res.ok) {
            throw new Error(`Error en la petición: ${res.status} ${res.statusText}`);
        }

        let data;
        if (tipoRespuesta === "json") {
            data = await res.json();
        } else if (tipoRespuesta === "text") {
            data = await res.text();
        } else {
            data = null;
        }

        callback(data);

    } catch (e) {
        alert("Ocurrió un problema en POST");
    }
}

let objConfiguracionGlobal;

function pintar(objConfiguracion) {
    objConfiguracionGlobal = objConfiguracion;

    let divID = objConfiguracionGlobal.divContenedorTabla || "divContenedorTabla";

    

    fetchGet(objConfiguracion.url, "json", function (res) {
        let contenido = "<div id='" + divID + "'>";
        contenido += generarTabla(res);
        contenido += "</div>";

        document.getElementById(divID).innerHTML = contenido;
    });
}

function generarTabla(res) {
    let contenido = "";
    let id = objConfiguracionGlobal.id || "example";
    let cabeceras = objConfiguracionGlobal.cabeceras;
    let propiedades = objConfiguracionGlobal.propiedades;
   

    contenido += "<table class='table display' id="+id+" style='width: 100%'>";
    contenido += "<thead><tr>";

    // Generar encabezados de la tabla
    for (let i = 0; i < cabeceras.length; i++) {
        contenido += "<th>" + cabeceras[i] + "</th>";
    }

    if (objConfiguracionGlobal.editar || objConfiguracionGlobal.eliminar || objConfiguracionGlobal.agregar || objConfiguracionGlobal.continuar) {
        contenido += "<th>Operaciones</th>";
    }

    contenido += "</tr></thead><tbody>";

    // Generar filas dinámicamente
    for (let i = 0; i < res.length; i++) {
        let obj = res[i];
        contenido += "<tr>";

        for (let j = 0; j < propiedades.length; j++) {
            let propiedad = propiedades[j];

            
            
            if (propiedad === "imagenString" || propiedad ===  "imagenVehiculo") {
                if (obj[propiedad]) {
                    contenido += `<td><img src="${obj[propiedad]}" alt="Vehículo" width="100"></td>`;
                } else {
                    contenido += `<td>Sin imagen</td>`;
                }
            } else if (propiedad === "estado") {
                let icono = "";
                let color = "";

                if (obj[propiedad] === "Disponible") {
                    icono = '<i class="fa fa-check-circle-o"></i>'; 
                    color = "green";
                } else if (obj[propiedad] === "Reservado") {
                    icono = '<i class="fa fa-ban"></i>'; 
                    color = "red";
                } else if (obj[propiedad] === "Mantenimiento") {
                    icono = "🛠";
                    color = "orange";
                }

                contenido += `<td>${obj[propiedad]} <span style="color:${color};">${icono}</span></td>`;
            }
            
            else if (propiedad === "categoria") {
                let icono = "";
                let color = "";

                if (obj[propiedad] === "Economico") {
                    icono = '<i class="fa fa-money"></i>'; // Ícono de billetera
                    color = "blue";
                } else if (obj[propiedad] === "Standar") {
                    icono = '<i class="fa fa-car"></i>'; // Ícono de auto estándar
                    color = "black";
                } else if (obj[propiedad] === "Deluxe") {
                    icono = '<i class="fa fa-diamond"></i>'; // Ícono de diamante
                    color = "purple";
                }

                contenido += `<td>${obj[propiedad]} <span style="color:${color};">${icono}</span></td>`;
            }
            // Cualquier otra propiedad se muestra normalmente
            else {
                contenido += `<td>${obj[propiedad]}</td>`;
            }
            
        }

        // Botones de editar/eliminar
        if (objConfiguracionGlobal.editar || objConfiguracionGlobal.eliminar || objConfiguracionGlobal.agregar || objConfiguracionGlobal.continuar) {
            contenido += "<td>";

            if (objConfiguracionGlobal.editar) {
                contenido += `<button class="btn btn-info modificar-btn" data-bs-toggle="modal" data-bs-target="#modalVehiculo" data-id="` + obj.id + `">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                        <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                        <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z"/>
                    </svg>
                </button>`;
            }

            contenido += " ";
            if (objConfiguracionGlobal.eliminar) {
                contenido += `<i class="btn btn-danger eliminar-btn" data-id="` + obj.id + `">

                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash" viewBox="0 0 16 16">
                            <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0z"/>
                            <path d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4zM2.5 3h11V2h-11z"/>
                        </svg>
                    </i>`;
            }
            contenido += " ";
            if (objConfiguracionGlobal.agregar) {
                contenido += `
                <button class="btn btn-success" onclick="agendar(` + obj.id + `)">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-calendar-plus me-2" viewBox="0 0 16 16">
                        <path d="M8 7a.5.5 0 0 1 .5.5V9H10a.5.5 0 0 1 0 1H8.5v1.5a.5.5 0 0 1-1 0V10H6a.5.5 0 0 1 0-1h1.5V7.5A.5.5 0 0 1 8 7z"/>
                        <path d="M3.5 0a.5.5 0 0 1 .5.5V1h8V.5a.5.5 0 0 1 1 0V1h1a2 2 0 0 1 2 2v11a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2V3a2 2 0 0 1 2-2h1V.5a.5.5 0 0 1 .5-.5M1 4v10a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1V4z"/>
                    </svg>
                    Reservar
                </button>`;
            }
            contenido += " ";
            if (objConfiguracionGlobal.continuar) {
                contenido += `
                <i class="btn btn-success continuar-btn" data-bs-toggle="modal" data-bs-target="#exampleModal" data-id="` + obj.id + `">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-calendar-plus me-2" viewBox="0 0 16 16">
                        <path d="M8 7a.5.5 0 0 1 .5.5V9H10a.5.5 0 0 1 0 1H8.5v1.5a.5.5 0 0 1-1 0V10H6a.5.5 0 0 1 0-1h1.5V7.5A.5.5 0 0 1 8 7z"/>
                        <path d="M3.5 0a.5.5 0 0 1 .5.5V1h8V.5a.5.5 0 0 1 1 0V1h1a2 2 0 0 1 2 2v11a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2V3a2 2 0 0 1 2-2h1V.5a.5.5 0 0 1 .5-.5M1 4v10a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1V4z"/>
                    </svg>
                    Continuar
                </i>`;
            }

            contenido += "</td>";
        }

        contenido += "</tr>";
    }

    contenido += "</tbody></table>";

    // Inicializar DataTable después de generar la tabla
    setTimeout(() => {
        new DataTable("#"+id);
    }, 100);

    return contenido;
}

async function exitoAlert() {
    window.Swal.fire({
        title: "Modificado correctamente",
        icon: "success",
        timer: 2000,
        showConfirmButton: false
    });
}

function mostrarAlerta(titulo, mensaje, tipo, callback = null) {
    Swal.fire({
        title: titulo,
        text: mensaje,
        icon: tipo, 
        confirmButtonText: "Aceptar",
        allowOutsideClick: false
    }).then((result) => {
        if (result.isConfirmed && callback) {
            callback(); 
        }
    });
}

function mostrarConfirmacion(titulo, texto, tipo) {
    return Swal.fire({
        title: titulo,
        text: texto,
        icon: tipo,
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Sí, confirmar",
        cancelButtonText: "Cancelar"
    }).then((result) => {
        return result.isConfirmed;
    });
}


