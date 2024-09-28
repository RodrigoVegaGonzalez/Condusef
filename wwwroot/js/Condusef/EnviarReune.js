import { ESTATUS_TICKET, NOMBRE_MESES, ID_MESES, ID_PERIODO_REDECO, TIPO_DOCUMENTO } from "../constantes.js";


$(document).ready(function () {
    //validarPeriodo();
    $('#modalSubirArchivo').on('show.bs.modal', function () {
        obtenerPeriodo();
    });

    $('#modalSubirArchivo').on('hidden.bs.modal', function (event) {
        // Aquí puedes ejecutar el código que deseas cuando el modal se cierra
        $('#periodo-input').val('');
        $('#year-input').val('');
        $('#inputname').val('');
    });
})

function CambiaFakePath(input) {
    var FileName = input.value.toString().replace("C:\\fakepath\\", "");
    $("#inputname").val(FileName);
};

function CambiaFakePath2(input) {
    var FileName = input.value.toString().replace("C:\\fakepath\\", "");
    $("#inputname2").val(FileName);
};

var otablaTickets = $("#tablaTickets").DataTable({
    "ajax": {
        "url": urlConsultarTickets,
        "method": "POST",
        "datatype": "JSON",
        "cache": false,
        "data": {
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
        },

    },
    "scrollCOllapse": true,
    "autoWidth": false,
    processing: true,
    "language":
    {
        paginate:
        {
            first: textoScript.primero,
            previous: textoScript.anterior,
            next: textoScript.siguiente,
            last: textoScript.ultimo
        },
        aria: {
            first: textoScript.inicio,
            previous: textoScript.anterior,
            next: textoScript.siguiente,
            last: textoScript.ultimo
        },
        emptyTable: textoScript.sinDatos,
        info: "",
        search: textoScript.buscar,
        infoFiltered: "",
        infoEmpty: "",
        lengthMenu: `${textoScript.mostrar} _MENU_`,
        searchPlaceHolder: textoScript.buscando,
        processing: `<span style="width:100%;">${textoScript.cargandoRegistros}</br>`,
        "zeroRecords": textoScript.noseencontraron,
    },
    "columns":
        [
            { "data": "id", "name": "id", "autowidth": true },
            { "data": "ticket", "name": "ticket", "autowidth": true },
            {
                "data": null, render: function (data, type, row) {
                    return TIPO_DOCUMENTO[data.tipoDocumento]
                }
            },
            /*{ "data": "archivo", "name": "archivo", "autowidth": true },*/
            { "data": "año", "name": "año", "autowidth": true },
            {
                "data": null, render: function (data, type, row) {
                    return ID_PERIODO_REDECO[data.periodo]
                }
            },
            {
                "data": null, render: function (data, type, row) {
                    if (data.estatus === ESTATUS_TICKET.pendiente) return textoScript.pendiente;
                    else if (data.estatus === ESTATUS_TICKET.rechazado) return textoScript.rechazado;
                    else return textoScript.aceptado;
                }
            },
            { "data": "fechaEnvio", "name": "fechaEnvio", "autowidth": true },
            {
                "data": null, render: function (data, type, row) {
                    return `<button type="button" class="btn-detalles btn btn-sm btn-block btn-primary text-white text-center waves-effect waves-light" style="font-size:small">${textoScript.detalles}</button>`;
                }
            },
            {
                "data": null, render: function (data, type, row) {
                    return data.estatus === ESTATUS_TICKET.pendiente ? `<button type="button" class="btn-verificar btn btn-sm btn-block btn-warning text-white text-center waves-effect waves-light" style="font-size:small">${textoScript.verificar}</button>`
                        : '<span></span>';
                }
            },
        ],
    "columnDefs":
        [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [1],
                "width": "30%"
            },
            {
                "targets": [2],
                "width": "10%"
            },
            {
                "targets": [4, 6],
                "width": "15%"
            },
            {
                "targets": [3, 5, 7, 8],
                "width": "6%"
            },
        ]
});

var otablaArchivosTicket = $("#tablaArchivosTicket").DataTable({
    scrollCOllapse: true,
    select: false,
    bAutoWidth: false,
    processing: false,
    paging: false,
    searching: false,
    language:
    {
        paginate:
        {
            first: textoScript.primero,
            previous: textoScript.anterior,
            next: textoScript.siguiente,
            last: textoScript.ultimo
        },

        aria: {
            first: textoScript.inicio,
            previous: textoScript.anterior,
            next: textoScript.siguiente,
            last: textoScript.ultimo
        },
        select: {
            style: 'single'
        },
        emptyTable: textoScript.sinDatos,
        info: "",
        search: textoScript.buscar,
        infoFiltered: "",
        infoEmpty: "",
        lengthMenu: `${textoScript.mostrar} _MENU_`,
        searchPlaceHolder: textoScript.buscando,
        processing: `<span style="width:100%;">${textoScript.cargandoRegistros}</br>`,
        zeroRecords: textoScript.noseencontraron,
    },
    columns:
        [
            { "data": "id", "name": "id", "autowidth": true },
            { "data": "file_id", "name": "file_id", "autowidth": true },
            { "data": "file", "name": "file", "autowidth": true },
            {
                "data": null, render: function (data, type, row) {
                    return ValidarEstatus(data.status);
                }
            },
            {
                "data": null, render: function (data, type, row) {
                    return validarErrores(data.file_with_error);
                }
            },
            {
                "data": null, render: function (data, type, row) {
                    //console.log(data.status, data.file_with_error)
                    if (data.status !== "False" && data.file_with_error) {
                        return `<button type="button" class="btn-corregir btn btn-sm btn-block btn-primary text-white text-center waves-effect waves-light" style="font-size:small" data-bs-toggle="modal" data-bs-target="#modalCorregirArchivo">${textoScript.corregir}</button>`;
                    }
                    else if (data.status === "False") {
                        return `<button type="button" class="btn-eliminar btn btn-sm btn-block btn-danger text-white text-center waves-effect waves-light" style="font-size:small">${textoScript.eliminar}</button>`;
                    }
                    else {
                        return '<span></span>'
                    }
                }
            },
        ],
    columnDefs:
        [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [1],
                "width": "30%"
            },
            {
                "targets": [2],
                "width": "25%"
            },
            {
                "targets": [3,4],
                "width": "12%"
            },
            {
                "targets": [5],
                "width": "10%"
            },
        ]
});

$('#consulta-btn').on('click', function () {
    alert(ID_PERIODO_REDECO['Enero'])
    //$.ajax({
    //    url: urlConsultarTickets,
    //    method: "POST",
    //    cache: false,
    //    data: {
    //        __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
    //    },
    //    beforeSend: function () {
    //        showLoading();
    //    },
    //    complete: function () {
    //        hideLoading();
    //    },
    //    success: function (respuesta) {
    //        //console.log(respuesta)
    //    },
    //    error: function (respuesta) {
    //        console.error('Ocurrio un error:', respuesta)
    //        //toastr.clear();
    //        //toastr.error(textoScript.errorSimple);
    //    }
    //})
})

$('#tablaTickets tbody').on('click', 'button.btn-detalles', function () {
    // Obtener la fila correspondiente al botón
    var fila = otablaTickets.row($(this).closest('tr')).data();
    $.ajax({
        url: urlConsultarArchivosTicket,
        method: "POST",
        cache: false,
        data: {
            idTicket: fila.id,
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
        },
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        success: function (respuesta) {
            if (respuesta.success) {
                var data = new Array();
                $.each(respuesta.data, function (i, v) {
                    var Ries = {};
                    Ries.id = v.id;
                    Ries.file_id = v.file_id;
                    Ries.status = v.status;
                    Ries.file = v.file;
                    Ries.file_with_error = v.file_with_error;
                    data.push(Ries);
                });
                //console.log(data)
                $('#idTicket1').val(fila.id);
                $('#Ticket1').val(fila.ticket);
                ConstruirTablaElementos1(otablaArchivosTicket, data);
                //$('#modalArchivos').show();
                //console.log('j')
                $('#abrir-btn').click();
            } else {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Something went wrong!",
                });
            }

        },
        error: function (respuesta) {
            console.error('Ocurrio un error:', respuesta)
            //toastr.clear();
            //toastr.error(textoScript.errorSimple);
        }
    })

});

function ConstruirTablaElementos1(elementoTabla, arregloDatos) {
    elementoTabla.rows().remove();
    if (!/undefined|NaN|null/.test(arregloDatos)) {
        //console.log('dibujar')
        elementoTabla.rows.add(arregloDatos).draw(true);
    }

    else {
        //console.log('no')
        elementoTabla.draw(true);
    }

}

$('#tablaTickets tbody').on('click', 'button.btn-verificar', function () {
    // Obtener la fila correspondiente al botón
    var fila = otablaTickets.row($(this).closest('tr')).data();
    $.ajax({
        url: urlConsultarEstatusTicket,
        method: "POST",
        cache: false,
        data: {
            idTicket: fila.id,
            ticket: fila.ticket,
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
        },
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        success: function (respuesta) {
            if (respuesta.success) {
                Swal.fire({
                    icon: "success",
                    title: textoScript.ticketActualizado,
                    showConfirmButton: false,
                    timer: 3500
                })
                setTimeout(() => {
                    location.reload();
                },4500)
                
            } else {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Something went wrong!",
                });
            }

        },
        error: function (respuesta) {
            console.error('Ocurrio un error:', respuesta)
            //toastr.clear();
            //toastr.error(textoScript.errorSimple);
        }
    })

});

$('#tablaArchivosTicket tbody').on('click', 'button.btn-eliminar', function () {
    // Obtener la fila correspondiente al botón
    Swal.fire({
        title: textoScript.eliminarTicket,
        text: textoScript.noPodraRevertir,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: textoScript.eliminar
    }).then((result) => {
        if (result.isConfirmed) {
            var fila = otablaTickets.row($(this).closest('tr')).data();
            $.ajax({
                url: urlEliminarReporte,
                method: "POST",
                cache: false,
                data: {
                    idTicket: fila.id,
                    ticket: fila.ticket,
                    __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
                },
                beforeSend: function () {
                    showLoading();
                },
                complete: function () {
                    hideLoading();
                },
                success: function (respuesta) {
                    if (respuesta.success) {
                        Swal.fire({
                            icon: "success",
                            title: textoScript.mEliminarTicketExitoso,
                            showConfirmButton: false,
                            timer: 2500
                        })
                    } else {
                        Swal.fire({
                            icon: "error",
                            title: textoScript.error,
                            text: textoScript.mErrorPeticion,
                        });
                    }

                },
                error: function (respuesta) {
                    console.error('Ocurrio un error:', respuesta)
                    //toastr.clear();
                    //toastr.error(textoScript.errorSimple);
                    Swal.fire({
                        icon: "error",
                        title: textoScript.error,
                        text: textoScript.mErrorPeticion,
                    });
                }
            })
        }
    });
    

});


$('#labelinput').on('change', function () {
    CambiaFakePath(this);
});

$('#labelinput2').on('change', function () {
    CambiaFakePath2(this);
});

$('#subirBtn').on('click', function () {
    subirRedeco();
});

$('#tablaArchivosTicket tbody').on('click', 'button.btn-corregir', function () {
    var fila = otablaTickets.row($(this).closest('tr')).data();
    //console.log(fila)
    $('#ticket-clave').val(fila.ticket);
    $('#idTicket').val(fila.id);
    $('#modalCorregirArchivo').show();
});

$('#corregirBtn').on('click', function () {
    corregirRedeco()
});

function subirRedeco() {
    let inputFile = document.getElementById('labelinput');
    if (inputFile.files.length === 0) {
        toastr.error('Debe agregar un archivo')
    } else {
        var Data = new FormData;
        Data.append("formFile", document.getElementById('labelinput').files[0]);
        Data.append("periodo", ID_MESES[$('#periodo-input').val()]);
        Data.append("anio", $('#year-input').val());
        Data.append("estatus", ESTATUS_TICKET.pendiente);
        Data.append("tipoDocumento", $("#TipoDocumento").val());
        Data.append("__RequestVerificationToken", $("input[name='__RequestVerificationToken']").val());
        $.ajax({
            url: urlSubirReporte,
            type: "POST",
            cache: false,
            data: Data,
            processData: false,  // tell jQuery not to process the data
            contentType: false,   // tell jQuery not to set contentType
            beforeSend: function () {
                showLoading();
            },
            complete: function () {
                hideLoading();
            },
            success: function (respuesta) {
                if (respuesta.success) {
                    Swal.fire({
                        icon: "success",
                        title: textoScript.archivoSubido,
                        showConfirmButton: false,
                        timer: 2500
                    })
                    otablaTickets.ajax.reload();
                    limpiarModalSubida();
                } else {
                    Swal.fire({
                        icon: "error",
                        title: textoScript.error,
                        text: textoScript.mErrorSubida,
                    });
                }
            },
            error: function (respuesta) {
                console.error('Ocurrio un error:', respuesta)
                //toastr.clear();
                //toastr.error(textoScript.errorSimple);
            }
        })
    }
}

function limpiarModalSubida() {
    $('.modal-header button').click();
    $('#inputname').val();
    $('#TipoDocumento').val("");
    $('#periodo-input').val();
    $('#year-input').val();
}

function corregirRedeco() {
    var ticket = {
        ticket: $('#ticket-clave').val(),
        idTicket: $('#idTicket').val()
    }
    var Data = new FormData;
    Data.append("formFile", document.getElementById('labelinput2').files[0]);
    Data.append("ticket", ticket);
    //Data.append("ticket", $('#ticket-clave').val());
    //Data.append("idTicket", $('#idTicket').val());
    Data.append("__RequestVerificationToken", $("input[name='__RequestVerificationToken']").val());
    ////console.log(Data[0])
    $.ajax({
        url: urlCorregirReporte,
        type: "POST",
        cache: false,
        data: Data,
        processData: false,  // tell jQuery not to process the data
        contentType: false,   // tell jQuery not to set contentType
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        success: function (respuesta) {
            if (respuesta.success) {
                Swal.fire({
                    icon: "success",
                    title: "Your work has been saved",
                    showConfirmButton: false,
                    timer: 2500
                })
            } else {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Something went wrong!",
                });
            }
        },
        error: function (respuesta) {
            console.error('Ocurrio un error:', respuesta)
            //toastr.clear();
            //toastr.error(textoScript.errorSimple);
        }
    })
}

function eliminarReune() {
    var ticket = {
        ticket: $('#ticket-clave').val(),
        idTicket: $('#idTicket').val()
    }
    var Data = new FormData;
    Data.append("formFile", document.getElementById('labelinput2').files[0]);
    Data.append("ticket", ticket);
    //Data.append("ticket", $('#ticket-clave').val());
    //Data.append("idTicket", $('#idTicket').val());
    Data.append("__RequestVerificationToken", $("input[name='__RequestVerificationToken']").val());
    ////console.log(Data[0])
    $.ajax({
        url: urlCorregirReporte,
        type: "POST",
        cache: false,
        data: Data,
        processData: false,  // tell jQuery not to process the data
        contentType: false,   // tell jQuery not to set contentType
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        success: function (respuesta) {
            if (respuesta.success) {
                Swal.fire({
                    icon: "success",
                    title: "Your work has been saved",
                    showConfirmButton: false,
                    timer: 2500
                })
            } else {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Something went wrong!",
                });
            }
        },
        error: function (respuesta) {
            console.error('Ocurrio un error:', respuesta)
            //toastr.clear();
            //toastr.error(textoScript.errorSimple);
        }
    })
}

function validarPeriodo() {
    var trimestres = [0,3,6,9] //Enero, Abril, Julio, Octubre
    var fechaActual = new Date();
    var mesActual = fechaActual.getMonth();
    var diaActual = fechaActual.getDate();
    if (!(diaActual >= 1 && diaActual <= 10) && !trimestres.includes(mesActual)) {
        $('#subir-btn').prop("disabled", true);
    }
}

function obtenerPeriodo() {
    var fechaActual = new Date();

    // Obtener el mes actual (0-11)
    var mesActual = fechaActual.getMonth();

    // Obtener el año actual (cuatro dígitos)
    var anioActual = fechaActual.getFullYear();

    // Calcular el trimestre anterior
    var trimestreAnterior = Math.floor((mesActual - 1) / 3);
    if (trimestreAnterior < 0) {
        trimestreAnterior = 3 - Math.abs(trimestreAnterior);
        anioActual -= 1; // Retroceder al año anterior si estamos en el primer trimestre
    }

    //console.log(trimestreAnterior)

    $('#periodo-input').val(NOMBRE_MESES[mesActual - 1]);
    $('#year-input').val(anioActual);

}

//$('#subir-btn').on('click', function () {
//    alert('d');
//    if ($('#subir-btn').prop("disabled")) {
        
//        toastr.error('Error');
//    } else obtenerPeriodo();
//})

function obtenerProximaFechaSubidaInformacion() {
    // Obtiene la fecha actual
    var fechaActual = new Date();

    // Obtiene el mes actual (0-11)
    var mesActual = fechaActual.getMonth();

    let trimestres = [0, 3, 6, 9];

    // Encuentra el trimestre correspondiente al mes actual
    var trimestreActual;
    for (var i = 0; i < trimestres.length; i++) {
        if (mesActual >= trimestres[i] && mesActual < trimestres[i + 1]) {
            trimestreActual = trimestres[i];
            break;
        }
    }

    trimestreActual = (trimestreActual / 3) + 13;

    var mensaje = "La próxima fecha para subir información será en los primeros 10 días del mes de " + ID_PERIODO_REDECO[mesActual + 1]
        + ". Y, deberá subir la información correspondiente al trimestre " + ID_PERIODO_REDECO[trimestreActual];

    return mensaje;
}

$('#consultar-btn').on('click', function () {
    // Ejemplo de uso
    var mensaje = obtenerProximaFechaSubidaInformacion();
    //console.log(mensaje);
    alert(mensaje)
})


function ValidarEstatus(estatus) {
    return estatus !== "False" ? textoScript.validado : textoScript.noValidado;
}

function validarEstatus(input) {
    // Convertir la cadena a minúsculas para hacer la comparación insensible a mayúsculas
    const lowerCaseInput = input.toLowerCase();

    // Verificar si la cadena contiene "no"
    return !lowerCaseInput.includes("no");
}

function validarErrores(tieneErrores) {
    //console.log('error', tieneErrores)
    //console.log(textoScript.sinErrores, textoScript.conErrores)
    return tieneErrores ? textoScript.conErrores : textoScript.sinErrores;
}
