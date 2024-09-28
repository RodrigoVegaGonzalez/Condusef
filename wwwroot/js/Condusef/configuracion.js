
$(document).ready(function () {
    $('.selectitems').select2();
});

var otablausuario = $("#tablaUsuarios").DataTable({
    ajax: {
        url: urlConsultaTablaUsuario,
        method: "POST",
        cache: false,
        data: function (d) {
            return "__RequestVerificationToken=" + $("input[name='__RequestVerificationToken']").val();
        }
    },
    buttons: [
        'copy', 'excel', 'pdf'
    ],
    layout: {
        topStart: 'buttons'
    },
    scrollCOllapse: true,
    select: false,
    "autoWidth": false,
    processing: true,
    language:
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
            previous: Text.siguiente,
            next: textoScript.anterior,
            last: textoScript.ultimo
        },
        select: {
            style: 'single'
        },
        emptyTable: textoScript.sinUsuarios,
        info: "",
        search: textoScript.buscar,
        infoFiltered: "",
        infoEmpty: "",
        lengthMenu: `${textoScript.mostrar} _MENU_`,
        searchPlaceHolder: `${textoScript.buscando}`,
        //processing: `<span style="width:100%;">${textoScript.obteniendoRegistros}</br><img src="${urlLoad}"/></span>`,
        zeroRecords: textoScript.zeroRecords
    },
    columns: [
        { "data": "idUsuario", "name": "idUsuario", "autowidth": true },
        { "data": "activo", "name": "activo", "autowidth": true },
        { "data": "usuario", "name": "usuario", "autowidth": true },
        { "data": "correoResponsable", "name": "correoResponsable", "autowidth": true },
        { "data": "rol", "name": "rol", "autowidth": true },
        {
            "data": null, "render": function (data, type, row) {
                return data.activo ? textoScript.activo : textoScript.desactivado
            }
        },
        {
            "data": null, "render": function (data, type, row) {
                return `<a class="boton-correo btn btn-sm btn-block btn-info" data-bs-toggle="modal" data-bs-target="#cambiarCorreoModal" style="font-size:small">${textoScript.cambiarCorreo}</a>`
            }
        },
        {
            "data": null, "render": function (data, type, row) {
                if (data.rol !== "Administrador") {
                    if (data.activo) return `<a class="boton-desactivar btn btn-sm btn-block btn-warning" style="font-size:small">${textoScript.desactivar}</a>`
                    else return `<a class="boton-desactivar btn btn-sm btn-block btn-success" style="font-size:small">${textoScript.activar}</a>`
                }
                else { return "<p></p>" }
            }
        },
        {
            "data": null, "render": function (data, type, row) {
                return `<a class="boton-cambiar btn btn-sm btn-block btn-secondary" data-bs-toggle="modal" data-bs-target="#cambiarPasswordModal" style="font-size:small">${textoScript.cambiarContrasena}</a>`
            }
        },
        {
            "data": null, "render": function (data, type, row) {
                return `<a class="boton-eliminar btn btn-sm btn-block btn-danger" style="font-size:small">${textoScript.eliminar}</a>`
            }
        },
    ],
    columnDefs: [
        { "width": "1%", targets: [0, 1], orderable: false, searchable: false, visible: false },
        { "width": "25%", targets: [2, 3], orderable: true, searchable: true },
        { "width": "10%", targets: [4, 6], orderable: false, searchable: false },
        { "width": "8%", targets: [5,7, 9], orderable: false, searchable: false },
        { "width": "15%", targets: [8], orderable: false, searchable: false },
    ],
    order: [[2, "asc"]]
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

function llenarTablaErrores(listado) {
    otablaErroress.clear().draw();
    $.each(listado, function (index, dato) {
        otablaErroress.row.add({ descripcion: dato }).draw();
    });

    $("#modalErrores").modal("show")
}

function changeArrow(element) {
    let expanded = $(element).attr('aria-expanded');
    let show = validaNullBool(expanded);
    if (show) {
        $(element).find('.fa-arrow-up').show();
        $(element).find('.fa-arrow-down').hide();
    }else {
        $(element).find('.fa-arrow-up').hide();
        $(element).find('.fa-arrow-down').show();
    }
}

function cambiarIconoPassword(icono,id) {
    var inputPassword = document.getElementById(id);
    var tipoInput = inputPassword.getAttribute("type");

    if (tipoInput === "password") {
        inputPassword.setAttribute("type", "text");
        icono.innerHTML = '<i class="fa-solid fa-eye-slash icon-blue"></i>';
    } else {
        inputPassword.setAttribute("type", "password");
        icono.innerHTML = '<i class="fa-solid fa-eye icon-blue"></i>';
    }
}

function guardarUsuario() {
    let datos = {
        usuario: $('#username').val().trim(),
        password: $('#new-password').val().trim(),
        idPerfil: $('#idRol').val(),
        activo: $('#chk_ActivarUsuario').is(":checked"),
        correoResponsable: $("#correoResponsable").val().trim()
    }

    let validar = validarNuevoUsuario(datos)
    if (validar.hayErrores) {
        llenarTablaErrores(validar.listaErrores)
    }
    else {
        let username = $('#username').val() + "@" + $("#NomCorto").val();
        $.ajax({
            url: urlGuardarUsuario,
            method: "POST",
            cache: false,
            beforeSend: function () {
                showLoading();
            },
            complete: function () {
                hideLoading();
            },
            data: {
                usuario: username,
                password: datos.password,
                idPerfil: datos.idPerfil,
                activo: datos.activo,
                correoResponsable: datos.correoResponsable,
                __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
            },
            success: function (respuesta) {
                if (respuesta.success) {
                    alerta("success", "Operación exitosa", "¡Se ha creado el nuevo usuario!", 2500)
                    limpiarModalUsuario();
                    otablausuario.ajax.reload();
                }
                else {
                    if (respuesta.code === 403) {
                        alerta("error", "Debe registrar las claves de su servicio REDECO y REUNE")
                    } else {
                        alerta("error", "Ocurrio un error al crear su usuario", "Intentelo nuevamente");
                    }
                }
            },
            error: function (err) {
                alerta("error", "Ocurrio un error en su peticion", "Intentelo nuevamente");
            }
        });
    }
}


$("#tablaUsuarios tbody").on("click", "td .boton-eliminar", function (e) {
    e.preventDefault();
    $(this).blur();
    let usuario = (otablausuario.row($(this).closest('tr')).data())["idUsuario"];
    Swal.fire({
        title: textoScript.eliminarUsuario,
        text: textoScript.noRevertir,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: textoScript.si,
        cancelButtonText: textoScript.no,
    }).then((r) => {
        if (r.value) {
            $.ajax({
                url: urlEliminarUsuario,
                method: "POST",
                cache: false,
                beforeSend: function () {
                    showLoading();
                },
                complete: function () {
                    hideLoading();
                },
                data: {
                    idUsuario: usuario,
                    __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
                },
                success: function (respuesta) {
                    if (respuesta.success) {
                        Swal.fire({
                            icon: "success",
                            title: "Operación exitosa",
                            text: textoScript.usuarioEliminado,
                            showConfirmButton: false,
                            timer: 2500
                        });
                    }
                    else {
                        Swal.fire({
                            icon: "error",
                            title: "Ocurrió un error",
                            text: textoScript.errorEliminarUsuario,
                        });
                    }
                    otablausuario.ajax.reload();
                },
                error: function (xhr, e, t) {
                    Swal.fire({
                        icon: "error",
                        title: "Error",
                        text: "Ocurrió un error en su petición. Inténtelo nuevamente",
                    });
                }
            });
        }
        else {
            return;
        }
    });
});

$("#tablaUsuarios tbody").on("click", "td .boton-desactivar", function (e) {
    e.preventDefault();
    $(this).blur();
    let usuario = (otablausuario.row($(this).closest('tr')).data())["idUsuario"];
    let activo = (otablausuario.row($(this).closest('tr')).data())["activo"];
    activo = !activo;

    let titulo = !activo ? textoScript.pDesactivarUsuario : textoScript.pActivarUsuario;

    Swal.fire({
        title: titulo,
        text: textoScript.noRevertir,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: textoScript.si,
        cancelButtonText: textoScript.no,
    }).then((r) => {
        if (r.value) {
            $.ajax({
                url: urlDesactivarUsuario,
                method: "POST",
                cache: false,
                beforeSend: function () {
                    showLoading();
                },
                complete: function () {
                    hideLoading();
                },
                data: {
                    idUsuario: usuario,
                    estaActivado: activo,
                    __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
                },
                success: function (respuesta) {
                    if (respuesta.success) {
                        alerta("success", "Operación exitosa", textoScript.mUsuarioDesactivado, 2500)
                    }
                    else {
                        alerta("error", "Ocurrió un error", textoScript.mErrorDesactivarUsuario, 2500)
                    }
                    otablausuario.ajax.reload();
                },
                error: function (xhr, e, t) {
                    alerta("error", "Ocurrió un error en su peticion", "Intentelo nuevamente", 2500)
                }
            });
        }
        else {
            return;
        }
    });
});

$("#tablaUsuarios tbody").on("click", "td .boton-correo", function (e) {
    e.preventDefault();
    $(this).blur();
    let usuario = (otablausuario.row($(this).closest('tr')).data())["idUsuario"];
    $("#idUsuario").val(usuario)
});

$("#tablaUsuarios tbody").on("click", "td .boton-cambiar", function (e) {
    e.preventDefault();
    $(this).blur();
    let usuario = (otablausuario.row($(this).closest('tr')).data())["idUsuario"];
    $("#idUsuario").val(usuario)
});
function cambiarCorreoResponsable() {
    let correoResponsable = $('#correoResponsable2').val().trim();
    let usuario = $("#idUsuario").val();
    $.ajax({
        url: urlCambiarCorreoResponsable,
        method: "POST",
        cache: false,
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        data: {
            idUsuario: usuario, 
            correo: correoResponsable,
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
        },
        success: function (respuesta) {
            if (respuesta.success) {
                alerta("success", "Operación exitosa", "¡Se ha cambiado el correo del responsable!", 2500);
                limpiarModalCorreo();
                otablausuario.ajax.reload();
            }
            else {
                alerta("error", "Ocurrio un error", "¡No se pudo cambiar este correo. Intentelo nuevamente.", 2500);
            }
        },
        error: function (err) {
            alerta("error", "Ocurrio un error en su peticion", "¡Intentelo nuevamente!", 2500);
        }
    });
}

function cambiarPasswordUsuario() {
    let newPassword = $("#new-password2").val().trim();
    let confimPassword = $("#confirm-new-password").val().trim();
    if (newPassword != confimPassword) alerta("error", "Las contraseñas deben ser iguales")
    else {
        let usuario = $("#idUsuario").val();
        $.ajax({
            url: urlCambiarPasswordUsuario,
            method: "POST",
            cache: false,
            beforeSend: function () {
                showLoading();
            },
            complete: function () {
                hideLoading();
            },
            data: {
                idUsuario: usuario,
                password: newPassword,
                __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
            },
            success: function (respuesta) {
                if (respuesta.success) {
                    alerta("success", "Operación exitosa", "¡Se ha cambiado la contraseña del usuario!", 2500);
                    limpiarModalPassword();
                }
                else {
                    alerta("error", "Ocurrio un error", "¡No se pudo cambiar la contraseña del usuario. Intentelo nuevamente.", 2500);
                }
            },
            error: function (err) {
                alerta("error", "Ocurrio un error en su peticion", "¡Intentelo nuevamente!", 2500);
            }
        });
    }
}

function limpiarModalUsuario() {
    $("#nuevoUsuarioModal").modal("hide");
    $('#username').val("");
    $('#new-password').val("");
    $('#correoResponsable').val("");
    reiniciarSelect("idRol")
    $('#chk_ActivarUsuario').prop("checked", true);
}

function limpiarModalCorreo() {
    $('#correoResponsable2').val("");
}

function limpiarModalPassword() {
    $('#new-password2').val("");
    $('#confirm-new-password2').val("");
}

$('#actualizarToken-btn').on('click', function () {
    Swal.fire({
        title: "¿Está seguro?",
        text: "¿Está seguro de actualizar los tokens?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Sí"
    }).then((result) => {
        if (result.isConfirmed) {    
            $.ajax({
                url: urlActualizarToken,
                method: "POST",
                cache: false,
                beforeSend: function () {
                    showLoading();
                },
                complete: function () {
                    hideLoading();
                },
                data: {
                    tokenRedeco: $('#TokenRedeco').val().trim(),
                    tokenReune: $('#TokenReune').val().trim(),
                    __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
                },
                success: function (respuesta) {
                    if (respuesta.success) {
                        alertaConRecarga("success", "Operación exitosa", "¡Se han registrado las claves de sus servicios!")
                    }
                    else {
                        alerta("error", "Ocurrió un error", "No se guardaron sus claves de servicios. Intentelo nuevamente")
                    }
                },
                error: function (err) {
                    alerta("error", "Error", "Ocurrió un error en su petición. Inténtelo nuevamente")
                }
            });
        }
    });
})

$("#btn_descargarProductos").on("click", function () {
    $.ajax({
        url: urlDescargarProductos,
        method: "POST",
        cache: false,
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        data: {
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
        },
        success: function (respuesta) {
            if (respuesta.success) {
                Swal.fire({
                    icon: "success",
                    title: "Operación exitosa",
                    text: "¡Se han descargado los productos de la empresa!",
                    showConfirmButton: false,
                    timer: 2500
                });
                //otablausuario.ajax.reload();
            }
            else {
                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: "Ocurrió un error en su petición. Inténtelo nuevamente",
                });
            }
        },
        error: function (err) {
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Ocurrió un error en su petición. Inténtelo nuevamente",
            });
        }
    });
})

$("#btn_GenerarSuperUser").on("click", function () {
    $.ajax({
        url: urlGenerarSuperUsuario,
        method: "POST",
        cache: false,
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        data: {
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
        },
        success: function (respuesta) {
            if (respuesta.success) {
                alertaConRecarga("success", "Operacion exitosa", "¡Se ha generado su super usuario en CONDUSEF. Ya puede crear nuevos usuarios",4000)
            }
            else {
                if (respuesta.code === 501) alerta("error", "Debe registrar las claves de su servicio REDECO y REUNE");
                else if (respuesta.code === 401) alerta("error", textoScript.mErrorKeyInvalida,"",4000);
                else alerta("error", "Ocurrio un error", "No se pudo crear su super usuario en CONDUSEF. Inténtelo nuevamente");
            }
        },
        error: function (err) {
            alerta("error", "Error", "Ocurrió un error en su petición. Inténtelo nuevamente")
        }
    });
})

$("#actualizarPerfil").on("click", function () {
    let datos = {
        nombre: $("#Nombre").val().trim(),
        DirCalle: $("#DirCalle").val().trim(),
        DirColonia: $("#DirColonia").val().trim(),
        DirNumInt: $("#DirNumInt").val().trim(),
        DirNumExt: $("#DirNumExt").val().trim(),
        DirCP: $("#DirCP").val().trim(),
        DirMunicipio: $("#DirMunicipio").val().trim(),
        DirCiudad: $("#DirCiudad").val().trim(),
        DirEstado: $("#DirEstado").val(),
        CorreoContacto: $("#CorreoContacto").val().trim(),
        TelefonoContacto: $("#TelefonoContacto").val().trim(),
        PersonaContacto: $("#PersonaContacto").val().trim()
    }
    $.ajax({
        url: urlActualizarDatosEmpresa,
        method: "POST",
        cache: false,
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        data: {
            datosEmpresa: datos,
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
        },
        success: function (respuesta) {
            if (respuesta.success) {
                alerta("success", "Operación exitosa", "Se actualizó la información de la empresa", 2500)
            }
            else {
                alerta("error", "Ocurrió un error", "No se puedo actualizar la información de la empresa. Intentelo nuevamente", 2500)
            }
        },
        error: function (err) {
            alerta("error", "Ocurrió un error en su petición", "Inténtelo nuevamente", 2500)
        }
    });
})

function validarFormatoTokensCondusef(cadena) {
    // Expresión regular para validar el formato
    var patron = /^\d+\|\d+\|\d+\|(SI|NO)\|NO$/;

    // Se verifica si la cadena coincide con el patrón
    return patron.test(cadena);
}

function validarNuevoUsuario(datos) {
    let errores = [];
    // Expresiones regulares para los requisitos
    const tieneNumero = /\d/;
    const tieneMinuscula = /[a-z]/;
    const tieneMayuscula = /[A-Z]/;

    if (esCadenaVaciaOBlanca(datos.usuario)) errores.push("Debe agregar un usuario valido");
    if (esCadenaVaciaOBlanca(datos.password)) errores.push("Debe agregar una contraseña valida");
    if (esEnteroValidoNiCero(datos.idPerfil)) errores.push("Debe seleccionar un tipo de perfil de usuario");
    if (esCadenaVaciaOBlanca(datos.correoResponsable)) errores.push("Debe agregar un email valido");

    if(datos.password.length < 12) errores.push("La contraseña debe tener mínimo 12 caracteres")
    if (!tieneNumero.test(datos.password)) errores.push("La contraseña debe contener al menos un número")
    if (!tieneMinuscula.test(datos.password)) errores.push("La contraseña debe contener al menos una letra minúscula")
    if (!tieneMayuscula.test(datos.password)) errores.push("La contraseña debe contener al menos una letra mayúscula")

    let _hayErrores = errores.length > 0;
    return {
        hayErrores: _hayErrores,
        listaErrores: errores
    }
}

$("#modalErrores").on('hidden.bs.modal', function () {
    otablaErroress.clear().draw()
});