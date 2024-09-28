
$("#btn_enviarSolicitud").on("click", function () {
    $.ajax({
        url: urlSolicitarCambio,
        method: "POST",
        cache: false,
        data: {
            username: $("#usuario").val(),
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
                alerta("success", 'Se ha enviado correo con su codigo de verificacion', 'Revise su bandeja de entrada o de spam', 4000);
                setTimeout(() => {
                    location.href = "/"
                }, 4000)
            } else {
                if(respuesta.code === 400) alerta("error", "Este usuario no existe en nuestro sistema.", "")
                else alerta("error", "Ocurrio un error al solicitar su cambio de contraseña. Intentelo nuevamente", "")
            }
        },
        error: function (respuesta) {
            alerta("error", "Ocurrio un error con su peticion. Intentelo nuevamente")
        }
    })
})