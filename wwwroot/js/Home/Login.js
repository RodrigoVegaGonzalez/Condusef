


$('#btnLogin').on('click', function () {
    $.ajax({
        url: urlIngresar,
        method: "POST",
        cache: false,
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        data: {
            username: $('#email').val().trim(),
            password: $('#password').val().trim(),
            remember: false,
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
        },
        success: function (respuesta) {
            console.log(respuesta)
            if (respuesta.success) {
                window.location.href = respuesta.message;
            }
            else {
                if (respuesta.code === 403) {
                    alerta("error", textoScript.errorLoginTitulo, textoScript.errorCuentaDesactivada)
                }
                else {
                    alerta("error", textoScript.errorLoginTitulo, textoScript.errorLogin)
                }
            }
        },
        error: function (err) {
            console.log(respuesta)
            alerta("error", textoScript.ErrorGenerico)
        } 
    });
})

$('#btnRecuperar').on('click', function () {
    alert('j')
})

