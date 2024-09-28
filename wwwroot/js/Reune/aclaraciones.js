import { ACLARACIONES_FICTICIAS, VISTAS } from "../constantes.js";

//Variables
var tipoLocalidad = [];

var tabla = null;

//Ready
//$(document).ready(function () {
//    mostrarAnioActual()
//})

// Llamar a la funci�n para mostrar el a�o actual cuando se cargue la p�gina
mostrarAnioActual("anioActual");


//Tablas

// tablaReclamaciones

var otablaConsultas = $("#tablaAclaraciones").DataTable({
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
            { "data": "AclaracionNumero", "name": "AclaracionNumero", "autowidth": true },
            { "data": "AclaracionFolioAtencion", "name": "AclaracionFolioAtencion", "autowidth": true },
            { "data": "AclaracionEstadoConPend", "name": "AclaracionEstadoConPend", "autowidth": true },
            { "data": "AclaracionFechaAclaracion", "name": "AclaracionFechaAclaracion", "autowidth": true },
            { "data": "AclaracionMedioRecepcionCanal", "name": "AclaracionMedioRecepcionCanal", "autowidth": true },
            { "data": "AclaracionNivelAtencion", "name": "AclaracionNivelAtencion", "autowidth": true },
            { "data": "AclaracionProductoServicio", "name": "AclaracionProductoServicio", "autowidth": true },
            { "data": "AclaracionCausaMotivo", "name": "AclaracionCausaMotivo", "autowidth": true },
            { "data": "AclaracionDenominacion", "name": "AclaracionDenominacion", "autowidth": true },
            { "data": "AclaracionSector", "name": "AclaracionSector", "autowidth": true },
            { "data": "AclaracionTrimestre", "name": "AclaracionTrimestre", "autowidth": true },
            { "data": "AclaracionPori", "name": "AclaracionPori", "autowidth": true },
            { "data": "AclaracionFechaAtencion", "name": "AclaracionFechaAtencion", "autowidth": true },
            { "data": "AclaracionFechaResolucion", "name": "AclaracionFechaResolucion", "autowidth": true },
            { "data": "AclaracionFechaNotifiUsuario", "name": "AclaracionFechaNotifiUsuario", "autowidth": true },
            { "data": "AclaracionEntidadFederativa", "name": "AclaracionEntidadFederativa", "autowidth": true },
            { "data": "AclaracionMunicipioAlcaldia", "name": "AclaracionMunicipioAlcaldia", "autowidth": true },
            { "data": "AclaracionColonia", "name": "AclaracionColonia", "autowidth": true },
            { "data": "AclaracionCodigoPostal", "name": "AclaracionCodigoPostal", "autowidth": true },
            { "data": "AclaracionLocalidad", "name": "AclaracionLocalidad", "autowidth": true },
            { "data": "AclaracionMonetario", "name": "AclaracionMonetario", "autowidth": true },
            { "data": "AclaracionMontoReclamado", "name": "AclaracionMontoReclamado", "autowidth": true },
            { "data": "AclaracionTipoPersona", "name": "AclaracionTipoPersona", "autowidth": true },
            { "data": "AclaracionSexo", "name": "AclaracionSexo", "autowidth": true },
            { "data": "AclaracionEdad", "name": "AclaracionEdad", "autowidth": true },
            { "data": "AclaracionFolioCondusef", "name": "AclaracionFolioCondusef", "autowidth": true },
            { "data": "AclaracionReversa", "name": "AclaracionReversa", "autowidth": true },
            { "data": "AclaracionOperacionExtranjero", "name": "AclaracionOperacionExtranjero", "autowidth": true },
            {
                "data": null, render: function (data, type, row) {
                    return `<button type="button" class="btn-modificar btn btn-sm btn-block btn-secondary text-white text-center waves-effect waves-light" style="font-size:small">${textoScript.modificar}</button>`
                }
            },
            {
                "data": null, render: function (data, type, row) {
                    return `<button type="button" class="btn-eliminar btn btn-sm btn-block btn-danger text-white text-center waves-effect waves-light" style="font-size:small">${textoScript.eliminar}</button>`
                }
            },
        ],
    "columnDefs":
        [
            {
                "targets": [8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [1],
                "width": "15%"
            },
            {
                "targets": [0, 2, 3, 4, 5, 6, 7],
                "width": "10%"
            },
            {
                "targets": [28, 29],
                "width": "5%"
            },
        ]
});

var otablaListadoErrores = $("#tablaListadoErrores").DataTable({
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
            { "data": "aclaracionNumero", "name": "AclaracionNumero", "autowidth": true },
            { "data": "aclaracionFolioAtencion", "name": "AclaracionFolioAtencion", "autowidth": true },
            { "data": "aclaracionEstadoConPend", "name": "AclaracionEstadoConPend", "autowidth": true },
            { "data": "aclaracionFechaAclaracion", "name": "AclaracionFechaAclaracion", "autowidth": true },
            { "data": "aclaracionMedioRecepcionCanal", "name": "AclaracionMedioRecepcionCanal", "autowidth": true },
            { "data": "aclaracionNivelAtencion", "name": "AclaracionNivelAtencion", "autowidth": true },
            { "data": "aclaracionProductoServicio", "name": "AclaracionProductoServicio", "autowidth": true },
            { "data": "aclaracionCausaMotivo", "name": "AclaracionCausaMotivo", "autowidth": true },
            { "data": "aclaracionDenominacion", "name": "aclaracionDenominacion", "autowidth": true },
            { "data": "aclaracionSector", "name": "AclaracionSector", "autowidth": true },
            { "data": "aclaracionTrimestre", "name": "AclaracionTrimestre", "autowidth": true },
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
            {
                "data": null, render: function (data, type, row) {
                    return `<button type="button" class="btn-errores btn btn-sm btn-block btn-secondary text-white text-center waves-effect waves-light" style="font-size:small">${textoScript.verErrores}</button>`
                }
            },
            { "data": "errors", "name": "errors", "autowidth": true },
        ],
    "columnDefs":
        [
            {
                "targets": [8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 29],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [1],
                "width": "15%"
            },
            {
                "targets": [0, 2, 3, 4, 5, 6, 7],
                "width": "10%"
            },
            {
                "targets": [28],
                "width": "5%"
            },
        ]
});

var otablaErroress = $("#tablaErrores").DataTable({
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
            { "data": "descripcion", "name": "descripcion", "autowidth": true },
        ],
    "columnDefs":
        [
            {
                "targets": [0],
                "width": "100%"
            },
        ]
});

//Botones Tablas

$('#tablaAclaraciones tbody').on('click', 'button.btn-modificar', function () {
    // Obtener la fila correspondiente al bot�n
    var fila = otablaConsultas.row($(this).closest('tr')).data();
    let tr = $(this).closest('tr');
    let row = otablaConsultas.row(tr).index();

    $("#indice-fila").val(row)
    //tabla = otablaConsultas.row($(this).closest('tr'));
    cargarModificarInfo(fila);
    $("#modalAgregar").modal("show");
});

$('#tablaAclaraciones tbody').on('click', '.btn-eliminar', function () {
    Swal.fire({
        title: textoScript.mEliminarRegistro,
        showDenyButton: true,
        icon: "warning",
        confirmButtonText: textoScript.si,
        denyButtonText: textoScript.no
    }).then((result) => {
        // Verificar si el usuario hizo clic en el bot�n "Deny"
        if (result.isConfirmed) {
            // Obtener la fila que contiene el bot�n
            var fila = $(this).closest('tr');

            // Eliminar la fila del DataTable
            otablaConsultas.row(fila).remove().draw();
            alerta("error", textoScript.mRegistroEliminado)
        }
    });
});

$('#tablaListadoErrores tbody').on('click', 'button.btn-errores', function () {
    // Obtener la fila correspondiente al bot�n
    var fila = otablaListadoErrores.row($(this).closest('tr')).data();

    cargarErrores(fila.aclaracionTrimestre, fila.aclaracionFolioAtencion, fila.aclaracionFechaAclaracion, fila.errors)
});


//Botones
$("#btn_agregarDatosFicticios").on('click', function () {
    var tabla = $('#tablaAclaraciones').DataTable();
    $.each(ACLARACIONES_FICTICIAS, function (index, consulta) {
        consulta.AclaracionDenominacion = $("#institucion-clave").val();
        consulta.AclaracionSector = $("#sector").val();
        consulta.AclaracionFolioAtencion = generarFolio();
        tabla.row.add(consulta).draw();
    });
})


//Modal

$("#modalAgregar").on('shown.bs.modal', function () {
    //obtenerPeriodoReportar()
});

$("#modalAgregar").on('hidden.bs.modal', function () {
    limpiarAgregarModal()
});

$("#modalListadoErrores").on('hidden.bs.modal', function () {
    otablaListadoErrores.clear().draw();
});

$("#modalErrores").on('hidden.bs.modal', function () {
    otablaErroress.clear().draw();
    reiniciarSelect("trimestre_Ver");
    $("#folio_Ver").val('');
    $("#fechaRecepcion_Ver").val('');
});

//Enviar datos al controlador
$("#btn_subirDatos").on("click", function () {
    var tabla = $('#tablaAclaraciones').DataTable();

    // Obtener los datos de la tabla
    var datos = tabla.rows().data();

    // Array para almacenar los datos como objetos
    var objetoDatos = [];

    // Iterar sobre los datos y construir el array de objetos
    datos.each(function (index, fila) {
        if (index.AclaracionNivelAtencion == "null") index.AclaracionNivelAtencion = null;
        if (index.AclaracionMontoReclamado == "null") index.AclaracionMontoReclamado = null;
        if (index.AclaracionFolioCondusef == "null") index.AclaracionFolioCondusef = null;
        if (index.AclaracionFechaResolucion == "null") index.AclaracionFechaResolucion = null;
        if (index.AclaracionFechaNotifiUsuario == "null") index.AclaracionFechaNotifiUsuario = null;
        if (index.AclaracionFechaAtencion == "null") index.AclaracionFechaAtencion = null;
        if (index.AclaracionReversa == "null") index.AclaracionReversa = null;
        if (index.AclaracionCodigoPostal == "null") index.AclaracionCodigoPostal = null;
        if (index.AclaracionColonia == "null") index.AclaracionColonia = null;
        if (index.AclaracionLocalidad == "null") index.AclaracionLocalidad = null;

        if (index.AclaracionSexo == "null") index.AclaracionSexo = null;
        if (index.AclaracionEdad == "null") index.AclaracionEdad = null;

        objetoDatos.push(index);
    });

    // Mostrar el array de objetos en la consola
    ////console.log(objetoDatos);
    if (objetoDatos.length == 0) {
        alerta("error", "Debe agregar una o m�s aclaraciones")
    }
    else {
        $.ajax({
            url: urlEnviarDatos,
            method: "POST",
            cache: false,
            data: {
                anio: getAnioActual(),
                aclaraciones: objetoDatos,
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
                        alertaConRecarga("success", textoScript.mExitoEnvioQuejas, "")
                    } else if (respuesta.code >= 500) {
                        alerta("error", "El servicio de Reune no funciona. Intentelo mas tarde")
                    } else {
                        alerta("error", textoScript.mErrorEnvioQuejas, "")
                        otablaConsultas.clear().draw()
                        ConstruirTablaElementos(otablaListadoErrores, respuesta.errores);

                        //$("#btn_ListadoErrores").click();
                        $("#modalListadoErrores").modal("show");
                    }
                } else {
                    alerta("error", "El servicio de Reune no funciona. Intentelo mas tarde")
                }

            },
            error: function (respuesta) {
                console.log("Respuesta")
                console.log(respuesta)
                alerta("error", textoScript.mErrorPeticion)
            }
        })
    }
})

$("#btn_corregirQuejas").on("click", function () {
    let tabla = $('#tablaListadoErrores').DataTable();
    let tabla2 = $('#tablaAclaraciones').DataTable();

    // Obtener los datos de la tabla
    var datos = tabla.rows().data();

    // Array para almacenar los datos como objetos
    var arrayObjetos = [];

    // Iterar sobre los datos y construir el array de objetos
    datos.each(function (index, fila) {
        arrayObjetos.push(index);
    });

    // Mostrar el array de objetos en la consola
    ////console.log(arrayObjetos);

    tabla2.clear().draw();

    $.each(arrayObjetos, function (index, queja) {
        var obj = capitalizeObjectKeys(queja);
        //console.log(obj)
        obj.AclaracionCodigoPostal = convertirCP(obj.AclaracionCodigoPostal);
        tabla2.row.add(obj).draw();
    });

    $("#modalListadoErrores").modal("hide");
})

function construyeObjetoInfo() {
    return new Promise(function (resolve, reject) {
        let objetoSeleccionado = tipoLocalidad.find(function (objeto) {
            return objeto.id === $("#colonias-select").val();
        });

        let info = {
            "AclaracionDenominacion": $("#institucion-clave").val(),
            "AclaracionSector": $("#sector").val(),
            "AclaracionNumero": $("#AclaracionNumero").val(),
            "AclaracionTrimestre": $("#trimestre-select").val(),
            "AclaracionFolioAtencion": $("#AclaracionFolioAtencion").val().trim(),
            "AclaracionEstadoConPend": $('input[name=estatus-radio]:checked').val(),
            "AclaracionFechaAtencion": $("#AclaracionFechaAtencion").val(),
            "AclaracionFechaAclaracion": $("#AclaracionFechaAclaracion").val(),
            "AclaracionMedioRecepcionCanal": $("#medios-select").val(),
            "AclaracionNivelAtencion": $("#niveles-select").val(),
            "AclaracionProductoServicio": $("#productos-select").val(),
            "AclaracionCausaMotivo": $("#causas-select").val(),
            "AclaracionFechaResolucion": $("#AclaracionFechaResolucion").val(),
            "AclaracionFechaNotifiUsuario": $("#AclaracionFechaNotifiUsuario").val(),
            "AclaracionEntidadFederativa": $("#estados-select").val(),
            "AclaracionMunicipioAlcaldia": $("#municipios-select").val(),
            "AclaracionLocalidad": validaStringNumerico(objetoSeleccionado?.valor),
            "AclaracionColonia": $("#colonias-select").val(),
            "AclaracionCodigoPostal": $("#cp-select").val(),
            "AclaracionMonetario": $('input[name=monetario-radio]:checked').val(),
            "AclaracionMontoReclamado": $("#AclaracionMontoReclamado").val(),
            "AclaracionPori": $('input[name=pori-radio]:checked').val(),
            "AclaracionTipoPersona": $('input[name=persona-radio]:checked').val(),
            "AclaracionSexo": $('input[name=sexo-radio]:checked').val(),
            "AclaracionEdad": $("#AclaracionEdad").val(),
            "AclaracionFolioCondusef": $("#AclaracionFolioCondusef").val().trim(),
            "AclaracionReversa": $('input[name=reversa-radio]:checked').val(),
            "AclaracionOperacionExtranjero": $('input[name=operacion-radio]:checked').val(),
        };

        // Resolve la promesa con el objeto info
        resolve(info);
    });
}


//Agregar dato a tabla
$("#btn_agregar").on('click', function () {
    construyeObjetoInfo().then(function (info) {
        ////console.log('info', info)
        let validado = validarInformacion(info);
        if (validado.hayErrores) {
            cargarErrores(info.AclaracionTrimestre, info.AclaracionFolioAtencion, info.AclaracionFechaAclaracion, validado.listaErrores);
        } else {
            info.AclaracionFechaAtencion = transformarFecha_ddMMaaaa(info.AclaracionFechaAtencion)
            info.AclaracionFechaAclaracion = transformarFecha_ddMMaaaa(info.AclaracionFechaAclaracion)
            let datos = validarDatosNull(info);
            ////console.log("info p", datos)

            // Agregar la nueva fila
            otablaConsultas.row.add(datos).draw();
            limpiarAgregarModal();
        }
    }).catch(function (error) {
        alerta("error", "Ocurri� un error al capturar la aclaraci�n", "Int�ntelo nuevamente");
        console.error('Error al construir el objeto info:', error);
    });
});

//Actualizar informaci�n del registro seleccionado
$("#btn_actualizar").on("click", function () {
    construyeObjetoInfo().then(function (info) {
        ////console.log('info', info)
        let validado = validarInformacion(info);
        if (validado.hayErrores) {
            cargarErrores(info.AclaracionTrimestre, info.AclaracionFolioAtencion, info.AclaracionFechaAclaracion, validado.listaErrores);
        } else {
            info.AclaracionFechaAtencion = transformarFecha_ddMMaaaa(info.AclaracionFechaAtencion)
            info.AclaracionFechaAclaracion = transformarFecha_ddMMaaaa(info.AclaracionFechaAclaracion)
            info.AclaracionFechaResolucion = transformarFecha_ddMMaaaa(info.AclaracionFechaResolucion)
            info.AclaracionFechaNotifiUsuario = transformarFecha_ddMMaaaa(info.AclaracionFechaNotifiUsuario)
            let newData = validarDatosNull(info);
            ////console.log("info p", datos)
            let rowIndex = $("#indice-fila").val();
            otablaConsultas.row(rowIndex).data(newData).draw();
            limpiarAgregarModal();
            $("#btn_agregar").show();
            $("#btn_actualizar").hide();
        }
    }).catch(function (error) {
        alerta("error", "Ocurri� un error al capturar la aclaraci�n", "Int�ntelo nuevamente");
        console.error('Error al construir el objeto info:', error);
    });
})

//Select

$("#productos-select").on("change", function () {
    let idProducto = $("#productos-select").val();
    if (!esCadenaVaciaOBlanca(idProducto)) {
        $.ajax({
            url: urlConsultarCausas,
            method: "POST",
            cache: false,
            data: {
                idProducto: idProducto,
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
                if (respuesta.success) {
                    llenarSelect("causas-select", respuesta.data);
                } else {
                    //alerta("error", "No se pudo cargar las causas", "");
                }
            },
            error: function (respuesta) {
                //alerta("error", "No se pudo cargar las causas", "");
            }
        })
    }
})

$("#estados-select").on("change", function () {
    let id = $("#estados-select").val();
    if (!esCadenaVaciaOBlanca(id)) {
        $.ajax({
            url: urlConsultarMunicipios,
            method: "POST",
            cache: false,
            data: {
                idEstado: id,
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
                    llenarSelect("municipios-select", respuesta.data);
                } else {
                    //alerta("error", "No se pudo cargar los municipios", "");
                }
            },
            error: function (respuesta) {
                //alerta("error", "No se pudo cargar los municipios", "");
            }
        })
    }
})

$("#municipios-select").on("change", function () {
    let idE = $("#estados-select").val();
    let idM = $("#municipios-select").val();
    if (!esCadenaVaciaOBlanca(idE) || !esCadenaVaciaOBlanca(idM)) {
        $.ajax({
            url: urlConsultarCP,
            method: "POST",
            cache: false,
            data: {
                idEstado: idE,
                idMunicipio: idM,
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
                    llenarSelect("cp-select", respuesta.data);
                } else {
                    //alerta("error", "No se pudo cargar los c�digos postales", "");
                }
            },
            error: function (respuesta) {
                //alerta("error", "No se pudo cargar los c�digos postales", "");
            }
        })
    }
})

$("#cp-select").on("change", function () {
    let idE = $("#estados-select").val();
    let idM = $("#municipios-select").val();
    let idC = $("#cp-select").val();
    if (!esCadenaVaciaOBlanca(idE) || !esCadenaVaciaOBlanca(idM) || !esCadenaVaciaOBlanca(idC)) {
        $.ajax({
            url: urlConsultarColonias,
            method: "POST",
            cache: false,
            data: {
                idEstado: idE,
                idMunicipio: idM,
                cp: idC,
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
                    llenarSelect("colonias-select", respuesta.data);
                    tipoLocalidad = respuesta.data;

                } else {
                    //alerta("error", "No se pudo cargar las colonias", "");
                }
            },
            error: function (respuesta) {
                //alerta("error", "No se pudo cargar las colonias", "");
            }
        })
    }
})

$('input[name=persona-radio]').on('change', function () {
    if ($('input[name=persona-radio]:checked').val() === "2") {
        $('input[name=sexo-radio]').prop('disabled', true);
        $('input[name=sexo-radio][value=H]').prop('checked', true);
    } else {
        $('input[name=sexo-radio]').prop('disabled', false);
    }
});

// Eventos Inputs

$('input[name=estatus-radio]').on('change', function () {
    if ($('input[name=estatus-radio]:checked').val() === "1") {
        $("#AclaracionFechaAtencion").prop('disabled', true);
        $("#AclaracionFechaResolucion").prop('disabled', true);
        $("#AclaracionFechaNotifiUsuario").prop('disabled', true);
        $('#niveles-select').prop('disabled', true);
    } else {
        $("#AclaracionFechaAtencion").prop('disabled', false);
        $("#AclaracionFechaResolucion").prop('disabled', false);
        $("#AclaracionFechaNotifiUsuario").prop('disabled', false);
        $('#niveles-select').prop('disabled', false);
    }
});

$('input[name=persona-radio]').on('change', function () {
    if ($('input[name=persona-radio]:checked').val() === "2") {
        $('input[name=sexo-radio]').prop('disabled', true);
        $('input[name=sexo-radio][value=H]').prop('checked', true);
        $('#AclaracionEdad').prop('disabled', true);
    } else {
        $('input[name=sexo-radio]').prop('disabled', false);
        $('#AclaracionEdad').prop('disabled', false);
    }
});

$('input[name=monetario-radio]').on('change', function () {
    if ($('input[name=monetario-radio]:checked').val() === "NO") {
        $('#AclaracionMontoReclamado').prop('disabled', true);
    } else {
        $('#AclaracionMontoReclamado').prop('disabled', false);
    }
});


$('#medios-select').on('change', function () {
    if ($('#medios-select').val() !== "6" && $('#medios-select').val() !== "7") {
        $('#AclaracionFolioCondusef').prop('disabled', true);
        $('input[name=reversa-radio]').prop('disabled', true);
    } else {
        if ($('#medios-select').val() === "6") $('input[name=reversa-radio]').prop('disabled', false);
        $('#AclaracionFolioCondusef').prop('disabled', false);
    }
});


//Functiones


function cargarErrores(ConsultasTrim, ConsultasFolio, ConsultasFecRecepcion, errors) {
    cambiarValorSelect("trimestre_Ver", ConsultasTrim);
    mostrarAnioActual("anioActual_Ver")
    $("#folio_Ver").val(ConsultasFolio);
    $("#fechaRecepcion_Ver").val(transformarFecha_aaaaMMdd(ConsultasFecRecepcion));

    $.each(errors, function (index, dato) {
        otablaErroress.row.add({ descripcion: dato }).draw();
    });
    //$("#btn_ErroresQueja").click()
    $("#modalErrores").modal("show");
}

function limpiarAgregarModal() {
    $("#modalAgregar").modal("hide");
    $("#AclaracionNumero").val("");
    $("#AclaracionFolioAtencion").val("");
    $('input[name=estatus-radio][value=1]').prop('checked', true);
    $("#AclaracionFechaAclaracion").val("");
    reiniciarSelect("medios-select");
    reiniciarSelect("niveles-select");
    reiniciarSelect("productos-select");
    reiniciarSelect("causas-select");
    reiniciarSelect("trimestre-select")
    $('input[name=pori-radio][value=NO]').prop('checked', true);
    $("#AclaracionFechaAtencion").val("");
    $("#AclaracionFechaResolucion").val("");
    $("#AclaracionFechaNotifiUsuario").val("");
    reiniciarSelect("estados-select");
    reiniciarSelect("municipios-select");
    reiniciarSelect("colonias-select");
    reiniciarSelect("cp-select");
    $('input[name=monetario-radio][value=NO]').prop('checked', true);
    $("#AclaracionMontoReclamado").val("0");
    $('input[name=persona-radio][value=1]').prop('checked', true);
    $('input[name=sexo-radio][value=H]').prop('checked', true);
    $("#AclaracionEdad").val("0");
    reiniciarSelect("RecSentidoResolucion")
    $("#AclaracionFolioCondusef").val("");
    $('input[name=reversa-radio][value=0]').prop('checked', true);
    $('input[name=operacion-radio][value=NO]').prop('checked', true);

    $("#btn_agregar").show();
    $("#btn_actualizar").hide();
};

function cargarModificarInfo(fila) {
    cambiarValorSelect("trimestre-select", fila.AclaracionTrimestre);
    $("#AclaracionDenominacion").val(fila.AclaracionDenominacion);
    $("#AclaracionSector").val(fila.AclaracionSector);
    $("#AclaracionNumero").val(fila.AclaracionNumero);
    $("#AclaracionFolioAtencion").val(fila.AclaracionFolioAtencion);
    $("#AclaracionFechaAclaracion").val(transformarFecha_aaaaMMdd(fila.AclaracionFechaAclaracion));
    $("#AclaracionFechaAtencion").val(transformarFecha_aaaaMMdd(fila.AclaracionFechaAtencion));
    $("#AclaracionFechaResolucion").val(transformarFecha_aaaaMMdd(fila.AclaracionFechaResolucion));
    $("#AclaracionFechaNotifiUsuario").val(transformarFecha_aaaaMMdd(fila.AclaracionFechaNotifiUsuario));
    cambiarValorSelect("medios-select", fila.AclaracionMedioRecepcionCanal);
    cambiarValorSelect("niveles-select", fila.AclaracionNivelAtencion);
    cambiarValorSelect("productos-select", fila.AclaracionProductoServicio);
    setTimeout(function () {
        cambiarValorSelect("causas-select", fila.AclaracionCausaMotivo);
    }, 1000);

    $(`input[name=estatus-radio][value=${fila.AclaracionEstadoConPend}]`).prop('checked', true);
    $(`input[namepersona-radio][value=${fila.AclaracionTipoPersona}]`).prop('checked', true);
    $(`input[name=pori-radio][value = ${fila.AclaracionPori}]`).prop('checked', true);
    if (fila.AclaracionReversa != "null" && fila.AclaracionReversa != "") {
        $(`input[name=reversa-radio][value = ${fila.AclaracionReversa}]`).prop('checked', true);
    }
    $(`input[name=monetario-radio][value = ${fila.AclaracionMonetario}]`).prop('checked', true);
    if (fila.AclaracionSexo != "null" && fila.AclaracionSexo != "") {
        $(`input[name=sexo-radio][value = ${fila.AclaracionSexo}]`).prop('checked', true);
    }

    cambiarValorSelect("estados-select", fila.AclaracionEntidadFederativa);
    setTimeout(function () {
        cambiarValorSelect("municipios-select", fila.AclaracionMunicipioAlcaldia);
    }, 1000);
    setTimeout(function () {
        cambiarValorSelect("cp-select", fila.AclaracionCodigoPostal);
    }, 1500);
    setTimeout(function () {
        cambiarValorSelect("colonias-select", fila.AclaracionColonia);
    }, 2000);

    $("#AclaracionEdad").val(fila.AclaracionEdad);
    $("#AclaracionFolioCondusef").val(fila.AclaracionFolioCondusef);
    $(`input[name=operacion-radio][value = ${fila.AclaracionOperacionExtranjero}]`).prop('checked', true);

    $("#btn_agregar").hide();
    $("#btn_actualizar").show();
}

function validarInformacion(info) {
    //obtenerPeriodoReportar()
    let errores = [];
    //Que no sean datos vac�os
    if (esCadenaVaciaOBlanca(info.AclaracionNumero)) errores.push("Debe agregar un folio");
    if (esCadenaVaciaOBlanca(info.AclaracionFolioAtencion)) errores.push("Debe agregar un folio");
    if (esCadenaVaciaOBlanca(info.AclaracionFechaAclaracion)) errores.push("Debe agregar una fecha de reclamaci�n");
    if (esCadenaVaciaOBlanca(info.AclaracionMedioRecepcionCanal)) errores.push("Debe seleccionar un medio de recepci�n");
    //if (esCadenaVaciaOBlanca(info.AclaracionNivelAtencion)) errores.push("Debe seleccionar un nivel de atenci�n");
    if (esCadenaVaciaOBlanca(info.AclaracionProductoServicio)) errores.push("Debe seleccionar un producto");
    if (esCadenaVaciaOBlanca(info.AclaracionCausaMotivo)) errores.push("Debe seleccionar una causa");
    if (esCadenaVaciaOBlanca(info.AclaracionEntidadFederativa)) errores.push("Debe seleccionar una entidad federativa");
    if (esCadenaVaciaOBlanca(info.AclaracionMunicipioAlcaldia)) errores.push("Debe seleccionar un municipio");
    //if ((info.AclaracionMedioRecepcionCanal == 3 || info.AclaracionMedioRecepcionCanal == 5 || info.AclaracionMedioRecepcionCanal == 17) && esCadenaVaciaOBlanca(info.AclaracionColonia)) errores.push("Debe seleccionar una colonia");
    if ((info.AclaracionMedioRecepcionCanal == 3 || info.AclaracionMedioRecepcionCanal == 5 || info.AclaracionMedioRecepcionCanal == 17) && esCadenaVaciaOBlanca(info.AclaracionCodigoPostal)) errores.push("Debe seleccionar un c�digo postal");

    if (info.AclaracionEstadoConPend == 2 && esCadenaVaciaOBlanca(info.AclaracionNivelAtencion)) {
        errores.push("Debe seleccionar un nivel de atenci�n");
    }

    if (info.AclaracionEstadoConPend == 2 && esCadenaVaciaOBlanca(info.AclaracionFechaAtencion)) {
        errores.push("Debe agregar una fecha de atenci�n");
    }
    if (info.AclaracionEstadoConPend == 2 && esCadenaVaciaOBlanca(info.AclaracionFechaResolucion)) {
        errores.push("Debe agregar una fecha de resoluci�n");
    }
    if (info.AclaracionEstadoConPend == 2 && esCadenaVaciaOBlanca(info.AclaracionFechaNotifiUsuario)) {
        errores.push("Debe agregar una fecha de notificaci�n al usuario");
    }
    if (info.AclaracionMonetario == "SI" && esEnteroValidoNiCero(info.AclaracionMontoReclamado)) {
        errores.push("Debe agregar un monto de reclamaci�n v�lido");
    }
    //if (info.AclaracionTipoPersona == 1 && esEnteroValidoNiCero(info.RecImporteAbonado)) {
    //    errores.push("Debe agregar un importe abonado v�lido");
    //}

    if (info.AclaracionTipoPersona == 1 && esEnteroValidoNiCero(info.AclaracionEdad)) {
        errores.push("Debe agregar una edad v�lida");
    }

    if ((info.AclaracionMedioRecepcionCanal == 6 || info.AclaracionMedioRecepcionCanal == 7) && esCadenaVaciaOBlanca(info.AclaracionFolioCondusef)) {
        errores.push("Debe agregar el folio emitido por la CONDUSEF");
    }

    //Validar fechas
    if (!validaFechaPeriodoReune(info.AclaracionFechaAclaracion, info.AclaracionTrimestre)) errores.push("La fecha de reclamaci�n no puede ser mayor al trimestre a reportar");

    if (info.AclaracionEstadoConPend == 2) {
        if (valida2Fechas(info.AclaracionFechaAclaracion, info.AclaracionFechaAtencion)) errores.push("La fecha de atenci�n no puede ser menor que la fecha de recepci�n");
        if (valida2Fechas(info.AclaracionFechaAclaracion, info.AclaracionFechaResolucion)) errores.push("La fecha de resoluci�n no puede ser menor que la fecha de recepci�n");
        if (valida2Fechas(info.AclaracionFechaResolucion, info.AclaracionFechaNotifiUsuario)) errores.push("La fecha de notificaci�n no puede ser menor que la fecha de resoluci�n");
    }

    let _hayErrores = errores.length > 0;
    return {
        hayErrores: _hayErrores,
        listaErrores: errores,
    }
}

function validarDatosNull(info) {
    if (info.AclaracionMonetario == "NO") info.AclaracionMontoReclamado = "null";
    if (info.AclaracionMedioRecepcionCanal < 6 && info.AclaracionMedioRecepcionCanal > 7) info.AclaracionFolioCondusef = "null";
    if (info.AclaracionEstadoConPend == 1) {
        info.AclaracionNivelAtencion = "null";
        info.AclaracionFechaAtencion = "null";
        info.AclaracionFechaResolucion = "null";
        info.AclaracionFechaNotifiUsuario = "null";
    }

    if (info.AclaracionMedioRecepcionCanal != 6) info.AclaracionReversa = "null";

    if (info.AclaracionTipoPersona == "2") {
        info.AclaracionSexo = "null";
        info.AclaracionEdad = "null";
    }

    //if (info.AclaracionMedioRecepcionCanal != 3 && info.AclaracionMedioRecepcionCanal != 5 && info.AclaracionMedioRecepcionCanal != 17) {
    //    info.AclaracionCodigoPostal = "null";
    //    info.AclaracionColonia = "null";
    //    info.AclaracionLocalidad = "null";
    //}

    return info;
}

function obtenerPeriodoReportar() {
    const fecha = new Date();
    const mes = fecha.getMonth() + 1; // Se suma 1 ya que los meses van de 0 a 11
    let trimestre;

    if (mes >= 1 && mes <= 3) {
        trimestre = 1;
    } else if (mes >= 4 && mes <= 6) {
        trimestre = 2;
    } else if (mes >= 7 && mes <= 9) {
        trimestre = 3;
    } else {
        trimestre = 4;
    }

    cambiarValorSelect("trimestre-select", trimestre.toString());
}

function obtenerTrimestre() {
    const fecha = new Date();
    const mes = fecha.getMonth() + 1; // Se suma 1 ya que los meses van de 0 a 11
    let trimestre;

    if (mes >= 1 && mes <= 3) {
        trimestre = 1;
    } else if (mes >= 4 && mes <= 6) {
        trimestre = 2;
    } else if (mes >= 7 && mes <= 9) {
        trimestre = 3;
    } else {
        trimestre = 4;
    }

    return trimestre;
}