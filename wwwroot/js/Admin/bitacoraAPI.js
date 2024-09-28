

var otablaLog = $("#tablaLog").DataTable({
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
            { "data": "metodo", "name": "metodo", "autowidth": true },
            { "data": "codigoError", "name": "codigoError", "autowidth": true },
            {
                "data": "peticion", render: function (data, type, row) {
                    return truncateString(data)
                }
            },
            {
                "data": "error", render: function (data, type, row) {
                    return truncateString(data)
                }
            },
            {
                "data": "fecha", render: function (data, type, row) {
                    return transformarFechaSubida(data)
                }
            },
            {
                "data": null, render: function (data, type, row) {
                    return `<button type="button" class="btn-mostrar btn btn-sm btn-block btn-info" style="font-size:small">Mostrar Detalles</button>`
                }
            },
            { "data": "peticion", "name": "peticion", "autowidth": true },
            { "data": "error", "name": "error", "autowidth": true },
        ],
    "columnDefs":
        [
            {
                "targets": [6, 7],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [0],
                "width": "10%"
            },
            {
                "targets": [1],
                "width": "5%"
            },
            {
                "targets": [2,3],
                "width": "30%"
            },
            {
                "targets": [4],
                "width": "15%",
                "type": "datetime"
            },
            {
                "targets": [5],
                "width": "10%",
                orderable: false
            },
        ]
});


$('#tablaLog tbody').on('click', 'button.btn-mostrar', function () {
    // Obtener la fila correspondiente al botón
    var fila = otablaLog.row($(this).closest('tr')).data();
    //console.log(fila)
    mostrarDetalles(fila);
});

$("#btn_buscar").on("click", function () {
    $.ajax({
        url: urlConsultarLog,
        method: "POST",
        cache: false,
        data: {
            idEmpresa: $("#empresa").val(),
            idServicio: $("#servicio").val(),
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
        },
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        success: function (respuesta) {
            console.log("Respuesta")
            console.log(respuesta)
            if (respuesta.success) {
                if (respuesta.code === 200) {
                    ConstruirTablaElementos(otablaLog, respuesta.data);
                }
                else {
                    alerta("error", "No se encontraron errores en la bitacora")
                }
            } else {
                alerta("error", "Ocurrió un error al buscar los errores en la bitacora", "Inténtelo nuevamente")
            }

        },
        error: function (respuesta) {
            alerta("error", "Ocurrió un error en su petición", "Inténtelo nuevamente")
        }
    })
})

function mostrarDetalles(datos) {
    //console.log("datos")
    //console.log(datos)
    $("#metodo").val(datos.metodo);
    $("#codigoError").val(datos.codigoError);
    $("#fecha").val(transformarFechaSubida(datos.fecha))
    $("#peticion").val(datos.peticion);
    $("#error").val(datos.error);
    $("#modalDetalles").modal("show");
}

$("#modalDetalles").on('hidden.bs.modal', function () {
    limpiarModalDetalles()
});


function limpiarModalDetalles() {
    $("#modalDetalles").modal("hide");
    $("#metodo").val("");
    $("#codigoError").val("");
    $("#fecha").val("")
    $("#peticion").val("");
    $("#error").val("");
}

function truncateString(str, maxLength = 100) {
    if (str.length > maxLength) {
        return str.substring(0, maxLength) + "...";
    } else {
        return str;
    }
}

