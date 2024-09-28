var otablaEmpresasNoRegistradas = jQuery("#tablaEmpresasNoRegistradas").DataTable({
    "ajax": {
        "url": urlConsultarEmpresasNoRegistradas,
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
            { "data": "idEmpresa", "name": "idEmpresa", "autowidth": true },
            { "data": "nombreEmpresa", "name": "nombreEmpresa", "autowidth": true },
            {
                "data": null, render: function () {
                    return `<button type="button" class="btn-mostrar btn btn-sm btn-block btn-primary text-white text-center waves-effect waves-light" style="font-size:small">${textoScript.mostrar}</button>`;
                }
            },
            {
                "data": null, render: function () {
                    return `<a class="btn-activar btn btn-sm btn-block btn-danger text-center text-white waves-effect waves-light" style="font-size:small">${textoScript.btnHabilitarEmpresa}</a>`;
                }
            }
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
                "width": "70%"
            },
            {
                "targets": [2, 3],
                "searchable": false,
                "orderable": false,
                "width": "15%"
            }
        ]
});

var otablaEmpresasActivas = jQuery("#tablaEmpresasActivas").DataTable({
    "ajax": {
        "url": urlConsultarEmpresasActivas,
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
            { "data": "idEmpresa", "name": "idEmpresa", "autowidth": true },
            { "data": "nombreEmpresa", "name": "nombreEmpresa", "autowidth": true },
            {
                "data": null, render: function (data, type, row) {
                    if (data.estatus) return textoScript.empresaActiva;
                    else return textoScript.empresaBloqueada;
                }
            },
            {
                "data": null, render: function () {
                    return `<button type="button" class="btn-mostrar btn btn-sm btn-block btn-primary text-white text-center waves-effect waves-light" style="font-size:small">${textoScript.mostrar}</button>`;
                }
            },
            {
                "data": null, render: function (data, type, row) {
                    if (data.estatus) {
                        return `<a class="btn-activar btn btn-sm btn-block btn-danger" style="font-size:small">${textoScript.btnDesactivarEmpresa}</a>`;
                    } else {
                        return `<a class="btn-activar btn btn-sm btn-block btn-warning" style="font-size:small">${textoScript.btnActivarEmpresa}</a>`;
                    }
                }
            }
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
                "width": "55%"
            },
            {
                "targets": [2, 3, 4],
                "searchable": false,
                "orderable": false,
                "width": "15%"
            }
        ]
});


$('#tablaEmpresasNoRegistradas tbody').on('click', 'button.btn-mostrar', function () {
    // Obtener la fila correspondiente al botón
    var fila = otablaEmpresasNoRegistradas.row($(this).closest('tr')).data();
    $.ajax({
        url: urlConsultarInfoEmpresa,
        method: "POST",
        cache: false,
        data: {
            idEmpresa: fila.idEmpresa,
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
        },
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        success: function (respuesta) {
            //console.log(respuesta)
            if (respuesta.success) {
                cargarInfoEmpresa(respuesta.data)
            } else {
                Swal.fire({
                    icon: "error",
                    title: textoScript.mErrorEnvioQuejas,
                    showConfirmButton: false,
                    timer: 2000
                })
            }
        },
        error: function (respuesta) {
            console.error('Ocurrio un error:', respuesta)
            Swal.fire({
                icon: "error",
                title: textoScript.ErrorGenerico,
                showConfirmButton: false,
                timer: 2000
            })
            //toastr.clear();
            //toastr.error(textoScript.errorSimple);
        }
    })
    //cargarModificarQueja(fila);
    //$("#btn_abrirModalQueja").click()
});

$('#tablaEmpresasActivas tbody').on('click', 'button.btn-mostrar', function () {
    // Obtener la fila correspondiente al botón
    var fila = otablaEmpresasActivas.row($(this).closest('tr')).data();
    //console.log(fila)
    $.ajax({
        url: urlConsultarInfoEmpresa,
        method: "POST",
        cache: false,
        data: {
            idEmpresa: fila.idEmpresa,
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
                cargarInfoEmpresa(respuesta.data)
            } else {
                Swal.fire({
                    icon: "error",
                    title: textoScript.mErrorEnvioQuejas,
                    showConfirmButton: false,
                    timer: 2000
                })
            }
        },
        error: function (respuesta) {
            console.error('Ocurrio un error:', respuesta)
            Swal.fire({
                icon: "error",
                title: textoScript.ErrorGenerico,
                showConfirmButton: false,
                timer: 2000
            })
            //toastr.clear();
            //toastr.error(textoScript.errorSimple);
        }
    })
    //cargarModificarQueja(fila);
    //$("#btn_abrirModalQueja").click()
});

$('#tablaEmpresasNoRegistradas tbody').on('click', '.btn-activar', function () {
    Swal.fire({
        title: textoScript.mActivarEmpresa,
        showDenyButton: true,
        confirmButtonText: textoScript.si,
        denyButtonText: textoScript.no
    }).then((result) => {

        // Obtener la fila correspondiente al botón
        var fila = otablaEmpresasNoRegistradas.row($(this).closest('tr')).data();

        $.ajax({
            url: urlHabilitarEmpresa,
            method: "POST",
            cache: false,
            data: {
                idEmpresa: fila.idEmpresa,
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
                        title: textoScript.mExitoTitulo,
                        text: textoScript.mEmpresaHabilitada,
                        showConfirmButton: false,
                        timer: 2500
                    })
                    setTimeout(function () {
                        location.reload();
                    }, 3000); 
                    
                } else {
                    Swal.fire({
                        icon: "error",
                        title: textoScript.mErrorTitulo,
                        text: textoScript.mErrorEmpresaHabilitada,
                        showConfirmButton: false,
                        timer: 2000
                    })
                }
            },
            error: function (respuesta) {
                Swal.fire({
                    icon: "error",
                    title: textoScript.ErrorGenerico,
                    showConfirmButton: false,
                    timer: 2000
                })
            }
        })
    });
});




$("#modalDetalles").on("hidden.bs.modal", function () {
    limpiarInfoEmpresa()
})

function cargarInfoEmpresa(fila) {
    //console.log(fila)
    cambiarValorSelect("Estado", fila.dirEstado);
    $("#Sector").val(fila.sector);
    $("#RFC").val(fila.rfc);
    $("#Nombre").val(fila.nombre);
    $("#NomCorto").val(fila.nomCorto);
    $("#DirCalle").val(fila.dirCalle);
    $("#DirColonia").val(fila.dirColonia);
    $("#DirNumInt").val(fila.dirNumInt);
    $("#DirNumExt").val(fila.dirNumExt);
    $("#DirCP").val(fila.dirCP);
    $("#DirMunicipio").val(fila.dirMunicipio);
    $("#CorreoContacto").val(fila.correoContacto);
    $("#TelefonoContacto").val(fila.telefonoContacto);
    $("#PersonaContacto").val(fila.personaContacto);

    $("#modalDetalles").modal("show");
}

function limpiarInfoEmpresa() {
    reiniciarSelect("DirEstado");
    $("#CASFIM").val("");
    $("#RFC").val("");
    $("#Nombre").val("");
    $("#NomCorto").val("");
    $("#DirCalle").val("");
    $("#DirColonia").val("");
    $("#DirNumInt").val("");
    $("#DirNumExt").val("");
    $("#DirCP").val("");
    $("#DirMunicipio").val("");
    $("#CorreoContacto").val("");
    $("#TelefonoContacto").val("");
    $("#PersonaContacto").val("");
}