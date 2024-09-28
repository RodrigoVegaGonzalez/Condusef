import { VISTAS } from "../constantes.js";

$(document).ready(function () {
    $('.selectitems').select2();
});

var otablaConsultas = $("#tablaAclaraciones").DataTable({
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
            { "data": "aclaracionNumero", "name": "AclaracionNumero", "autowidth": true },
            { "data": "aclaracionFolioAtencion", "name": "AclaracionFolioAtencion", "autowidth": true },
            { "data": "aclaracionEstadoConPend", "name": "AclaracionEstadoConPend", "autowidth": true },
            { "data": "aclaracionFechaAclaracion", "name": "AclaracionFechaAclaracion", "autowidth": true },
            { "data": "aclaracionMedioRecepcionCanal", "name": "AclaracionMedioRecepcionCanal", "autowidth": true },
            { "data": "aclaracionNivelAtencion", "name": "AclaracionNivelAtencion", "autowidth": true },
            { "data": "aclaracionProductoServicio", "name": "AclaracionProductoServicio", "autowidth": true },
            { "data": "aclaracionCausaMotivo", "name": "AclaracionCausaMotivo", "autowidth": true },
            { "data": "aclaracionPori", "name": "AclaracionPori", "autowidth": true },
            { "data": "aclaracionFechaAtencion", "name": "AclaracionFechaAtencion", "autowidth": true },
            { "data": "aclaracionFechaResolucion", "name": "AclaracionFechaResolucion", "autowidth": true },
            { "data": "aclaracionFechaNotifiUsuario", "name": "AclaracionFechaNotifiUsuario", "autowidth": true },
            { "data": "aclaracionEntidadFederativa", "name": "AclaracionEntidadFederativa", "autowidth": true },
            { "data": "aclaracionMunicipioAlcaldia", "name": "AclaracionMunicipioAlcaldia", "autowidth": true },
            { "data": "aclaracionColonia", "name": "AclaracionColonia", "autowidth": true },
            { "data": "aclaracionCodigoPostal", "name": "AclaracionCodigoPostal", "autowidth": true },
            { "data": "aclaracionLocalidad", "name": "AclaracionLocalidad", "autowidth": true },
            { "data": "aclaracionMonetario", "name": "AclaracionMonetario", "autowidth": true },
            { "data": "aclaracionMontoReclamado", "name": "AclaracionMontoReclamado", "autowidth": true },
            { "data": "aclaracionTipoPersona", "name": "AclaracionTipoPersona", "autowidth": true },
            { "data": "aclaracionSexo", "name": "AclaracionSexo", "autowidth": true },
            { "data": "aclaracionEdad", "name": "AclaracionEdad", "autowidth": true },
            { "data": "aclaracionFolioCondusef", "name": "AclaracionFolioCondusef", "autowidth": true },
            { "data": "aclaracionReversa", "name": "AclaracionReversa", "autowidth": true },
            { "data": "aclaracionOperacionExtranjero", "name": "AclaracionOperacionExtranjero", "autowidth": true },
            //{ "data": "fechaSubida", "name": "FechaSubida", "autowidth": true },
            {
                "data": "fechaSubida", render: function (data, type, row) {
                    return transformarFechaSubida(data)
                }
            },
            {
                "data": null, render: function (data, type, row) {
                    return `<button type="button" class="btn-modificar btn btn-sm btn-block btn-secondary text-white text-center waves-effect waves-light" style="font-size:small">${textoScript.mostrar}</button>`
                }
            },
            //{
            //    "data": null, render: function (data, type, row) {
            //        return `<button type="button" class="btn-eliminar btn btn-sm btn-block btn-danger text-white text-center waves-effect waves-light" style="font-size:small">${textoScript.eliminar}</button>`
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
                "targets": [0, 5, 7, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [1],
                "width": "17%"
            },
            {
                "targets": [2, 3, 6],
                "width": "10%"
            },
            {
                "targets": [25],
                "width": "13%",
                "type": "datetime"
            },
            {
                "targets": [8, 4],
                "width": "5%"
            },
            {
                "targets": [26, 27],
                "width": "5%",
                orderable: false
            },
        ]
});

//Botones Tablas

$('#tablaAclaraciones tbody').on('click', 'button.btn-modificar', function () {
    // Obtener la fila correspondiente al botón
    var fila = otablaConsultas.row($(this).closest('tr')).data();
    cargarModificarInfo(fila);
    $("#modalAgregar").modal("show");
});

$('#tablaAclaraciones tbody').on('click', 'button.btn-respuesta', function () {
    // Obtener la fila correspondiente al botón
    var fila = otablaConsultas.row($(this).closest('tr')).data();
    mostrarRespuestaJson(fila);
});

//$('#tablaAclaraciones tbody').on('click', '.btn-eliminar', function () {
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
                //console.log("Respuesta")
                //console.log(respuesta)
                if (respuesta.success) {
                    if (respuesta.listado.length > 0) {
                        //console.log(respuesta.listado)
                        ConstruirTablaElementos(otablaConsultas, respuesta.listado);
                    } else {
                        alerta("error", "No se encontraron resultados", "")
                    }
                } else {
                    alerta("error", textoScript.mErrorEnvioQuejas2)
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

function limpiarAgregarModal() {
    $("#modalAgregar").modal("hide");
    $("#AclaracionNumero").val("");
    $("#AclaracionFolioAtencion").val("");
    $('input[name=estatus-radio][value=1]').prop('checked', true);
    $("#AclaracionFechaAclaracion").val("");
    reiniciarSelect("medios-select");
    reiniciarSelect("niveles-select");
    reiniciarSelect("productos-select");
    reiniciarSelect("trimestre-select")
    $('input[name=pori-radio][value=NO]').prop('checked', true);
    $("#AclaracionFechaAtencion").val("");
    $("#AclaracionFechaResolucion").val("");
    $("#AclaracionFechaNotifiUsuario").val("");
    reiniciarSelect("estados-select");
    $('input[name=monetario-radio][value=NO]').prop('checked', true);
    $("#AclaracionMontoReclamado").val("0");
    $('input[name=persona-radio][value=1]').prop('checked', true); monetario - radio
    $('input[name=sexo-radio][value=H]').prop('checked', true);
    $("#AclaracionEdad").val("0");
    reiniciarSelect("RecSentidoResolucion")
    $("#AclaracionFolioCondusef").val("");
    $('input[name=reversa-radio][value=0]').prop('checked', true);
    $('input[name=operacion-radio][value=NO]').prop('checked', true);

    $("#causa-text").val();
    $("#municipio-text").val();
    $("#colonias-text").val();
    $("#cp-text").val();

    //$("#btn_agregar").show();
    //$("#btn_actualizar").hide();
};

function cargarModificarInfo(fila) {
    cambiarValorSelect("trimestre-select", $("#periodo").val());
    $("#AclaracionDenominacion").val(fila.aclaracionDenominacion);
    $("#AclaracionSector").val(fila.aclaracionSector);
    $("#AclaracionNumero").val(fila.aclaracionNumero);
    $("#AclaracionFolioAtencion").val(fila.aclaracionFolioAtencion);
    $("#AclaracionFechaAclaracion").val(transformarFecha_aaaaMMdd(fila.aclaracionFechaAclaracion));
    $("#AclaracionFechaAtencion").val(transformarFecha_aaaaMMdd(fila.aclaracionFechaAtencion));
    $("#AclaracionFechaResolucion").val(transformarFecha_aaaaMMdd(fila.aclaracionFechaResolucion));
    $("#AclaracionFechaNotifiUsuario").val(transformarFecha_aaaaMMdd(fila.aclaracionFechaNotifiUsuario));
    cambiarValorSelect("medios-select", fila.aclaracionMedioRecepcionCanal);
    cambiarValorSelect("niveles-select", fila.aclaracionNivelAtencion);
    cambiarValorSelect("productos-select", fila.aclaracionProductoServicio);

    $(`input[name=estatus-radio][value=${fila.aclaracionEstadoConPend}]`).prop('checked', true);
    $(`input[name=persona-radio][value=${fila.aclaracionTipoPersona}]`).prop('checked', true);
    $(`input[name=pori-radio][value = ${fila.aclaracionPori}]`).prop('checked', true);
    $(`input[name=reversa-radio][value = ${fila.aclaracionReversa}]`).prop('checked', true);
    $(`input[name=monetario-radio][value = ${fila.aclaracionMonetario}]`).prop('checked', true);
    $(`input[name=sexo-radio][value = ${fila.aclaracionSexo}]`).prop('checked', true);

    cambiarValorSelect("estados-select", fila.aclaracionEntidadFederativa);

    $("#AclaracionEdad").val(fila.aclaracionEdad);
    $("#AclaracionFolioCondusef").val(fila.aclaracionFolioCondusef);
    $(`input[name=operacion-radio][value = ${fila.aclaracionOperacionExtranjero}]`).prop('checked', true);

    $("#causa-text").val(fila.aclaracionCausaMotivo);
    $("#municipio-text").val(fila.municipioText);
    $("#cp-text").val(fila.aclaracionCodigoPostal);
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
            folio: datos.aclaracionFolioAtencion,
            vista: VISTAS.aclaraciones,
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
                $("#folio_Ver").val(datos.aclaracionFolioAtencion)
                $("#fechaRecepcion_Ver").val(transformarFecha_aaaaMMdd(datos.aclaracionFechaAclaracion))
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


