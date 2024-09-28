
//$(document).ready(function () {
//    $('.selectitems').select2();
//});


$("#btn_cp").on("click", function () {
    $.ajax({
        url: urlCatalogoCP,
        method: "POST",
        cache: false,
        data: {
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
                    title: "Exito",
                    showConfirmButton: false,
                    timer: 2500
                })   
            }
            else {
                Swal.fire({
                    icon: "error",
                    title: "Ocurrio error en cp",
                    showConfirmButton: false,
                    timer: 2500
                })
            }

        },
        error: function (respuesta) {
            console.error('Ocurrio un error:', respuesta)
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Something went wrong!",
            });
        }
    })
})


$("#btn_guardarEmpresa").on("click", function () {
    $.ajax({
        url: urlAgregarEmpresa,
        method: "POST",
        cache: false,
        data: {
            nombre: $("#Nombre").val(),
            nomCorto: $("#NomCorto").val(),
            rfc: $("#RFC").val(),
            casfim: $("#CASFIM").val(),
            tokenRedeco: $("#TokenRedeco").val(),
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
                    title: "Exito al agregar usuario",
                    showConfirmButton: false,
                    timer: 2500
                })
            }
            else {
                Swal.fire({
                    icon: "error",
                    title: "Ocurrio error en cp",
                    showConfirmButton: false,
                    timer: 2500
                })
            }
            limpiarModal();
        },
        error: function (respuesta) {
            console.error('Ocurrio un error:', respuesta)
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Something went wrong!",
            });
        }
    })
})

function limpiarModal() {
    $("#btn_agregarEmpresa_close").click();
    $("#Nombre").val("");
    $("#NomCorto").val("");
    $("#RFC").val("");
    $("#CASFIM").val("");
    $("#TokenRedeco").val("");
}

$("#btn_datosUsuarios").on('click', function () {
    Swal.fire({
        icon: "success",
        title: "Exito al agregar usuario",
        showConfirmButton: false,
        timer: 2500
    })
    $("#usuario").val("administrador@mexfactor");
    $("#password").val("skaBiljoMrhe0L5U");
    $("#modalDatosUsuario").show();
})

function mostrarDatos(username, password) {
    $("#usuario").val(username);
    $("#password").val(password);
    $("#modalDatosUsuario").show();
}

$("#btn_cambiarToken").on("click", function () {
    $.ajax({
        url: urlCambiarTokenUsuario,
        method: "POST",
        cache: false,
        data: {
            idUsuario: $("#id-usuario").val(),
            token: $("#token").val(),
            idServicio: $("#servicio").val()
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
                    title: "Exito al cambiar el token",
                    showConfirmButton: false,
                    timer: 2500
                })
            }
            else {
                Swal.fire({
                    icon: "error",
                    title: "Ocurrio error en cp",
                    showConfirmButton: false,
                    timer: 2500
                })
            }
        },
        error: function (respuesta) {
            console.error('Ocurrio un error:', respuesta)
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Something went wrong!",
            });
        }
    })
})

$("#btn_obtenerToken").on("click", function () {
    $.ajax({
        url: urlObtenerTokenUsuario,
        method: "POST",
        cache: false,
        data: {
            idUsuario: $("#id-usuario2").val(),
            idServicio: $("#servicio2").val()
        },
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        success: function (respuesta) {
            if (respuesta.success) {
                $("#token2").val(respuesta.message);
                alerta("success", "Exito al obtener el token","")
            }
            else {
                alerta("error", "No se pudo obtener el token del usuario", "")
            }
        },
        error: function (respuesta) {
            console.error('Ocurrio un error:', respuesta)
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Something went wrong!",
            });
        }
    })
})

function CambiaFakePath(input) {
    var FileName = input.value.toString().replace("C:\\fakepath\\", "");
    $("#inputname").val(FileName);
};

$("#subirLayoutBtn").on("click", function () {
    var Data = new FormData;
    Data.append("layout", document.getElementById('labelinput').files[0]);
    Data.append("__RequestVerificationToken", $("input[name='__RequestVerificationToken']").val());
    $.ajax({
        url: urlSubirCatalogoProdReune,
        method: "POST",
        cache: false,
        data: Data,
        contentType: false,
        processData: false,
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        success: function (respuesta) {
            if (respuesta.success) {
                $("#token2").val(respuesta.message);
                alerta("success", "Exito al guardar excel", "")
            }
            else {
                alerta("error", "No se pudo guardar excel", "")
            }
        },
        error: function (respuesta) {
            console.error('Ocurrio un error:', respuesta)
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Something went wrong!",
            });
        }
    })
})

document.getElementById('copy-token').addEventListener('click', function () {
    var textToCopy = document.getElementById('token2').value;

    navigator.clipboard.writeText(textToCopy).then(function () {
        alert('¡Texto copiado al portapapeles!');
    }, function (err) {
        console.error('Error al copiar al portapapeles: ', err);
    });
});











