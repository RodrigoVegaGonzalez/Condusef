
$('#btnLogin').on('click', function () {
    //__RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
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
            username: $('#email').val(),
            password: $('#password').val(),
        },
        success: function (respuesta) {
            if (!respuesta.success) {
                alerta("error", textoScript.errorLoginTitulo, textoScript.errorLogin)
            }
            else {
                window.location.href = respuesta.message;
            }
        },
        error: function (err) {
            alerta("error", textoScript.errorLoginTitulo, textoScript.errorLogin)
        }
    });
})

$('#btnRecuperar').on('click', function () {
    alert('j')

})

