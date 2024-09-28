






function cambiarIconoPassword(icono) {
    var inputPassword = document.getElementById("new-password");
    var tipoInput = inputPassword.getAttribute("type");

    if (tipoInput === "password") {
        inputPassword.setAttribute("type", "text");
        icono.innerHTML = '<i class="fa-solid fa-eye-slash icon-blue"></i>';
    } else {
        inputPassword.setAttribute("type", "password");
        icono.innerHTML = '<i class="fa-solid fa-eye icon-blue"></i>';
    }
}

function cambiarIconoPassword2(icono) {
    var inputPassword = document.getElementById("confirm-new-password");
    var tipoInput = inputPassword.getAttribute("type");

    if (tipoInput === "password") {
        inputPassword.setAttribute("type", "text");
        icono.innerHTML = '<i class="fa-solid fa-eye-slash icon-blue"></i>';
    } else {
        inputPassword.setAttribute("type", "password");
        icono.innerHTML = '<i class="fa-solid fa-eye icon-blue"></i>';
    }
}

$("#btn_enviarSolicitud").on("click", function () {
    let password = $("#new-password").val();
    let confimPassword = $("#confirm-new-password").val();
    if (password != confimPassword) alerta("error", "Las contraseñas deben ser iguales")
    else {
        $.ajax({
            url: urlSolicitarCambio,
            method: "POST",
            cache: false,
            data: {
                otp: $("#otp").val(),
                password: $("#new-password").val(),
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
                    alerta("success", "¡Se ha cambiado su contraseña!", "Ya puede iniciar sesion", 3000)
                    setTimeout(() => {
                        location.href = "/"
                    }, 3000)
                } else {
                    if (respuesta.code == 400) alerta("error", "¡Su codigo de verificacion ha expirado!", "Solicite uno nuevo")
                    else alerta("error", "Ocurrio un error al cambiar su contraseña. Intentelo nuevamente", "")
                }
            },
            error: function (respuesta) {
                alerta("error", "Ocurrio un error con su peticion. Intentelo nuevamente")
            }
        })
    }
})





