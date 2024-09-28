
$(document).ready(function () {
    $('.selectitems').select2();
});

//Tablas
var otablaConsultas = $("#tablaQuejas").DataTable({
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
            { "data": "quejasNum", "name": "QuejasNum", "autowidth": true },
            { "data": "quejasNoMes", "name": "QuejasNoMes", "autowidth": true },
            { "data": "quejasFolio", "name": "QuejasFolio", "autowidth": true },
            { "data": "quejasFecRecepcion", "name": "QuejasFecRecepcion", "autowidth": true },
            { "data": "quejasMedio", "name": "QuejasMedio", "autowidth": true },
            { "data": "quejasNivelAT", "name": "QuejasNivelAT", "autowidth": true },
            { "data": "quejasProducto", "name": "QuejasProducto", "autowidth": true },
            { "data": "causaText", "name": "QuejasCausa", "autowidth": true },
            { "data": "quejasEstatus", "name": "QuejasEstatus", "autowidth": true },
            { "data": "quejasTipoPersona", "name": "QuejasTipoPersona", "autowidth": true },
            { "data": "quejasPORI", "name": "QuejasPORI", "autowidth": true },
            { "data": "quejasEstados", "name": "QuejasEstados", "autowidth": true },
            { "data": "municipioText", "name": "QuejasMunId", "autowidth": true },
            { "data": "quejasLocId", "name": "QuejasLocId", "autowidth": true },
            { "data": "coloniaText", "name": "QuejasColId", "autowidth": true },
            { "data": "quejasCP", "name": "QuejasCP", "autowidth": true },
            { "data": "quejasSexo", "name": "QuejasSexo", "autowidth": true },
            { "data": "quejasEdad", "name": "QuejasEdad", "autowidth": true },
            { "data": "quejasFecResolucion", "name": "QuejasFecResolucion", "autowidth": true },
            { "data": "quejasFecNotificacion", "name": "QuejasFecNotificacion", "autowidth": true },
            { "data": "quejasRespuesta", "name": "QuejasRespuesta", "autowidth": true },
            { "data": "quejasPenalizacion", "name": "QuejasPenalizacion", "autowidth": true },
            { "data": "quejasNumPenal", "name": "QuejasNumPenal", "autowidth": true },
            //{ "data": "fechaSubida", "name": "FechaSubida", "autowidth": true },
            {
                "data": "fechaSubida", render: function (data, type, row) {
                    return transformarFechaSubida(data)
                }
            },
            {
                "data": null, render: function (data, type, row) {
                    return `<button type="button" class="btn-mostrar btn btn-sm btn-block btn-secondary text-white text-center waves-effect waves-light" style="font-size:small">${textoScript.mostrar}</button>`
                }
            },
            {
                "data": null, render: function (data, type, row) {
                    return `<button type="button" class="btn-eliminar btn btn-sm btn-block btn-danger text-white text-center waves-effect waves-light" style="font-size:small">${textoScript.eliminar}</button>`
                }
            },
            {
                "data": null, render: function (data, type, row) {
                    return `<button type="button" class="btn-respuesta btn btn-sm btn-block btn-info" style="font-size:small">${textoScript.verRespuesta}</button>`
                }
            },
        ],
    "columnDefs":
        [
            {
                "targets": [0, 1, 5, 7, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [2],
                "width": "15%"
            },
            {
                "targets": [2, 3, 6],
                "width": "10%"
            },
            {
                "targets": [4, 8, 9],
                "width": "5%"
            },
            {
                "targets": [23],
                "width": "15%",
                "type": "datetime"
            },
            {
                "targets": [24, 25,  26],
                "width": "5%",
                orderable: false
            },
        ]
});


//Botones Tablas

$('#tablaQuejas tbody').on('click', 'button.btn-mostrar', function () {
    // Obtener la fila correspondiente al botón
    var fila = otablaConsultas.row($(this).closest('tr')).data();
    cargarModificarQueja(fila);
    $("#modalAgregarQueja").modal("show")
});

$('#tablaQuejas tbody').on('click', '.btn-eliminar', function () {
    Swal.fire({
        title: textoScript.mEliminarQueja,
        showDenyButton: true,
        icon: "warning",
        confirmButtonText: textoScript.si,
        denyButtonText: textoScript.no
    }).then((result) => {
        // Verificar si el usuario hizo clic en el botón "Deny"
        if (result.isConfirmed) {
            // Obtener la fila correspondiente al botón
            var fila = otablaConsultas.row($(this).closest('tr')).data();
            $.ajax({
                url: urlEliminarQuejas,
                method: "POST",
                cache: false,
                data: {
                    folio: fila.quejasFolio,
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
                        if (respuesta.code === 200) alertaConRecarga("success", textoScript.mExitoEliminarQueja, "");
                        else if (respuesta.code === 400) alerta("error", textoScript.mErrorQuejaNoEliminada, "");
                    } else {
                        alerta("error", textoScript.mErrorEliminarQueja)
                    }
                },
                error: function (respuesta) {
                    alerta("error", textoScript.errorGenerico, "")
                }
            })
        }
    });
});

$('#tablaQuejas tbody').on('click', 'button.btn-respuesta', function () {
    // Obtener la fila correspondiente al botón
    var fila = otablaConsultas.row($(this).closest('tr')).data();
    mostrarRespuestaJson(fila);
});

$("#btn_buscarQuejas").on("click", function () {
    let periodo = $("#periodo").val();
    let anio = $("#anio").val();
    if (periodo == 0) alerta("error", "Debe seleccionar un periodo consultar")
    else if (anio == 0) alerta("error", "Debe seleccionar un anio consultar")
    else {
        $.ajax({
            url: urlConsultarQuejas,
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
                    if (respuesta.data.length > 0) {
                        //console.log(respuesta.data)
                        ConstruirTablaElementos(otablaConsultas, respuesta.data);
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


$("#modalAgregarQueja").on('hidden.bs.modal', function () {
    limpiarAgregarQueja()
});


function limpiarAgregarQueja() {
    $("#modalAgregarQueja").modal("hide");
    reiniciarSelect("QuejasNoMes")
    $("#QuejasFolio").val("");
    $("#QuejasFecRecepcion").val("");
    reiniciarSelect("QuejasMedio");
    reiniciarSelect("QuejasNivelAT");
    reiniciarSelect("QuejasProducto");
    $('input[name=QuejasEstatus][value=1]').prop('checked', true);
    $('input[name=QuejasTipoPersona][value=1]').prop('checked', true);
    $('input[name=QuejasPORI][value=NO]').prop('checked', true);
    reiniciarSelect("QuejasEstados");
    $('input[name=QuejasSexo][value=H]').prop('checked', true);
    $("#QuejasEdad").val("0");
    $("#QuejasFecResolucion").val("");
    $("#QuejasFecNotificacion").val("");
    reiniciarSelect("QuejasRespuesta")
    reiniciarSelect("QuejasPenalizacion")
    $("#QuejasNumPenal").val("0");
    //$("#btn_agregarQueja").show();
    //$("#btn_actualizarQueja").hide();

    $("#causa-text").val();
    $("#municipio-text").val();
    $("#colonia-text").val();
    $("#cp-text").val();
};

function cargarModificarQueja(fila) {
    //console.log(fila)
    cambiarValorSelect("QuejasNoMes", $("#periodo").val());
    $("#QuejasFolio").val(fila.quejasFolio);
    $("#QuejasNum").val(fila.quejasNum);
    $("#QuejasFecRecepcion").val(transformarFecha_aaaaMMdd(fila.quejasFecRecepcion));
    cambiarValorSelect("QuejasMedio", fila.quejasMedio);
    cambiarValorSelect("QuejasNivelAT", fila.quejasNivelAT);
    cambiarValorSelect("QuejasProducto", fila.quejasProducto);

    $(`input[name=QuejasEstatus][value=${fila.quejasEstatus}]`).prop('checked', true);
    $(`input[name=QuejasTipoPersona][value=${fila.quejasTipoPersona}]`).prop('checked', true);
    $(`input[name=QuejasPORI][value = ${fila.quejasPORI}]`).prop('checked', true);
    cambiarValorSelect("QuejasEstados", fila.quejasEstados);

    $(`input[name=QuejasSexo][value=${fila.quejasSexo}]`).prop('checked', true);
    $("#QuejasEdad").val(fila.quejasEdad);
    $("#QuejasFecResolucion").val(transformarFecha_aaaaMMdd(fila.quejasFecResolucion));
    $("#QuejasFecNotificacion").val(transformarFecha_aaaaMMdd(fila.quejasFecNotificacion));
    cambiarValorSelect("QuejasRespuesta", fila.quejasRespuesta);
    cambiarValorSelect("QuejasPenalizacion", fila.quejasPenalizacion);
    $("#QuejasNumPenal").val(fila.quejasNumPenal);
    //$("#btn_agregarQueja").hide();
    //$("#btn_actualizarQueja").show();

    $("#causa-text").val(fila.causaText);
    $("#municipio-text").val(fila.municipioText);
    $("#cp-text").val(fila.quejasCP);
    $("#colonia-text").val(fila.coloniaText);
}

function mostrarRespuestaJson(datos) {
    $.ajax({
        url: urlConsultarRespuestaJson,
        method: "POST",
        cache: false,
        data: {
            folio: datos.quejasFolio,
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
                $("#folio_Ver").val(datos.quejasFolio)
                $("#fechaRecepcion_Ver").val(transformarFecha_aaaaMMdd(datos.quejasFecRecepcion))
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

















