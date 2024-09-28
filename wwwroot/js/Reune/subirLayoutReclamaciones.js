import { RECLAMACIONES_FICTICIAS, VISTAS } from "../constantes.js";

//Variables
var tipoLocalidad = [];

var tabla = null;

//Ready
//$(document).ready(function () {
//    mostrarAnioActual()
//})

// Llamar a la función para mostrar el año actual cuando se cargue la página
mostrarAnioActual("anioActual");


//Tablas

// tablaReclamaciones

var otablaConsultas = $("#tablaReclamaciones").DataTable({
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
            { "data": "RecNumero", "name": "RecNumero", "autowidth": true },
            { "data": "RecFolioAtencion", "name": "RecFolioAtencion", "autowidth": true },
            { "data": "RecEstadoConPend", "name": "RecEstadoConPend", "autowidth": true },
            { "data": "RecFechaReclamacion", "name": "RecFechaReclamacion", "autowidth": true },
            { "data": "RecMedioRecepcionCanal", "name": "RecMedioRecepcionCanal", "autowidth": true },
            { "data": "RecNivelAtencion", "name": "RecNivelAtencion", "autowidth": true },
            { "data": "RecProductoServicio", "name": "RecProductoServicio", "autowidth": true },
            { "data": "RecCausaMotivo", "name": "RecCausaMotivo", "autowidth": true },
            { "data": "RecDenominacion", "name": "RecDenominacion", "autowidth": true },
            { "data": "RecSector", "name": "RecSector", "autowidth": true },
            { "data": "RecTrimestre", "name": "RecTrimestre", "autowidth": true },
            { "data": "RecPori", "name": "RecPori", "autowidth": true },
            { "data": "RecFechaAtencion", "name": "RecFechaAtencion", "autowidth": true },
            { "data": "RecFechaResolucion", "name": "RecFechaResolucion", "autowidth": true },
            { "data": "RecFechaNotifiUsuario", "name": "RecFechaNotifiUsuario", "autowidth": true },
            { "data": "RecEntidadFederativa", "name": "RecEntidadFederativa", "autowidth": true },
            { "data": "RecMunicipioAlcaldia", "name": "RecMunicipioAlcaldia", "autowidth": true },
            { "data": "RecColonia", "name": "RecColonia", "autowidth": true },
            { "data": "RecCodigoPostal", "name": "RecCodigoPostal", "autowidth": true },
            { "data": "RecLocalidad", "name": "RecLocalidad", "autowidth": true },
            { "data": "RecMonetario", "name": "RecMonetario", "autowidth": true },
            { "data": "RecMontoReclamado", "name": "RecMontoReclamado", "autowidth": true },
            { "data": "RecImporteAbonado", "name": "RecImporteAbonado", "autowidth": true },
            { "data": "RecFechaAbonoImporte", "name": "RecFechaAbonoImporte", "autowidth": true },
            { "data": "RecTipoPersona", "name": "RecTipoPersona", "autowidth": true },
            { "data": "RecSexo", "name": "RecSexo", "autowidth": true },
            { "data": "RecEdad", "name": "RecEdad", "autowidth": true },
            { "data": "RecSentidoResolucion", "name": "RecSentidoResolucion", "autowidth": true },
            { "data": "RecFolioCondusef", "name": "RecFolioCondusef", "autowidth": true },
            { "data": "RecReversa", "name": "RecReversa", "autowidth": true },
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
                "targets": [8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29],
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
                "targets": [30, 31],
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
            { "data": "recNumero", "name": "RecNumero", "autowidth": true },
            { "data": "recFolioAtencion", "name": "RecFolioAtencion", "autowidth": true },
            { "data": "recEstadoConPend", "name": "RecEstadoConPend", "autowidth": true },
            { "data": "recFechaReclamacion", "name": "RecFechaReclamacion", "autowidth": true },
            { "data": "recMedioRecepcionCanal", "name": "RecMedioRecepcionCanal", "autowidth": true },
            { "data": "recNivelAtencion", "name": "RecNivelAtencion", "autowidth": true },
            { "data": "recProductoServicio", "name": "RecProductoServicio", "autowidth": true },
            { "data": "recCausaMotivo", "name": "RecCausaMotivo", "autowidth": true },
            { "data": "recDenominacion", "name": "RecDenominacion", "autowidth": true },
            { "data": "recSector", "name": "RecSector", "autowidth": true },
            { "data": "recTrimestre", "name": "RecTrimestre", "autowidth": true },
            { "data": "recPori", "name": "RecPori", "autowidth": true },
            { "data": "recFechaAtencion", "name": "RecFechaAtencion", "autowidth": true },
            { "data": "recFechaResolucion", "name": "RecFechaResolucion", "autowidth": true },
            { "data": "recFechaNotifiUsuario", "name": "RecFechaNotifiUsuario", "autowidth": true },
            { "data": "recEntidadFederativa", "name": "RecEntidadFederativa", "autowidth": true },
            { "data": "recMunicipioAlcaldia", "name": "RecMunicipioAlcaldia", "autowidth": true },
            { "data": "recColonia", "name": "RecColonia", "autowidth": true },
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
                "targets": [8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 31],
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
                "targets": [30],
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

$('#tablaReclamaciones tbody').on('click', 'button.btn-modificar', function () {
    // Obtener la fila correspondiente al botón
    var fila = otablaConsultas.row($(this).closest('tr')).data();
    //tabla = otablaConsultas.row($(this).closest('tr'));
    ////console.log('tabla',fila)
    let tr = $(this).closest('tr');
    let row = otablaConsultas.row(tr).index();

    $("#indice-fila").val(row)
    cargarModificarInfo(fila);
    $("#modalAgregar").modal("show");
});

$('#tablaReclamaciones tbody').on('click', '.btn-eliminar', function () {
    Swal.fire({
        title: textoScript.mEliminarRegistro,
        showDenyButton: true,
        icon: "warning",
        confirmButtonText: textoScript.si,
        denyButtonText: textoScript.no
    }).then((result) => {
        // Verificar si el usuario hizo clic en el botón "Deny"
        if (result.isConfirmed) {
            // Obtener la fila que contiene el botón
            var fila = $(this).closest('tr');

            // Eliminar la fila del DataTable
            otablaConsultas.row(fila).remove().draw();
            alerta("error", textoScript.mRegistroEliminado)
        }
    });
});

$('#tablaListadoErrores tbody').on('click', 'button.btn-errores', function () {
    // Obtener la fila correspondiente al botón
    var fila = otablaListadoErrores.row($(this).closest('tr')).data();
    console.log(fila)
    cargarErrores(fila.recTrimestre, fila.recFolioAtencion, fila.recFechaReclamacion, fila.errors)
});


//Botones
$("#btn_agregarDatosFicticios").on('click', function () {
    var tabla = $('#tablaReclamaciones').DataTable();
    $.each(RECLAMACIONES_FICTICIAS, function (index, consulta) {
        consulta.RecDenominacion = $("#institucion-clave").val();
        consulta.RecSector = $("#sector").val();
        consulta.RecFolioAtencion = generarFolio();
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
    var tabla = $('#tablaReclamaciones').DataTable();

    // Obtener los datos de la tabla
    var datos = tabla.rows().data();

    // Array para almacenar los datos como objetos
    var objetoDatos = [];

    // Iterar sobre los datos y construir el array de objetos
    datos.each(function (index, fila) {
        if (index.RecNivelAtencion == "null") index.RecNivelAtencion = null;
        if (index.RecSentidoResolucion == "null") index.RecSentidoResolucion = null;
        if (index.RecImporteAbonado == "null") index.RecImporteAbonado = null;
        if (index.RecMontoReclamado == "null") index.RecMontoReclamado = null;
        if (index.RecFolioCondusef == "null") index.RecFolioCondusef = null;
        if (index.RecFechaResolucion == "null") index.RecFechaResolucion = null;
        if (index.RecFechaNotifiUsuario == "null") index.RecFechaNotifiUsuario = null;
        if (index.RecFechaAtencion == "null") index.RecFechaAtencion = null;
        if (index.RecReversa == "null") index.RecReversa = null;
        if (index.RecFechaAbonoImporte == "null") index.RecFechaAbonoImporte = null;
        if (index.RecSentidoResolucion == "null") index.RecSentidoResolucion = null;
        if (index.RecCodigoPostal == "null") index.RecCodigoPostal = null;
        if (index.RecColonia == "null") index.RecColonia = null;
        if (index.RecLocalidad == "null") index.RecLocalidad = null;
        objetoDatos.push(index);
    });

    // Mostrar el array de objetos en la consola
    ////console.log(objetoDatos);
    if (objetoDatos.length == 0) {
        alerta("error", "Debe agregar una o más reclamaciones")
    }
    else {
        $.ajax({
            url: urlEnviarDatos,
            method: "POST",
            cache: false,
            data: {
                anio: getAnioActual(),
                reclamaciones: objetoDatos,
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
                    if (respuesta.code === 200) {
                        alertaConRecarga("success", textoScript.mExitoEnvioQuejas, "")
                    } else if (respuesta.code >= 500) {
                        alerta("error", "El servicio de reune no funciona. Intentelo mas tarde")
                    } else {
                        alerta("error", textoScript.mErrorEnvioQuejas, "")
                        otablaConsultas.clear().draw()
                        ConstruirTablaElementos(otablaListadoErrores, respuesta.errores);

                        //$("#btn_ListadoErrores").click();
                        $("#modalListadoErrores").modal("show");
                    }
                } else {
                    alerta("error", textoScript.mErrorCondusef, "")
                    alerta("error", "El servicio de Reune no funciona. Intentelo mas tarde")
                }

            },
            error: function (respuesta) {
                alerta("error", textoScript.mErrorPeticion)
            }
        })
    }
})

$("#btn_corregirQuejas").on("click", function () {
    var tabla = $('#tablaListadoErrores').DataTable();
    var tabla2 = $('#tablaReclamaciones').DataTable();

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
        obj.RecCodigoPostal = convertirCP(obj.RecCodigoPostal);
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
            "RecDenominacion": $("#institucion-clave").val(),
            "RecSector": $("#sector").val(),
            "RecNumero": $("#RecNumero").val(),
            "RecTrimestre": $("#trimestre-select").val(),
            "RecFolioAtencion": $("#RecFolioAtencion").val().trim(),
            "RecEstadoConPend": $('input[name=estatus-radio]:checked').val(),
            "RecFechaAtencion": $("#RecFechaAtencion").val(),
            "RecFechaReclamacion": $("#RecFechaReclamacion").val(),
            "RecMedioRecepcionCanal": $("#medios-select").val(),
            "RecNivelAtencion": $("#niveles-select").val(),
            "RecProductoServicio": $("#productos-select").val(),
            "RecCausaMotivo": $("#causas-select").val(),
            "RecFechaResolucion": $("#RecFechaResolucion").val(),
            "RecFechaNotifiUsuario": $("#RecFechaNotifiUsuario").val(),
            "RecEntidadFederativa": $("#estados-select").val(),
            "RecMunicipioAlcaldia": $("#municipios-select").val(),
            "RecColonia": $("#colonias-select").val(),
            "RecCodigoPostal": $("#cp-select").val(),
            "RecMonetario": $('input[name=monetario-radio]:checked').val(),
            "RecMontoReclamado": $("#RecMontoReclamado").val(),
            "RecImporteAbonado": $("#RecImporteAbonado").val(),
            "RecFechaAbonoImporte": $("#RecFechaAbonoImporte").val(),
            "RecPori": $('input[name=pori-radio]:checked').val(),
            "RecTipoPersona": $('input[name=persona-radio]:checked').val(),
            "RecSexo": $('input[name=sexo-radio]:checked').val(),
            "RecEdad": $("#RecEdad").val(),
            "RecSentidoResolucion": $("#RecSentidoResolucion").val(),
            "RecFolioCondusef": $("#RecFolioCondusef").val().trim(),
            "RecReversa": $('input[name=reversa-radio]:checked').val(),
            "RecLocalidad": validaStringNumerico(objetoSeleccionado?.valor)
        };

        // Resolve la promesa con el objeto info
        resolve(info);
    });
}

//Agregar dato a tabla
$("#btn_agregar").on('click', function () {
    //var tabla = $('#tablaReclamaciones').DataTable();
    construyeObjetoInfo().then(function (info) {
        ////console.log('info', info)
        let validado = validarInformacion(info);
        if (validado.hayErrores) {
            cargarErrores(info.RecTrimestre, info.RecFolioAtencion, info.RecFechaReclamacion, validado.listaErrores);
        } else {
            info.RecFechaAtencion = transformarFecha_ddMMaaaa(info.RecFechaAtencion)
            info.RecFechaReclamacion = transformarFecha_ddMMaaaa(info.RecFechaReclamacion)
            info.RecFechaAbonoImporte = transformarFecha_ddMMaaaa(info.RecFechaAbonoImporte)
            info.RecFechaResolucion = transformarFecha_ddMMaaaa(info.RecFechaResolucion)
            info.RecFechaNotifiUsuario = transformarFecha_ddMMaaaa(info.RecFechaNotifiUsuario)
            let datos = validarDatosNull(info);
            ////console.log("info p", datos)

            // Agregar la nueva fila
            otablaConsultas.row.add(datos).draw();
            limpiarAgregarModal();
        }
    }).catch(function (error) {
        alerta("error", "Ocurrió un error al capturar la reclamación", "Inténtelo nuevamente");
        console.error('Error al construir el objeto info:', error);
    });
});

//Actualizar información del registro seleccionado
$("#btn_actualizar").on("click", function () {
    construyeObjetoInfo().then(function (info) {
        ////console.log('info', info)
        let validado = validarInformacion(info);
        if (validado.hayErrores) {
            cargarErrores(info.RecTrimestre, info.RecFolioAtencion, info.RecFechaReclamacion, validado.listaErrores);
        } else {
            info.RecFechaAtencion = transformarFecha_ddMMaaaa(info.RecFechaAtencion)
            info.RecFechaReclamacion = transformarFecha_ddMMaaaa(info.RecFechaReclamacion)
            info.RecFechaAbonoImporte = transformarFecha_ddMMaaaa(info.RecFechaAbonoImporte)
            info.RecFechaResolucion = transformarFecha_ddMMaaaa(info.RecFechaResolucion)
            info.RecFechaNotifiUsuario = transformarFecha_ddMMaaaa(info.RecFechaNotifiUsuario)
            let newData = validarDatosNull(info);

            let rowIndex = $("#indice-fila").val();
            otablaConsultas.row(rowIndex).data(newData).draw();

            limpiarAgregarModal();
            $("#btn_agregar").show();
            $("#btn_actualizar").hide();
        }
    }).catch(function (error) {
        alerta("error", "Ocurrió un error al capturar la reclamación", "Inténtelo nuevamente");
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
                if (respuesta.success) {
                    llenarSelect("causas-select", respuesta.data);
                } else {
                    alerta("error", "No se pudo cargar las causas", "");
                }
            },
            error: function (respuesta) {
                alerta("error", "No se pudo cargar las causas", "");
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
                    alerta("error", "No se pudo cargar los municipios", "");
                }
            },
            error: function (respuesta) {
                alerta("error", "No se pudo cargar los municipios", "");
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
                    alerta("error", "No se pudo cargar los códigos postales", "");
                }
            },
            error: function (respuesta) {
                alerta("error", "No se pudo cargar los códigos postales", "");
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
                    alerta("error", "No se pudo cargar las colonias", "");
                }
            },
            error: function (respuesta) {
                alerta("error", "No se pudo cargar las colonias", "");
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
    $("#RecNumero").val("");
    $("#RecFolioAtencion").val("");
    $('input[name=estatus-radio][value=1]').prop('checked', true);
    $("#RecFechaReclamacion").val("");
    reiniciarSelect("medios-select");
    reiniciarSelect("niveles-select");
    reiniciarSelect("productos-select");
    reiniciarSelect("causas-select");
    reiniciarSelect("trimestre-select")
    $('input[name=pori-radio][value=NO]').prop('checked', true);
    $("#RecFechaAtencion").val("");
    $("#RecFechaResolucion").val("");
    $("#RecFechaNotifiUsuario").val("");
    reiniciarSelect("estados-select");
    reiniciarSelect("municipios-select");
    reiniciarSelect("colonias-select");
    reiniciarSelect("cp-select");
    $('input[name=monetario-radio][value=NO]').prop('checked', true);
    $("#RecMontoReclamado").val("0");
    $("#RecImporteAbonado").val("0");
    $("#RecFechaAbonoImporte").val("0");
    $('input[name=persona-radio][value=1]').prop('checked', true);
    $('input[name=sexo-radio][value=H]').prop('checked', true);
    $("#RecEdad").val("0");
    reiniciarSelect("RecSentidoResolucion")
    $("#RecFolioCondusef").val("");
    $('input[name=reversa-radio][value=0]').prop('checked', true);

    $("#btn_agregar").show();
    $("#btn_actualizar").hide();
};

function cargarModificarInfo(fila) {
    console.log("fila", fila)
    cambiarValorSelect("trimestre-select", fila.RecTrimestre);
    $("#RecDenominacion").val(fila.RecDenominacion);
    $("#RecSector").val(fila.RecSector);
    $("#RecNumero").val(fila.RecNumero);
    $("#RecFolioAtencion").val(fila.RecFolioAtencion);
    $("#RecMontoReclamado").val(fila.RecMontoReclamado);
    $("#RecImporteAbonado").val(fila.RecImporteAbonado);
    $("#RecFechaReclamacion").val(transformarFecha_aaaaMMdd(fila.RecFechaReclamacion));
    $("#RecFechaAtencion").val(transformarFecha_aaaaMMdd(fila.RecFechaAtencion));
    $("#RecFechaResolucion").val(transformarFecha_aaaaMMdd(fila.RecFechaResolucion));
    $("#RecFechaNotifiUsuario").val(transformarFecha_aaaaMMdd(fila.RecFechaNotifiUsuario));
    $("#RecFechaAbonoImporte").val(transformarFecha_aaaaMMdd(fila.RecFechaAbonoImporte));
    cambiarValorSelect("medios-select", fila.RecMedioRecepcionCanal);
    cambiarValorSelect("niveles-select", fila.RecNivelAtencion);
    cambiarValorSelect("productos-select", fila.RecProductoServicio);
    setTimeout(function () {
        cambiarValorSelect("causas-select", fila.RecCausaMotivo);
    }, 2000);

    $(`input[name=estatus-radio][value=${fila.RecEstadoConPend}]`).prop('checked', true);
    $(`input[namepersona-radio][value=${fila.RecTipoPersona}]`).prop('checked', true);
    $(`input[name=pori-radio][value = ${fila.RecPori}]`).prop('checked', true);
    if (fila.RecReversa != "null" && fila.RecReversa != "") {
        $(`input[name=reversa-radio][value = ${fila.RecReversa}]`).prop('checked', true);
    }
    $(`input[name=monetario-radio][value = ${fila.RecMonetario}]`).prop('checked', true);
    if (fila.RecSexo != "null" && fila.RecSexo != "") {
        $(`input[name=sexo-radio][value = ${fila.RecSexo}]`).prop('checked', true);
    }

    cambiarValorSelect("estados-select", fila.RecEntidadFederativa);
    setTimeout(function () {
        cambiarValorSelect("municipios-select", fila.RecMunicipioAlcaldia);
    }, 1000);
    setTimeout(function () {
        cambiarValorSelect("cp-select", fila.RecCodigoPostal);
    }, 1500);
    setTimeout(function () {
        cambiarValorSelect("colonias-select", fila.RecColonia);
    }, 2000);

    $("#RecEdad").val(fila.RecEdad);
    $("#RecFolioCondusef").val(fila.RecFolioCondusef);
    cambiarValorSelect("RecSentidoResolucion", fila.RecSentidoResolucion);

    $("#btn_agregar").hide();
    $("#btn_actualizar").show();
}

function validarInformacion(info) {
    //console.log("queja")
    //console.log(info)
    //obtenerPeriodoReportar()
    let errores = [];
    //Que no sean datos vacíos
    if (esCadenaVaciaOBlanca(info.RecNumero)) errores.push("Debe agregar un folio");
    if (esCadenaVaciaOBlanca(info.RecFolioAtencion)) errores.push("Debe agregar un folio");
    if (esCadenaVaciaOBlanca(info.RecFechaReclamacion)) errores.push("Debe agregar una fecha de reclamación");
    if (esCadenaVaciaOBlanca(info.RecMedioRecepcionCanal)) errores.push("Debe seleccionar un medio de recepción");
    //if (esCadenaVaciaOBlanca(info.RecNivelAtencion)) errores.push("Debe seleccionar un nivel de atención");
    if (esCadenaVaciaOBlanca(info.RecProductoServicio)) errores.push("Debe seleccionar un producto");
    if (esCadenaVaciaOBlanca(info.RecCausaMotivo)) errores.push("Debe seleccionar una causa");
    if (esCadenaVaciaOBlanca(info.RecEntidadFederativa)) errores.push("Debe seleccionar una entidad federativa");
    if (esCadenaVaciaOBlanca(info.RecMunicipioAlcaldia)) errores.push("Debe seleccionar un municipio");
    //if ((info.RecMedioRecepcionCanal == 3 || info.RecMedioRecepcionCanal == 5 || info.RecMedioRecepcionCanal == 17) && esCadenaVaciaOBlanca(info.RecColonia)) errores.push("Debe seleccionar una colonia");
    if ((info.RecMedioRecepcionCanal == 3 || info.RecMedioRecepcionCanal == 5 || info.RecMedioRecepcionCanal == 17) && esCadenaVaciaOBlanca(info.RecCodigoPostal)) errores.push("Debe seleccionar un código postal");

    if (info.RecEstadoConPend == 2 && esCadenaVaciaOBlanca(info.RecNivelAtencion)) {
        errores.push("Debe seleccionar un nivel de atención");
    }

    if (info.RecEstadoConPend == 2 && esCadenaVaciaOBlanca(info.RecFechaAtencion)) {
        errores.push("Debe agregar una fecha de atención");
    }
    if (info.RecEstadoConPend == 2 && esCadenaVaciaOBlanca(info.RecFechaResolucion)) {
        errores.push("Debe agregar una fecha de resolución");
    }
    if (info.RecEstadoConPend == 2 && esCadenaVaciaOBlanca(info.RecFechaNotifiUsuario)) {
        errores.push("Debe agregar una fecha de notificación al usuario");
    }
    if (info.RecMonetario == "SI" && esEnteroValidoNiCero(info.RecMontoReclamado)) {
        errores.push("Debe agregar un monto de reclamación válido");
    }
    if (info.RecMonetario == "SI" && esEnteroValidoNiCero(info.RecImporteAbonado)) {
        errores.push("Debe agregar un importe abonado válido");
    }
    //if (info.RecTipoPersona == 1 && esEnteroValidoNiCero(info.RecImporteAbonado)) {
    //    errores.push("Debe agregar un importe abonado válido");
    //}

    if (info.RecTipoPersona == 1 && esEnteroValidoNiCero(info.RecEdad)) {
        errores.push("Debe agregar una edad válida");
    }
    if (info.RecEstadoConPend == 2 && esCadenaVaciaOBlanca(info.RecSentidoResolucion)) errores.push("Debe seleccionar un sentido de resolución");

    if ((info.RecMedioRecepcionCanal == 6 || info.RecMedioRecepcionCanal == 7) && esCadenaVaciaOBlanca(info.RecFolioCondusef)) {
        errores.push("Debe agregar el folio emitido por la CONDUSEF");
    }

    //Validar fechas
    if (!validaFechaPeriodoReune(info.RecFechaReclamacion, info.RecTrimestre)) errores.push("La fecha de reclamación no puede ser mayor al trimestre a reportar");

    if (info.RecEstadoConPend == 2) {
        if (valida2Fechas(info.RecFechaReclamacion, info.RecFechaAtencion)) errores.push("La fecha de atención no puede ser menor que la fecha de recepción");
        if (valida2Fechas(info.RecFechaReclamacion, info.RecFechaResolucion)) errores.push("La fecha de resolución no puede ser menor que la fecha de recepción");
        if (valida2Fechas(info.RecFechaResolucion, info.RecFechaNotifiUsuario)) errores.push("La fecha de notificación no puede ser menor que la fecha de resolución");
    }

    let _hayErrores = errores.length > 0;
    return {
        hayErrores: _hayErrores,
        listaErrores: errores
    }

}

function validarDatosNull(info) {
    if (info.RecMonetario == "NO") {
        info.RecMontoReclamado = "null";
        info.RecImporteAbonado = "null";
        info.RecFechaAbonoImporte = "null";
    }
    if (info.RecMedioRecepcionCanal < 6 && info.RecMedioRecepcionCanal > 7) info.RecFolioCondusef = "null";
    if (info.RecEstadoConPend == 1) {
        info.RecNivelAtencion = "null";
        info.RecFechaAtencion = "null";
        info.RecFechaResolucion = "null";
        info.RecFechaNotifiUsuario = "null";
        info.RecImporteAbonado = "null";
        info.RecFechaAbonoImporte = "null";
        info.RecSentidoResolucion = "null";
    }

    if (info.RecImporteAbonado == 0 || info.RecImporteAbonado == "null") {
        info.RecFechaAbonoImporte = "null";
    }

    if (info.RecMedioRecepcionCanal != 6) info.RecReversa = "null";

    //if (info.RecMedioRecepcionCanal != 3 && info.RecMedioRecepcionCanal != 5 && info.RecMedioRecepcionCanal != 17) {
    //    info.RecCodigoPostal = "null";
    //    info.RecColonia = "null";
    //    info.RecLocalidad = "null";
    //}

    return info;
}

///// -------------------------------------- Subir Layout -----------------------------------------------

function CambiaFakePath(input) {
    var FileName = input.value.toString().replace("C:\\fakepath\\", "");
    $("#inputname").val(FileName);
};

function CambiaFakePath2(input) {
    var FileName = input.value.toString().replace("C:\\fakepath\\", "");
    $("#inputname2").val(FileName);
};

//$('#modalSubirArchivo').on('show.bs.modal', function () {
//    alert('u')
//    obtenerPeriodo();
//});


$('#labelinput').on('change', function () {
    CambiaFakePath(this);
});

$('#labelinput2').on('change', function () {
    CambiaFakePath2(this);
});

$('#subirArchivo_btn').on('click', function () {
    subirLayout();
});

function subirLayout() {
    var Data = new FormData;
    Data.append("layout", document.getElementById('labelinput').files[0]);
    Data.append("__RequestVerificationToken", $("input[name='__RequestVerificationToken']").val());
    $.ajax({
        url: urlSubirLayout,
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
                alerta("success", textoScript.mExitoArchivoProcesado)
                //otablaConsultas.ajax.reload();
                let data = respuesta.data;

                let objetoDatos = [];
                // Iterar sobre los datos y construir el array de objetos
                data.forEach(function (index, fila) {
                    index = capitalizeKeys(index);
                    index.RecDenominacion = $("#institucion-clave").val();
                    index.RecSector = $("#sector").val();
                    objetoDatos.push(index);
                });
                console.log("data", objetoDatos)
                ConstruirTablaElementos(otablaConsultas, objetoDatos);
                limpiarModalSubida()
                $("#btn_subirDatos").prop("disabled", false);
            } else {
                alerta("error", textoScript.mErrorArchivoProcesado)
            }
        },
        error: function (respuesta) {
            alerta("error", textoScript.error)
            console.error('Ocurrio un error:', respuesta)
        }
    })
}

function limpiarModalSubida() {
    $('#modalSubirArchivo').modal('hide');
    $('#inputname').val();
}








