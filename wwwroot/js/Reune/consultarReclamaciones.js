
import { VISTAS } from "../constantes.js";

$(document).ready(function () {
    $('.selectitems').select2();
});

var otablaConsultas = $("#tablaReclamaciones").DataTable({
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
            { "data": "recNumero", "name": "RecNumero", "autowidth": true },
            { "data": "recFolioAtencion", "name": "RecFolioAtencion", "autowidth": true },
            { "data": "recEstadoConPend", "name": "RecEstadoConPend", "autowidth": true },
            { "data": "recFechaReclamacion", "name": "RecFechaReclamacion", "autowidth": true },
            { "data": "recMedioRecepcionCanal", "name": "RecMedioRecepcionCanal", "autowidth": true },
            { "data": "recNivelAtencion", "name": "RecNivelAtencion", "autowidth": true },
            { "data": "recProductoServicio", "name": "RecProductoServicio", "autowidth": true },
            { "data": "recCausaMotivo", "name": "RecCausaMotivo", "autowidth": true },
            { "data": "recPori", "name": "RecPori", "autowidth": true },
            { "data": "recFechaAtencion", "name": "RecFechaAtencion", "autowidth": true },
            { "data": "recFechaResolucion", "name": "RecFechaResolucion", "autowidth": true },
            { "data": "recFechaNotifiUsuario", "name": "RecFechaNotifiUsuario", "autowidth": true },
            { "data": "recEntidadFederativa", "name": "RecEntidadFederativa", "autowidth": true },
            { "data": "municipioText", "name": "RecMunicipioAlcaldia", "autowidth": true },
            { "data": "coloniaText", "name": "RecColonia", "autowidth": true },
            { "data": "recCodigoPostal", "name": "RecCodigoPostal", "autowidth": true },
            { "data": "recLocalidad", "name": "RecLocalidad", "autowidth": true },
            { "data": "recMonetario", "name": "RecMonetario", "autowidth": true },
            { "data": "recMontoReclamado", "name": "RecMontoReclamado", "autowidth": true },
            { "data": "recImporteAbonado", "name": "RecImporteAbonado", "autowidth": true },
            { "data": "recFechaAbonoImporte", "name": "RecFechaAbonoImporte", "autowidth": true },
            { "data": "recTipoPersona", "name": "RecTipoPersona", "autowidth": true },
            { "data": "recSexo", "name": "RecSexo", "autowidth": true },
            { "data": "recEdad", "name": "RecEdad", "autowidth": true },
            { "data": "recSentidoResolucion", "name": "RecSentidoResolucion", "autowidth": true },
            { "data": "recFolioCondusef", "name": "RecFolioCondusef", "autowidth": true },
            { "data": "recReversa", "name": "RecReversa", "autowidth": true },
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
                "targets": [0, 5, 7, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [1],
                "width": "17%"
            },
            {
                "targets": [3, 6],
                "width": "10%"
            },
            {
                "targets": [2, 4],
                "width": "8%"
            },
            {
                "targets": [27],
                "width": "13%",
                "type": "datetime"
            },
            {
                "targets": [8],
                "width": "2%"
            },
            {
                "targets": [28, 29],
                "width": "5%",
                orderable: false
            },
        ]
});

//Botones Tablas

$('#tablaReclamaciones tbody').on('click', 'button.btn-modificar', function () {
    // Obtener la fila correspondiente al botón
    var fila = otablaConsultas.row($(this).closest('tr')).data();
    cargarModificarInfo(fila);
    $("#modalAgregar").modal("show");
});

$('#tablaReclamaciones tbody').on('click', 'button.btn-respuesta', function () {
    // Obtener la fila correspondiente al botón
    var fila = otablaConsultas.row($(this).closest('tr')).data();
    mostrarRespuestaJson(fila);
});

//$('#tablaReclamaciones tbody').on('click', '.btn-eliminar', function () {
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

function limpiarAgregarModal() {
    $("#modalAgregar").modal("hide");
    $("#RecNumero").val("");
    $("#RecFolioAtencion").val("");
    $('input[name=estatus-radio][value=1]').prop('checked', true);
    $("#RecFechaReclamacion").val("");
    reiniciarSelect("medios-select");
    reiniciarSelect("niveles-select");
    reiniciarSelect("productos-select");
    $("#causa-text").val();
    reiniciarSelect("trimestre-select")
    $('input[name=pori-radio][value=NO]').prop('checked', true);
    $("#RecFechaAtencion").val("");
    $("#RecFechaResolucion").val("");
    $("#RecFechaNotifiUsuario").val("");
    reiniciarSelect("estados-select");
    //reiniciarSelect("municipios-select");
    //reiniciarSelect("colonias-select");
    //reiniciarSelect("cp-select");
    $('input[name=monetario-radio][value=NO]').prop('checked', true);
    $("#RecMontoReclamado").val("0");
    $("#RecImporteAbonado").val("0");
    $("#RecFechaAbonoImporte").val("0");
    $('input[name=persona-radio][value=1]').prop('checked', true); monetario - radio
    $('input[name=sexo-radio][value=H]').prop('checked', true);
    $("#RecEdad").val("0");
    reiniciarSelect("RecSentidoResolucion")
    $("#RecFolioCondusef").val("");
    $('input[name=reversa-radio][value=0]').prop('checked', true);

    $("#municipio-text").val();
    $("#colonias-text").val();
    $("#cp-text").val();

    //$("#btn_agregar").show();
    //$("#btn_actualizar").hide();
};

function cargarModificarInfo(fila) {
    cambiarValorSelect("trimestre-select", $("#periodo").val());
    $("#RecNumero").val(fila.recNumero);
    $("#RecFolioAtencion").val(fila.recFolioAtencion);
    $("#RecFechaReclamacion").val(transformarFecha_aaaaMMdd(fila.recFechaReclamacion));
    $("#RecFechaAtencion").val(transformarFecha_aaaaMMdd(fila.recFechaAtencion));
    $("#RecFechaResolucion").val(transformarFecha_aaaaMMdd(fila.recFechaResolucion));
    $("#RecFechaNotifiUsuario").val(transformarFecha_aaaaMMdd(fila.recFechaNotifiUsuario));
    cambiarValorSelect("medios-select", fila.recMedioRecepcionCanal);
    cambiarValorSelect("niveles-select", fila.recNivelAtencion);
    cambiarValorSelect("productos-select", fila.recProductoServicio);

    $(`input[name=estatus-radio][value=${fila.recEstadoConPend}]`).prop('checked', true);
    $(`input[namepersona-radio][value=${fila.recTipoPersona}]`).prop('checked', true);
    $(`input[name=pori-radio][value = ${fila.recPori}]`).prop('checked', true);
    $(`input[name=reversa-radio][value = ${fila.recReversa}]`).prop('checked', true);
    $(`input[name=monetario-radio][value = ${fila.recMonetario}]`).prop('checked', true);
    $(`input[name=sexo-radio][value = ${fila.recSexo}]`).prop('checked', true);

    cambiarValorSelect("estados-select", fila.recEntidadFederativa);

    $("#RecEdad").val(fila.recEdad);
    $("#RecFolioCondusef").val(fila.recFolioCondusef);
    cambiarValorSelect("RecSentidoResolucion", fila.recSentidoResolucion);

    $("#causa-text").val(fila.recCausaMotivo);
    $("#municipio-text").val(fila.municipioText);
    $("#cp-text").val(fila.recCodigoPostal);
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
            folio: datos.recFolioAtencion,
            vista: VISTAS.reclamaciones,
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
                $("#folio_Ver").val(datos.recFolioAtencion)
                $("#fechaRecepcion_Ver").val(transformarFecha_aaaaMMdd(datos.recFechaAtencion))
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





