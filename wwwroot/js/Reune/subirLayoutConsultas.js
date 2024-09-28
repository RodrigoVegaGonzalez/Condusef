import { CONSULTAS_FICTICIAS, VISTAS } from "../constantes.js";

//Variables
var tipoLocalidad = [];

var tabla = null;

//Ready
$(document).ready(function () {
    mostrarAnioActual("anioActual");
    //$(".selectpicker").select2();
})

//Tablas
var otablaConsultas = $("#tablaConsultas").DataTable({
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
            { "data": "NumConsultas", "name": "NumConsultas", "autowidth": true },
            { "data": "ConsultasFolio", "name": "ConsultasFolio", "autowidth": true },
            { "data": "ConsultasEstatusCon", "name": "ConsultasEstatusCon", "autowidth": true },
            { "data": "ConsultasFecRecepcion", "name": "ConsultasFecRecepcion", "autowidth": true },
            { "data": "MediosId", "name": "MediosId", "autowidth": true },
            { "data": "ConsultascatnivelatenId", "name": "ConsultascatnivelatenId", "autowidth": true },
            { "data": "Producto", "name": "Producto", "autowidth": true },
            { "data": "CausaId", "name": "CausaId", "autowidth": true },
            { "data": "InstitucionClave", "name": "InstitucionClave", "autowidth": true },
            { "data": "Sector", "name": "Sector", "autowidth": true },
            { "data": "ConsultasTrim", "name": "ConsultasTrim", "autowidth": true },
            { "data": "ConsultasPori", "name": "ConsultasPori", "autowidth": true },
            { "data": "ConsultasFecAten", "name": "ConsultasFecAten", "autowidth": true },
            { "data": "EstadosId", "name": "EstadosId", "autowidth": true },
            { "data": "ConsultasMpioId", "name": "ConsultasMpioId", "autowidth": true },
            { "data": "ConsultasColId", "name": "ConsultasColId", "autowidth": true },
            { "data": "ConsultasCP", "name": "ConsultasCP", "autowidth": true },
            { "data": "ConsultasLocId", "name": "ConsultasLocId", "autowidth": true },
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
                "targets": [8, 9, 10, 11, 12, 13, 14, 15, 16, 17],
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
                "targets": [18, 19],
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
            { "data": "numConsultas", "name": "NumConsultas", "autowidth": true },
            { "data": "consultasFolio", "name": "ConsultasFolio", "autowidth": true },
            { "data": "consultasEstatusCon", "name": "ConsultasEstatusCon", "autowidth": true },
            { "data": "consultasFecRecepcion", "name": "ConsultasFecRecepcion", "autowidth": true },
            { "data": "mediosId", "name": "MediosId", "autowidth": true },
            { "data": "consultascatnivelatenId", "name": "ConsultascatnivelatenId", "autowidth": true },
            { "data": "producto", "name": "Producto", "autowidth": true },
            { "data": "causaId", "name": "CausaId", "autowidth": true },
            { "data": "institucionClave", "name": "InstitucionClave", "autowidth": true },
            { "data": "sector", "name": "Sector", "autowidth": true },
            { "data": "consultasTrim", "name": "ConsultasTrim", "autowidth": true },
            { "data": "consultasPori", "name": "ConsultasPori", "autowidth": true },
            { "data": "consultasFecAten", "name": "ConsultasFecAten", "autowidth": true },
            { "data": "estadosId", "name": "EstadosId", "autowidth": true },
            { "data": "consultasMpioId", "name": "ConsultasMpioId", "autowidth": true },
            { "data": "consultasColId", "name": "ConsultasColId", "autowidth": true },
            { "data": "consultasCP", "name": "ConsultasCP", "autowidth": true },
            { "data": "consultasLocId", "name": "ConsultasLocId", "autowidth": true },
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
                "targets": [8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 19],
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
                "targets": [18],
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

$('#tablaConsultas tbody').on('click', 'button.btn-modificar', function () {
    // Obtener la fila correspondiente al botón
    var fila = otablaConsultas.row($(this).closest('tr')).data();
    //tabla = otablaConsultas.row($(this).closest('tr'));
    let tr = $(this).closest('tr');
    let row = otablaConsultas.row(tr).index();

    $("#indice-fila").val(row)
    cargarModificarInfo(fila);
    $("#modalAgregar").modal("show");
});

$('#tablaConsultas tbody').on('click', '.btn-eliminar', function () {
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

    cargarErrores(fila.consultasTrim, fila.consultasFolio, fila.consultasFecRecepcion, fila.errors)
});


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

$("#btn_subirDatos").on("click", function () {
    var tabla = $('#tablaConsultas').DataTable();

    // Obtener los datos de la tabla
    var datos = tabla.rows().data();

    // Array para almacenar los datos como objetos
    var objetoDatos = [];

    // Iterar sobre los datos y construir el array de objetos
    datos.each(function (index, fila) {
        if (index.ConsultascatnivelatenId == "null") index.ConsultascatnivelatenId = null;
        if (index.ConsultasFecAten == "null") index.ConsultasFecAten = null;
        if (index.ConsultasCP == "null") index.ConsultasCP = null;
        if (index.ConsultasColId == "null") index.ConsultasColId = null;
        if (index.ConsultasLocId == "null") index.ConsultasLocId = null;

        objetoDatos.push(index);
    });

    // Mostrar el array de objetos en la consola
    ////console.log(objetoDatos);
    if (objetoDatos.length == 0) {
        alerta("error", "Debe agregar una o más consultas")
    }
    else {
        $.ajax({
            url: urlEnviarDatos,
            method: "POST",
            cache: false,
            data: {
                anio: getAnioActual(),
                consultas: objetoDatos,
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
                    //console.log(respuesta)
                    if (respuesta.code === 200) {
                        alertaConRecarga("success", textoScript.mExitoEnvioQuejas, "")
                    } else if (respuesta.code >= 500) {
                        alerta("error", "El servicio de Reune no funciona. Intentelo mas tarde")
                    }
                    else {
                        alerta("error", textoScript.mErrorEnvioQuejas, "")
                        //console.log('Listado errr')
                        //console.log(respuesta.errores)
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
    var tabla2 = $('#tablaConsultas').DataTable();

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
        //console.log(obj)
        obj.ConsultasCP = convertirCP(obj.ConsultasCP);
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
            ConsultasFecAten: $("#ConsultasFecAten").val(),
            InstitucionClave: $("#institucion-clave").val(),
            Sector: $("#sector").val(),
            NumConsultas: $("#NumConsultas").val(),
            ConsultasTrim: $("#trimestre-select").val(),
            ConsultasFolio: $("#ConsultasFolio").val().trim(),
            ConsultasFecRecepcion: $("#ConsultasFecRecepcion").val(),
            "MediosId": $("#medios-select").val(),
            "ConsultascatnivelatenId": $("#niveles-select").val(),
            "Producto": $("#productos-select").val(),
            "CausaId": $("#causas-select").val(),
            "ConsultasEstatusCon": $('input[name=estatus-radio]:checked').val(),
            "ConsultasPori": $('input[name=pori-radio]:checked').val(),
            "EstadosId": $("#estados-select").val(),
            "ConsultasMpioId": $("#municipios-select").val(),
            "ConsultasLocId": validaStringNumerico(objetoSeleccionado?.valor),
            "ConsultasColId": $("#colonias-select").val(),
            "ConsultasCP": $("#cp-select").val()
        }

        // Resolve la promesa con el objeto info
        resolve(info);
    });
}

//Agregar registro a la tabla
$("#btn_agregar").on('click', function () {
    construyeObjetoInfo().then(function (info) {
        ////console.log('info', info)
        let validado = validarInformacion(info);
        if (validado.hayErrores) {
            cargarErrores(info.ConsultasTrim, info.ConsultasFolio, info.ConsultasFecRecepcion, validado.listaErrores);
        } else {
            info.ConsultasFecAten = transformarFecha_ddMMaaaa($("#ConsultasFecAten").val())
            info.ConsultasFecRecepcion = transformarFecha_ddMMaaaa(info.ConsultasFecRecepcion)
            let datos = validarDatosNull(info);
            ////console.log("info p", datos)

            // Agregar la nueva fila
            otablaConsultas.row.add(datos).draw();
            limpiarAgregarModal();
        }
    }).catch(function (error) {
        alerta("error", "Ocurrió un error al capturar la consulta", "Inténtelo nuevamente");
        console.error('Error al construir el objeto info:', error);
    });
});

$("#btn_actualizar").on("click", function () {
    construyeObjetoInfo().then(function (info) {
        ////console.log('info', info)
        let validado = validarInformacion(info);
        if (validado.hayErrores) {
            cargarErrores(info.ConsultasTrim, info.ConsultasFolio, info.ConsultasFecRecepcion, validado.listaErrores);
        } else {
            info.ConsultasFecAten = transformarFecha_ddMMaaaa($("#ConsultasFecAten").val())
            info.ConsultasFecRecepcion = transformarFecha_ddMMaaaa(info.ConsultasFecRecepcion)
            let newData = validarDatosNull(info);

            let rowIndex = $("#indice-fila").val();
            otablaConsultas.row(rowIndex).data(newData).draw();

            limpiarAgregarModal();
            $("#btn_agregar").show();
            $("#btn_actualizar").hide();
        }
    }).catch(function (error) {
        alerta("error", "Ocurrió un error al capturar la consulta", "Inténtelo nuevamente");
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
    reiniciarSelect("trimestre-select")
    $("#ConsultasFolio").val("");
    $("#NumConsultas").val("");
    $("#ConsultasFecAten").val("");
    $("#ConsultasFecRecepcion").val("");
    reiniciarSelect("medios-select");
    reiniciarSelect("niveles-select");
    reiniciarSelect("productos-select");
    reiniciarSelect("causas-select");
    $('input[name=estatus-radio][value=1]').prop('checked', true);
    $('input[name=pori-radio][value=NO]').prop('checked', true);
    reiniciarSelect("estados-select");
    reiniciarSelect("municipios-select");
    reiniciarSelect("colonias-select");
    reiniciarSelect("cp-select");

    $("#btn_agregar").show();
    $("#btn_actualizar").hide();
};

function cargarModificarInfo(fila) {
    //console.log('filas', fila)
    cambiarValorSelect("trimestre-select", fila.ConsultasTrim);
    $("#NumConsultas").val(fila.NumConsultas);
    $("#ConsultasFolio").val(fila.ConsultasFolio);
    $("#ConsultasFecRecepcion").val(transformarFecha_aaaaMMdd(fila.ConsultasFecRecepcion));
    $("#ConsultasFecAten").val(transformarFecha_aaaaMMdd(fila.ConsultasFecAten));
    cambiarValorSelect("medios-select", fila.MediosId);
    cambiarValorSelect("niveles-select", fila.ConsultascatnivelatenId);
    cambiarValorSelect("productos-select", fila.Producto);
    setTimeout(function () {
        cambiarValorSelect("causas-select", fila.CausaId);
    }, 1000);

    $(`input[name=estatus-radio][value=${fila.ConsultasEstatusCon}]`).prop('checked', true);
    $(`input[name=pori-select][value = ${fila.ConsultasPori}]`).prop('checked', true);

    cambiarValorSelect("estados-select", fila.EstadosId);
    setTimeout(function () {
        cambiarValorSelect("municipios-select", fila.ConsultasMpioId);
    }, 1000);
    setTimeout(function () {
        cambiarValorSelect("cp-select", fila.ConsultasCP);
    }, 1500);
    setTimeout(function () {
        cambiarValorSelect("colonias-select", fila.ConsultasColId);
    }, 2000);
    $("#btn_agregar").hide();
    $("#btn_actualizar").show();
}

function validarInformacion(info) {
    //console.log("queja")
    //console.log(info)
    //obtenerPeriodoReportar()
    let errores = [];
    //Que no sean datos vacíos
    if (esCadenaVaciaOBlanca(info.ConsultasFolio)) errores.push("Debe agregar un folio");
    if (esCadenaVaciaOBlanca(info.ConsultasFecRecepcion)) errores.push("Debe agregar una fecha de recepción");
    if (esCadenaVaciaOBlanca(info.MediosId)) errores.push("Debe seleccionar un medio de recepción");
    //if (esCadenaVaciaOBlanca(info.ConsultascatnivelatenId)) errores.push("Debe seleccionar un nivel de atención");
    if (esCadenaVaciaOBlanca(info.Producto)) errores.push("Debe seleccionar un producto");
    if (esCadenaVaciaOBlanca(info.CausaId)) errores.push("Debe seleccionar una causa");
    if (esCadenaVaciaOBlanca(info.EstadosId)) errores.push("Debe seleccionar una entidad federativa");
    if (esCadenaVaciaOBlanca(info.ConsultasMpioId)) errores.push("Debe seleccionar un municipio");
    //if ((info.MediosId == 3 || info.MediosId == 5 || info.MediosId == 17) && esCadenaVaciaOBlanca(info.ConsultasColId)) errores.push("Debe seleccionar una colonia");
    if ((info.MediosId == 3 || info.MediosId == 5 || info.MediosId == 17) && esCadenaVaciaOBlanca(info.ConsultasCP)) errores.push("Debe seleccionar un código postal");
    if (esEnteroValidoNiCero(info.NumConsultas)) errores.push("Debe ingresar un número de consultas válida (mínimo 1)");
    if (info.ConsultasEstatusCon == 2 && esCadenaVaciaOBlanca(info.ConsultasFecAten)) {
        errores.push("Debe agregar una fecha de atención");
    } else {
        info.ConsultasFecAten = null;
    }

    if (info.ConsultasEstatusCon == 2 && esCadenaVaciaOBlanca(info.ConsultascatnivelatenId)) {
        errores.push("Debe seleccionar un nivel de atención");
    }

    //Validar fechas
    if (!validaFechaPeriodoReune(info.ConsultasFecRecepcion, info.ConsultasTrim)) errores.push("La fecha de recepción no puede ser mayor al periodo a reportar");

    if (info.ConsultasEstatusCon == 2) {
        if (valida2Fechas(info.ConsultasFecRecepcion, $("#ConsultasFecAten").val())) errores.push("La fecha de atención no puede ser menor que la fecha de recepción");
    }

    let _hayErrores = errores.length > 0;
    return {
        hayErrores: _hayErrores,
        listaErrores: errores
    }

}

function validarDatosNull(info) {
    if (info.ConsultasEstatusCon == 1) {
        info.ConsultascatnivelatenId = "null";
        info.ConsultasFecAten = "null";
    }

    //if (info.MediosId != 3 && info.MediosId != 5 && info.MediosId != 17) {
    //    info.ConsultasCP = "null";
    //    info.ConsultasColId = "null";
    //    info.ConsultasLocId = "null";
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
                    index.InstitucionClave = $("#institucion-clave").val();
                    index.Sector = $("#sector").val();
                    objetoDatos.push(index);
                });
                console.log("data",objetoDatos)
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








