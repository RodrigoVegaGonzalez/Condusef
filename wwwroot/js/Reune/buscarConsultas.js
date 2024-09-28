import { VISTAS } from "../constantes.js";

$(document).ready(function () {
    $('.selectitems').select2();
});

$.fn.dataTable.ext.order['date-dd-mm-yyyy-hh-mm-ss'] = function (settings, col) {
    return this.api().column(col, { order: 'index' }).nodes().map(function (td, i) {
        var dateString = $(td).text().trim();
        var dateParts = dateString.split(" a las ");
        var date = dateParts[0].split('/');
        var time = dateParts[1].split(':');

        // Crear un objeto de fecha en el formato: new Date(año, mes-1, día, hora, minuto, segundo)
        return new Date(date[2], date[1] - 1, date[0], time[0], time[1], time[2]);
    });
};

var otablaConsultas = $("#tablaConsultas").DataTable({
    "scrollCOllapse": true,
    "autoWidth": false,
    processing: true,
    buttons: [
        'copy', 'excel', 'pdf'
    ],
    layout: {
        topStart: 'buttons'
    },
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
            { "data": "numConsultas", "name": "NumConsultas", "autowidth": true },
            { "data": "consultasFolio", "name": "ConsultasFolio", "autowidth": true },
            { "data": "consultasEstatusCon", "name": "ConsultasEstatusCon", "autowidth": true },
            { "data": "consultasFecRecepcion", "name": "ConsultasFecRecepcion", "autowidth": true },
            { "data": "mediosId", "name": "MediosId", "autowidth": true },
            { "data": "consultascatnivelatenId", "name": "ConsultascatnivelatenId", "autowidth": true },
            { "data": "producto", "name": "Producto", "autowidth": true },
            { "data": "causaId", "name": "CausaId", "autowidth": true },
            { "data": "consultasPori", "name": "ConsultasPori", "autowidth": true },
            { "data": "consultasFecAten", "name": "ConsultasFecAten", "autowidth": true },
            { "data": "estadosId", "name": "estadosId", "autowidth": true },
            { "data": "municipioText", "name": "ConsultasMpioId", "autowidth": true },
            { "data": "coloniaText", "name": "ConsultasColId", "autowidth": true },
            { "data": "consultasCP", "name": "ConsultasCP", "autowidth": true },
            { "data": "consultasLocId", "name": "ConsultasLocId", "autowidth": true },
            //{ "data": "fechaSubida", "name": "FechaSubida", "autowidth": true },
            {
                "data": "fechaSubida", render: function (data, type, row) {
                    return transformarFechaSubida(data)
                }
            },
            {
                "data": null, render: function (data, type, row) {
                    return `<button type="button" class="btn-modificar btn btn-sm btn-block btn-secondary " style="font-size:small">${textoScript.mostrar}</button>`
                }
            },
            //{
            //    "data": null, render: function (data, type, row) {
            //        return `<button type="button" class="btn-eliminar btn btn-sm btn-block btn-danger" style="font-size:small">${textoScript.eliminar}</button>`
            //    }
            //},
            {
                "data": null, render: function (data, type, row) {
                    return `<button type="button" class="btn-respuesta btn btn-sm btn-block btn-info" style="font-size:small">${textoScript.verRespuesta}</button>`
                }
            },
        ],
    "columnDefs":
        [
            {
                "targets": [0, 5, 7, 9, 10, 11, 12, 13, 14],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [1],
                "width": "17%"
            },
            {
                "targets": [2, 3, 4, 6],
                "width": "9%"
            },
            {
                "targets": [15],
                "type": "datetime",
                "width": "13%"
            },
            {
                "targets": [8],
                "width": "2%"
            },
            {
                "targets": [16,17],
                "width": "5%",
                orderable: false
            },
        ]
});

$('#tablaConsultas tbody').on('click', 'button.btn-modificar', function () {
    // Obtener la fila correspondiente al botón
    var fila = otablaConsultas.row($(this).closest('tr')).data();
    cargarModificarInfo(fila);
    $("#modalAgregar").modal("show");
});

$('#tablaConsultas tbody').on('click', 'button.btn-respuesta', function () {
    // Obtener la fila correspondiente al botón
    var fila = otablaConsultas.row($(this).closest('tr')).data();
    mostrarRespuestaJson(fila);
});

//$('#tablaConsultas tbody').on('click', '.btn-eliminar', function () {
//    // Obtener la fila que contiene el botón
//    var fila = $(this).closest('tr');

//    // Eliminar la fila del DataTable
//    otablaConsultas.row(fila).remove().draw();

//    alerta("error", "Registro eliminado", "", 1000)

//});

$("#btn_buscarQuejas").on("click", function () {
    let periodo = $("#periodo").val();
    let anio = $("#anio").val();
    if (periodo == 0) alerta("error", "Debe seleccionar un periodo consultar")
    else if (anio == 0) alerta("error", "Debe seleccionar un anio consultar")
    else {
        $.ajax({
            url: urlConsultarDatos,
            method: "POST",
            cache: false,
            data: {
                anio: anio,
                periodo: periodo,
                __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
            },
            beforeSend: function () {
                showLoading();
                otablaConsultas.clear().draw()
            },
            complete: function () {
                hideLoading();
            },
            success: function (respuesta) {
                ////console.log("Respuesta")
                ////console.log(respuesta)
                if (respuesta.success) {
                    if (respuesta.listado.length > 0) {
                        ConstruirTablaElementos(otablaConsultas, respuesta.listado);
                    } else {
                        alerta("error", "No se encontraron resultados", "")
                    }
                } else {
                    alerta("error", "Ocurrio un error al buscar sus consultas", "Intentelo nuevamente")
                }

            },
            error: function (respuesta) {
                alerta("error", textoScript.mErrorPeticion)
            }
        })
    }
})

$("#modalAgregar").on('hidden.bs.modal', function () {
    limpiarAgregarModal()
});

$("#modalRespuesta").on('hidden.bs.modal', function () {
    $("#folio_Ver").val("")
    $("#fechaRecepcion_Ver").val("")
    $("#fechaSubida_Ver").val("")
    $("#respuesta_ver").val("")
});

function limpiarAgregarModal() {
    $("#modalAgregar").modal("hide");
    reiniciarSelect("trimestre-select")
    $("#ConsultasFolio").val("");
    $("#NumConsultas").val("");
    $("#ConsultasFecAten").val("");
    $("#ConsultasFecRecepcion").val("");
    reiniciarSelect("medios-select")
    reiniciarSelect("niveles-select")
    reiniciarSelect("productos-select")
    $("#causaId").val();
    $('input[name=estatus-radio][value=1]').prop('checked', true);
    $('input[name=pori-radio][value=NO]').prop('checked', true);
    reiniciarSelect("estados-select");
    $("#causa-text").val();
    $("#municipio-text").val();
    $("#colonia-text").val();
    $("#cp-text").val();

    //$("#btn_agregar").show();
    //$("#btn_actualizar").hide();
};

function cargarModificarInfo(fila) {
    cambiarValorSelect("trimestre-select", $("#periodo").val());
    $("#NumConsultas").val(fila.numConsultas);
    $("#ConsultasFolio").val(fila.consultasFolio);
    $("#ConsultasFecRecepcion").val(transformarFecha_aaaaMMdd(fila.consultasFecRecepcion));
    $("#ConsultasFecAten").val(transformarFecha_aaaaMMdd(fila.consultasFecAten));
    cambiarValorSelect("medios-select", fila.mediosId);
    cambiarValorSelect("niveles-select", fila.consultascatnivelatenId);
    cambiarValorSelect("productos-select", fila.producto);

    $(`input[name=estatus-radio][value=${fila.consultasEstatusCon}]`).prop('checked', true);
    $(`input[name=pori-radio][value = ${fila.consultasPori}]`).prop('checked', true);

    cambiarValorSelect("estados-select", fila.estadosId);

    $("#causa-text").val(fila.causaId);
    $("#municipio-text").val(fila.municipioText);
    $("#cp-text").val(fila.consultasCP);
    $("#colonia-text").val(fila.coloniaText);
    //$("#btn_agregar").hide();
    //$("#btn_actualizar").show();
}


function mostrarRespuestaJson(datos) {
    $.ajax({
        url: urlConsultarRespuestaJson,
        method: "POST",
        cache: false,
        data: {
            folio: datos.consultasFolio,
            vista: VISTAS.consultas,
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
        },
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        success: function (respuesta) {
            ////console.log("Respuesta")
            ////console.log(respuesta)
            if (respuesta.success) {
                $("#folio_Ver").val(datos.consultasFolio)
                $("#fechaRecepcion_Ver").val(transformarFecha_aaaaMMdd(datos.consultasFecRecepcion))
                $("#fechaSubida_Ver").val(transformarFechaSubida(datos.fechaSubida))
                $("#respuesta_ver").val(respuesta.message)

                $("#modalRespuesta").modal("show");
            } else {
                alerta("error", "Error", "Ocurrio un error al consultar la respuesta de este folio. Intentelo nuevamente")
            }

        },
        error: function (respuesta) {
            alerta("error", textoScript.mErrorPeticion)
        }
    })

    

}