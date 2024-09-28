var tipoLocalidad = [];

var tabla = null;

//Ready
$(document).ready(function () {
    mostrarAnioActual("anioActual");
})

//Tablas
var otablaConsultas = $("#tablaQuejas").DataTable({
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
            { "data": "QuejasNum", "name": "QuejasNum", "autowidth": true },
            { "data": "QuejasNoMes", "name": "QuejasNoMes", "autowidth": true },
            { "data": "QuejasFolio", "name": "QuejasFolio", "autowidth": true },
            { "data": "QuejasFecRecepcion", "name": "QuejasFecRecepcion", "autowidth": true },
            { "data": "QuejasMedio", "name": "QuejasMedio", "autowidth": true },
            { "data": "QuejasNivelAT", "name": "QuejasNivelAT", "autowidth": true },
            { "data": "QuejasProducto", "name": "QuejasProducto", "autowidth": true },
            { "data": "QuejasCausa", "name": "QuejasCausa", "autowidth": true },
            { "data": "QuejasEstatus", "name": "QuejasEstatus", "autowidth": true },
            { "data": "QuejasTipoPersona", "name": "QuejasTipoPersona", "autowidth": true },
            { "data": "QuejasPORI", "name": "QuejasPORI", "autowidth": true },
            { "data": "QuejasEstados", "name": "QuejasEstados", "autowidth": true },
            { "data": "QuejasMunId", "name": "QuejasMunId", "autowidth": true },
            { "data": "QuejasLocId", "name": "QuejasLocId", "autowidth": true },
            { "data": "QuejasColId", "name": "QuejasColId", "autowidth": true },
            { "data": "QuejasCP", "name": "QuejasCP", "autowidth": true },
            { "data": "QuejasSexo", "name": "QuejasSexo", "autowidth": true },
            { "data": "QuejasEdad", "name": "QuejasEdad", "autowidth": true },
            { "data": "QuejasFecResolucion", "name": "QuejasFecResolucion", "autowidth": true },
            { "data": "QuejasFecNotificacion", "name": "QuejasFecNotificacion", "autowidth": true },
            { "data": "QuejasRespuesta", "name": "QuejasRespuesta", "autowidth": true },
            { "data": "QuejasPenalizacion", "name": "QuejasPenalizacion", "autowidth": true },
            { "data": "QuejasNumPenal", "name": "QuejasNumPenal", "autowidth": true },
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
            { "data": "QuejasDenominacion", "name": "QuejasDenominacion", "autowidth": true },
            { "data": "QuejasSector", "name": "QuejasSector", "autowidth": true },
        ],
    "columnDefs":
        [
            {
                "targets": [0, 1, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 25, 26],
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
                "targets": [4, 5, 7, 8, 9],
                "width": "5%"
            },
            {
                "targets": [23, 24],
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
            { "data": "quejasNum", "name": "QuejasNum", "autowidth": true },
            { "data": "quejasNoMes", "name": "QuejasNoMes", "autowidth": true },
            { "data": "quejasFolio", "name": "QuejasFolio", "autowidth": true },
            { "data": "quejasFecRecepcion", "name": "QuejasFecRecepcion", "autowidth": true },
            { "data": "quejasMedio", "name": "QuejasMedio", "autowidth": true },
            { "data": "quejasNivelAT", "name": "QuejasNivelAT", "autowidth": true },
            { "data": "quejasProducto", "name": "QuejasProducto", "autowidth": true },
            { "data": "quejasCausa", "name": "QuejasCausa", "autowidth": true },
            { "data": "quejasEstatus", "name": "QuejasEstatus", "autowidth": true },
            { "data": "quejasTipoPersona", "name": "QuejasTipoPersona", "autowidth": true },
            { "data": "quejasPORI", "name": "QuejasPORI", "autowidth": true },
            { "data": "quejasEstados", "name": "QuejasEstados", "autowidth": true },
            { "data": "quejasMunId", "name": "QuejasMunId", "autowidth": true },
            { "data": "quejasLocId", "name": "QuejasLocId", "autowidth": true },
            { "data": "quejasColId", "name": "QuejasColId", "autowidth": true },
            { "data": "quejasCP", "name": "QuejasCP", "autowidth": true },
            { "data": "quejasSexo", "name": "QuejasSexo", "autowidth": true },
            { "data": "quejasEdad", "name": "QuejasEdad", "autowidth": true },
            { "data": "quejasFecResolucion", "name": "QuejasFecResolucion", "autowidth": true },
            { "data": "quejasFecNotificacion", "name": "QuejasFecNotificacion", "autowidth": true },
            { "data": "quejasRespuesta", "name": "QuejasRespuesta", "autowidth": true },
            { "data": "quejasPenalizacion", "name": "QuejasPenalizacion", "autowidth": true },
            { "data": "quejasNumPenal", "name": "QuejasNumPenal", "autowidth": true },
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
                "targets": [0, 1, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 24],
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
                "targets": [4, 5, 7, 8, 9],
                "width": "5%"
            },
            {
                "targets": [23],
                "width": "5%"
            },
        ]
});

var otablaErroresQuejas = $("#tablaErroresQueja").DataTable({
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

$('#tablaQuejas tbody').on('click', 'button.btn-modificar', function () {
    // Obtener la fila correspondiente al botón
    let fila = otablaConsultas.row($(this).closest('tr')).data();
    let tr = $(this).closest('tr');
    let row = otablaConsultas.row(tr).index();

    $("#indice-fila").val(row)
    cargarModificarQueja(fila);
    $("#modalAgregarQueja").modal("show")
});

$('#tablaQuejas tbody').on('click', '.btn-eliminar', function () {
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

    cargarErrores(fila.quejasNoMes, fila.quejasFolio, fila.quejasFecRecepcion, fila.errors)
});

$("#modalListadoErrores").on('hidden.bs.modal', function () {
    otablaListadoErrores.clear().draw();
});

$("#modalErroresQueja").on('hidden.bs.modal', function () {
    otablaErroresQuejas.clear().draw();
    reiniciarSelect("QuejasNoTrim_Ver");
    $("#QuejasFolio_Ver").val('');
    $("#QuejasFecRecepcion_Ver").val('');
});

//Enviar datos al controlador
$("#btn_subirQuejas").on("click", function () {
    var tabla = $('#tablaQuejas').DataTable();

    // Obtener los datos de la tabla
    var datos = tabla.rows().data();

    // Array para almacenar los datos como objetos
    var objetoDatos = [];

    // Iterar sobre los datos y construir el array de objetos
    datos.each(function (index, fila) {
        if (index.QuejasSexo == "null") index.QuejasSexo = null;
        if (index.QuejasEdad == "null") index.QuejasEdad = null;
        if (index.QuejasPenalizacion == "null") index.QuejasPenalizacion = null;
        if (index.QuejasRespuesta == "null") index.QuejasRespuesta = null;
        if (index.QuejasNumPenal == "null") index.QuejasNumPenal = null;
        if (index.QuejasFecResolucion == "null") index.QuejasFecResolucion = null;
        if (index.QuejasFecNotificacion == "null") index.QuejasFecNotificacion = null;
        objetoDatos.push(index);
    });

    // Mostrar el array de objetos en la consola
    ////console.log(objetoDatos);

    $.ajax({
        url: urlEnviarQueja,
        method: "POST",
        cache: false,
        data: {
            anio: getAnioActual(),
            quejas: objetoDatos,
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
                }
                else if (respuesta.code >= 500) {
                    alerta("error", "El servicio de Redeco no funciona. Intentelo mas tarde")
                }

                else {
                    alerta("error", textoScript.mErrorEnvioQuejas, "")
                    otablaConsultas.clear().draw()
                    ConstruirTablaElementos(otablaListadoErrores, respuesta.errores);

                    //$("#btn_ListadoErrores").click();
                    $("#modalListadoErrores").modal("show");
                }
            } else {
                alerta("error", textoScript.mErrorEnvioQuejas2)
                alerta("error", "El servicio de Redeco no funciona. Intentelo mas tarde")
            }

        },
        error: function (respuesta) {
            alerta("error", textoScript.mErrorPeticion)
        }
    })
})

//Modificar información del registro seleccionado
$("#btn_corregirQuejas").on("click", function () {
    var tabla = $('#tablaListadoErrores').DataTable();
    var tabla2 = $('#tablaQuejas').DataTable();

    // Obtener los datos de la tabla
    var datos = tabla.rows().data();

    // Array para almacenar los datos como objetos
    var arrayObjetos = [];

    // Iterar sobre los datos y construir el array de objetos
    datos.each(function (index, fila) {
        arrayObjetos.push(index);
    });

    // Mostrar el array de objetos en la consola
    //console.log(arrayObjetos);

    tabla2.clear().draw();

    $.each(arrayObjetos, function (index, queja) {
        var obj = capitalizeObjectKeys(queja);
        //console.log("obj",obj)
        //obj.QuejasCP = convertirCP(obj.QuejasCP);
        obj.QuejasFecRecepcion = transformarFecha_ddMMaaaa(obj.QuejasFecRecepcion);
        obj.QuejasFecResolucion = transformarFecha_ddMMaaaa(obj.QuejasFecResolucion);
        obj.QuejasFecNotificacion = transformarFecha_ddMMaaaa(obj.QuejasFecNotificacion);

        //console.log("obj2", obj)
        tabla2.row.add(obj).draw();
    });

    $("#modalListadoErrores_close").click();
})

//Agregar regsitro en tabla

function construyeObjetoInfo() {
    return new Promise(function (resolve, reject) {
        let objetoSeleccionado = tipoLocalidad.find(function (objeto) {
            return objeto.id === $("#QuejasColId").val();
        });

        var queja = {
            "QuejasNum": $("#QuejasNum").val(),
            "QuejasNoMes": $("#QuejasNoMes").val(),
            "QuejasFolio": $("#QuejasFolio").val().trim(),
            "QuejasFecRecepcion": $("#QuejasFecRecepcion").val(),
            "QuejasMedio": $("#QuejasMedio").val(),
            "QuejasNivelAT": $("#QuejasNivelAT").val(),
            "QuejasProducto": $("#QuejasProducto").val(),
            "QuejasCausa": $("#QuejasCausa").val(),
            "QuejasEstatus": $('input[name=QuejasEstatus]:checked').val(),
            "QuejasTipoPersona": $('input[name=QuejasTipoPersona]:checked').val(),
            "QuejasPORI": $('input[name=QuejasPORI]:checked').val(),
            "QuejasEstados": $("#QuejasEstados").val(),
            "QuejasMunId": $("#QuejasMunId").val(),
            "QuejasLocId": validaStringNumerico(objetoSeleccionado?.valor),
            "QuejasColId": $("#QuejasColId").val(),
            "QuejasCP": $("#QuejasCP").val(),
            "QuejasSexo": $('input[name=QuejasSexo]:checked').val(),
            "QuejasEdad": $("#QuejasEdad").val(),
            "QuejasFecResolucion": $("#QuejasFecResolucion").val(),
            "QuejasFecNotificacion": $("#QuejasFecNotificacion").val(),
            "QuejasRespuesta": $("#QuejasRespuesta").val(),
            "QuejasPenalizacion": $("#QuejasPenalizacion").val(),
            "QuejasNumPenal": $("#QuejasNumPenal").val(),
            "QuejasDenominacion": $("#institucion-clave").val(),
            "QuejasSector": $("#sector").val()
        };

        // Resolve la promesa con el objeto info
        resolve(queja);
    });
}


$("#btn_agregarQueja").on('click', function () {
    construyeObjetoInfo().then(function (queja) {
        ////console.log('info', info)
        let validado = validarInformacion(queja);
        if (validado.hayErrores) {
            cargarErrores(queja.QuejasNoMes, queja.QuejasFolio, queja.QuejasFecRecepcion, validado.listaErrores);
        } else {
            queja.QuejasFecRecepcion = transformarFecha_ddMMaaaa(queja.QuejasFecRecepcion)
            queja.QuejasFecResolucion = transformarFecha_ddMMaaaa(queja.QuejasFecResolucion)
            queja.QuejasFecNotificacion = transformarFecha_ddMMaaaa(queja.QuejasFecNotificacion)
            let datos = validarDatosNull(queja);
            ////console.log("info p", datos)

            // Agregar la nueva fila
            otablaConsultas.row.add(datos).draw();
            limpiarAgregarQueja();
        }
    }).catch(function (error) {
        alerta("error", "Ocurrió un error al capturar la queja", "Inténtelo nuevamente");
        console.error('Error al construir el objeto info:', error);
    });
});

$("#btn_actualizarQueja").on("click", function () {
    construyeObjetoInfo().then(function (queja) {
        //console.log('info', queja)
        let validado = validarInformacion(queja);
        if (validado.hayErrores) {
            cargarErrores(queja.QuejasNoMes, queja.QuejasFolio, queja.QuejasFecRecepcion, validado.listaErrores);
        } else {
            queja.QuejasFecRecepcion = transformarFecha_ddMMaaaa(queja.QuejasFecRecepcion)
            queja.QuejasFecResolucion = transformarFecha_ddMMaaaa(queja.QuejasFecResolucion)
            queja.QuejasFecNotificacion = transformarFecha_ddMMaaaa(queja.QuejasFecNotificacion)
            let newData = validarDatosNull(queja);
            //console.log("info p", newData)
            // Agregar la nueva fila
            //otablaConsultas.data(datos).draw();
            //row.data(newData).draw(false)
            let rowIndex = $("#indice-fila").val();
            otablaConsultas.row(rowIndex).data(newData).draw();
            limpiarAgregarQueja();
            $("#btn_agregarQueja").show();
            $("#btn_actualizarQueja").hide();
        }
    }).catch(function (error) {
        alerta("error", "Ocurrió un error al capturar la queja", "Inténtelo nuevamente");
        console.error('Error al construir el objeto info:', error);
    });
})

//Select

$("#QuejasProducto").on("change", function () {
    let idProducto = $("#QuejasProducto").val();
    if (!esCadenaVaciaOBlanca(idProducto)) {
        $.ajax({
            url: urlConsultarCausas,
            method: "POST",
            cache: false,
            data: {
                idProducto: idProducto,
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
                    llenarSelect("QuejasCausa", respuesta.data);
                } else {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: "Something went wrong!",
                    });
                }
            },
            error: function (respuesta) {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Something went wrong!",
                });
            }
        })
    }
})

$("#QuejasEstados").on("change", function () {
    let id = $("#QuejasEstados").val();
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
                    llenarSelect("QuejasMunId", respuesta.data);
                } else {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: "Something went wrong!",
                    });
                }
            },
            error: function (respuesta) {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Something went wrong!",
                });
            }
        })
    }
})

$("#QuejasMunId").on("change", function () {
    let idE = $("#QuejasEstados").val();
    let idM = $("#QuejasMunId").val();
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
                    llenarSelect("QuejasCP", respuesta.data);
                } else {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: "Something went wrong!",
                    });
                }
            },
            error: function (respuesta) {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Something went wrong!",
                });
            }
        })
    }
})

$("#QuejasCP").on("change", function () {
    let idE = $("#QuejasEstados").val();
    let idM = $("#QuejasMunId").val();
    let idC = $("#QuejasCP").val();
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
                    llenarSelect("QuejasColId", respuesta.data);
                    tipoLocalidad = respuesta.data;

                } else {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: "Something went wrong!",
                    });
                }
            },
            error: function (respuesta) {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Something went wrong!",
                });
            }
        })
    }
})

$('input[name=QuejasTipoPersona]').on('change', function () {
    if ($('input[name=QuejasTipoPersona]:checked').val() === "2") {
        $('input[name=QuejasSexo]').prop('disabled', true);
        $('input[name=QuejasSexo][value=H]').prop('checked', true);
    } else {
        $('input[name=QuejasSexo]').prop('disabled', false);
    }
});

function cargarErrores(QuejasNoMes, QuejasFolio, QuejasFecRecepcion, errors) {
    cambiarValorSelect("QuejasNoTrim_Ver", QuejasNoMes);
    mostrarAnioActual("anioActual_Ver")
    $("#QuejasFolio_Ver").val(QuejasFolio);
    $("#QuejasFecRecepcion_Ver").val(transformarFecha_aaaaMMdd(QuejasFecRecepcion));

    $.each(errors, function (index, dato) {
        otablaErroresQuejas.row.add({ descripcion: dato }).draw();
    });
    //$("#btn_ErroresQueja").click()
    $("#modalErroresQueja").modal("show");
}

function limpiarAgregarQueja() {
    $("#modalAgregarQueja").modal("hide");
    reiniciarSelect("QuejasNoMes")
    $("#QuejasFolio").val("");
    $("#QuejasFecRecepcion").val("");
    reiniciarSelect("QuejasMedio");
    reiniciarSelect("QuejasNivelAT");
    reiniciarSelect("QuejasProducto");
    reiniciarSelect("QuejasCausa");
    $('input[name=QuejasEstatus][value=1]').prop('checked', true);
    $('input[name=QuejasTipoPersona][value=1]').prop('checked', true);
    $('input[name=QuejasPORI][value=NO]').prop('checked', true);
    reiniciarSelect("QuejasEstados");
    reiniciarSelect("QuejasMunId");
    reiniciarSelect("QuejasLocId");
    reiniciarSelect("QuejasColId");
    reiniciarSelect("QuejasCP");
    $('input[name=QuejasSexo][value=H]').prop('checked', true);
    $("#QuejasEdad").val("0");
    $("#QuejasFecResolucion").val("");
    $("#QuejasFecNotificacion").val("");
    reiniciarSelect("QuejasRespuesta")
    reiniciarSelect("QuejasPenalizacion")
    $("#QuejasNumPenal").val("0");
    $("#btn_agregarQueja").show();
    $("#btn_actualizarQueja").hide();
};

function cargarModificarQueja(fila) {
    cambiarValorSelect("QuejasNoMes", fila.QuejasNoMes);
    $("#QuejasFolio").val(fila.QuejasFolio);
    $("#QuejasFecRecepcion").val(transformarFecha_aaaaMMdd(fila.QuejasFecRecepcion));
    cambiarValorSelect("QuejasMedio", fila.QuejasMedio);
    cambiarValorSelect("QuejasNivelAT", fila.QuejasNivelAT);
    cambiarValorSelect("QuejasProducto", fila.QuejasProducto);
    setTimeout(function () {
        cambiarValorSelect("QuejasCausa", fila.QuejasCausa);
    }, 1000);
    $(`input[name=QuejasEstatus][value=${fila.QuejasEstatus}]`).prop('checked', true);
    $(`input[name=QuejasTipoPersona][value=${fila.QuejasTipoPersona}]`).prop('checked', true);
    $(`input[name=QuejasPORI][value = ${fila.QuejasPORI}]`).prop('checked', true);
    cambiarValorSelect("QuejasEstados", fila.QuejasEstados);
    setTimeout(function () {
        cambiarValorSelect("QuejasMunId", fila.QuejasMunId);
    }, 1000);
    setTimeout(function () {
        cambiarValorSelect("QuejasCP", fila.QuejasCP);
    }, 1500);
    setTimeout(function () {
        cambiarValorSelect("QuejasColId", fila.QuejasColId);
        cambiarValorSelect("QuejasLocId", fila.QuejasLocId);
    }, 2000);
    if (fila.QuejasSexo != "null" || fila.QuejasSexo != "") {
        $(`input[name=QuejasSexo][value=${fila.QuejasSexo}]`).prop('checked', true);
    }
    $("#QuejasEdad").val(fila.QuejasEdad);
    $("#QuejasFecResolucion").val(transformarFecha_aaaaMMdd(fila.QuejasFecResolucion));
    $("#QuejasFecNotificacion").val(transformarFecha_aaaaMMdd(fila.QuejasFecNotificacion));
    cambiarValorSelect("QuejasRespuesta", fila.QuejasRespuesta);
    cambiarValorSelect("QuejasPenalizacion", fila.QuejasPenalizacion);
    $("#QuejasNumPenal").val(fila.QuejasNumPenal);
    $("#btn_agregarQueja").hide();
    $("#btn_actualizarQueja").show();


}

function validarInformacion(queja) {
    //console.log("queja")
    //console.log(queja)
    //obtenerPeriodoReportar()
    let errores = [];
    //Que no sean datos vacíos
    if (esCadenaVaciaOBlanca(queja.QuejasFolio)) errores.push("Debe agregar un folio");
    if (esCadenaVaciaOBlanca(queja.QuejasFecRecepcion)) errores.push("Debe agregar una fecha de recepción");
    if (esCadenaVaciaOBlanca(queja.QuejasMedio)) errores.push("Debe seleccionar un medio de recepción");
    if (esCadenaVaciaOBlanca(queja.QuejasNivelAT)) errores.push("Debe seleccionar un nivel de atención");
    if (esCadenaVaciaOBlanca(queja.QuejasProducto)) errores.push("Debe seleccionar un producto");
    if (esCadenaVaciaOBlanca(queja.QuejasCausa)) errores.push("Debe seleccionar una causa");
    if (esCadenaVaciaOBlanca(queja.QuejasEstados)) errores.push("Debe seleccionar una entidad federativa");
    if (esCadenaVaciaOBlanca(queja.QuejasMunId)) errores.push("Debe seleccionar un municipio");
    if (esCadenaVaciaOBlanca(queja.QuejasColId)) errores.push("Debe seleccionar una colonia");
    if (esCadenaVaciaOBlanca(queja.QuejasCP)) errores.push("Debe seleccionar un código postal");
    if (queja.QuejasTipoPersona == 1 && esEnteroValidoNiCero(queja.QuejasEdad)) errores.push("Debe ingresar una edad válida");
    //if (esEnteroValido(queja.QuejasNumPenal)) errores.push("Debe ingresar un número de penalización válido");
    if (queja.QuejasEstatus == 2) {
        if (esCadenaVaciaOBlanca(queja.QuejasFecResolucion)) errores.push("Debe agregar una fecha de resolución");
        if (esCadenaVaciaOBlanca(queja.QuejasFecNotificacion)) errores.push("Debe agregar una fecha de notificación");
        if (esCadenaVaciaOBlanca(queja.QuejasRespuesta)) errores.push("Debe seleccionar una respuesta");

        //Validar fechas
        if (valida2Fechas(queja.QuejasFecRecepcion, queja.QuejasFecResolucion)) errores.push("La fecha de resolución no puede ser menor que la fecha de recepción");
        if (valida2Fechas(queja.QuejasFecResolucion, queja.QuejasFecNotificacion)) errores.push("La fecha de notificación no puede ser menor que la fecha de resolución");
    }


    //if (esCadenaVaciaOBlanca(queja.QuejasPenalizacion)) errores.push("Debe seleccionar un tipo de penalización");

    //Validar fechas
    if (!validaFechaPeriodo(queja.QuejasFecRecepcion, queja.QuejasNoMes)) errores.push("La fecha de recepción debe pertenecer al periodo a reportar");


    let _hayErrores = errores.length > 0;
    return {
        hayErrores: _hayErrores,
        listaErrores: errores
    }

}

function validarDatosNull(info) {

    if (info.QuejasTipoPersona == 2) {
        info.QuejasSexo = "null";
        info.QuejasEdad = "null";
    }
    if (info.QuejasEstatus == 1) {
        info.QuejasPenalizacion = "null";
        info.QuejasRespuesta = "null";
        info.QuejasNumPenal = "null";
        info.QuejasFecResolucion = "null";
        info.QuejasFecNotificacion = "null";
    }

    if (info.QuejasNumPenal == '' || info.QuejasNumPenal == 0) info.QuejasNumPenal = "null";
    if (info.QuejasPenalizacion == '') info.QuejasPenalizacion = "null";

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
                alerta("success",textoScript.mExitoArchivoProcesado)
                //otablaConsultas.ajax.reload();
                let data = respuesta.data;

                let objetoDatos = [];
                // Iterar sobre los datos y construir el array de objetos
                data.forEach(function (index, fila) {
                    index = capitalizeKeys(index);
                    index.QuejasDenominacion = $("#institucion-clave").val();
                    index.QuejasSector = $("#sector").val();
                    objetoDatos.push(index);
                });

                ConstruirTablaElementos(otablaConsultas, objetoDatos);
                limpiarModalSubida()
                $("#btn_subirQuejas").prop("disabled", false);
            } else {
                alerta("error",textoScript.mErrorArchivoProcesado)
            }
        },
        error: function (respuesta) {
            alerta("error",textoScript.error)
            console.error('Ocurrio un error:',respuesta)
        }
    })
}

function limpiarModalSubida() {
    $('#modalSubirArchivo').modal('hide');
    $('#inputname').val();
}












